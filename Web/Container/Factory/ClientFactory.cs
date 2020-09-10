using System;
using System.IO;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Core.Environment;
using Core.Gateway;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Infrastructure.Gateway.REST;
using Infrastructure.Gateway.REST.Client;
using Microsoft.AspNetCore.Http;
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

		public static ChannelBase ProvideGrpcChannel(IServiceProvider serviceProvider)
		{
			var credentials = CallCredentials.FromInterceptor((ctx, metadata) =>
			{
				if (serviceProvider is null) throw new ArgumentNullException(nameof(serviceProvider));
				var contextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
				if (contextAccessor is null) throw new NullReferenceException(nameof(contextAccessor));

				var context = contextAccessor.HttpContext;
				if (context is null) throw new NullReferenceException(nameof(context));
				var token = context.RetrieveCookieAuthToken();
				if (!string.IsNullOrEmpty(token)) 	metadata.Add("authorization", token);
				return Task.CompletedTask;
			});

			var host = new Uri(Environment.GatewayBaseUrl).Host;
			var rootCertificate = string.Empty;
			var rootCertFile = System.Environment.GetEnvironmentVariable("TLS_CERT_FILE");
			if (!string.IsNullOrEmpty(rootCertFile)) rootCertificate = File.ReadAllText(rootCertFile);

			var channel = new Channel(host, ChannelCredentials.Create(new SslCredentials(rootCertificate), credentials));

			return channel;
		}
	}
}