using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Core.Entities.Users;
using Core.Environment;
using Core.Gateway;
using Core.Services;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using Infrastructure.Gateway.REST;
using Infrastructure.Gateway.REST.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Web.Utils.Extensions;
using Environment = Core.Environment.Environment;

namespace Web.Container.Factory
{
	public static partial class ServiceFactory
	{
		public static RestfulClient ProvideRestClient(IServiceProvider serviceProvider)
		{
			if (serviceProvider is null) throw new ArgumentNullException(nameof(serviceProvider));

			var contextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
			if (contextAccessor is null) throw new NullReferenceException(nameof(contextAccessor));

			var context = contextAccessor.HttpContext;
			if (context is null) throw new NullReferenceException(nameof(context));

			var client = new RestfulClient();

			var token = context.RetrieveCookieAuthToken();
			if (!string.IsNullOrEmpty(token)) client.Authenticate(token);

			return client;
		}

		public static IHttpClientBuilder AddSecureGrpcClient<TClient>(this IServiceCollection services) where TClient : class
		{
			return services
				.AddGrpcClient<TClient>(options => options.Address = new Uri(Environment.GatewayBaseUrl))
				.AddInterceptor<AuthTokenInterceptor>()
				.ConfigureChannel(ConfigureGrpcChannel);
		}

		public static void ConfigureGrpcChannel(GrpcChannelOptions options)
		{
			var rootCertFile = System.Environment.GetEnvironmentVariable("TLS_CERT_FILE");
			var httpClientHandler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
			};
			if (!string.IsNullOrEmpty(rootCertFile)) httpClientHandler.ClientCertificates.Add(new X509Certificate2(rootCertFile));
			var httpClient = new HttpClient(httpClientHandler);

			options.MaxReceiveMessageSize = 25 * 1024 * 1024;
			options.HttpClient = httpClient;
			options.Credentials = new SslCredentials();
		}
	}

	public class AuthTokenInterceptor : Interceptor
	{
		public const string TokenMetadataKey = "authorization";

		private readonly IHttpContextAccessor _contextAccessor;

		public AuthTokenInterceptor(IHttpContextAccessor httpContextAccessor)
		{
			_contextAccessor = httpContextAccessor;
		}

		public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
		{
			return base.AsyncUnaryCall(request, AppendContextWithTokenMetadata(context), continuation);
		}

		public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, BlockingUnaryCallContinuation<TRequest, TResponse> continuation)
		{
			return base.BlockingUnaryCall(request, AppendContextWithTokenMetadata(context), continuation);
		}

		private ClientInterceptorContext<TRequest, TResponse> AppendContextWithTokenMetadata<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context)
			where TRequest : class
			where TResponse : class
		{
			if (_contextAccessor is null) throw new NullReferenceException(nameof(_contextAccessor));

			var httpContext = _contextAccessor.HttpContext;
			if (httpContext is null) throw new NullReferenceException(nameof(context));

			var token = httpContext.RetrieveCookieAuthToken();
			var metadata = new Metadata
			{
				{TokenMetadataKey, token.Token},
			};

			var options = context.Options.WithHeaders(metadata);
			return new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options);
		}
	}
}
