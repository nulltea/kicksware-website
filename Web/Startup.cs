using System;
using System.IO;
using Core.Entities.Products;
using Core.Entities.References;
using Core.Entities.Users;
using Core.Gateway;
using Core.Model;
using Core.Reference;
using Core.Repositories;
using Core.Services;
using Core.Services.Interactive;
using Elastic.Apm.NetCoreAll;
using Infrastructure.Data;
using Infrastructure.Gateway.gRPC.Builder;
using Infrastructure.Gateway.REST;
using Infrastructure.Gateway.REST.Builder;
using Infrastructure.Gateway.REST.Client;
using Infrastructure.Usecase;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using SmartBreadcrumbs.Extensions;
using Web.Config;
using Web.Container.Factory;
using Web.Handlers.Authentication;
using Web.Handlers.Authorisation;
using Web.Handlers.Filter;
using Web.Handlers.Users;

namespace Web
{
	public class Startup
	{
		private IWebHostEnvironment HostEnvironment { get; }

		public Startup(IConfiguration configuration, IWebHostEnvironment env) => (Configuration, HostEnvironment) = (configuration, env);

		private IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDefaultIdentity<User>(ConfigureAuthOptions);
			services.AddControllersWithViews();
			services.AddHttpContextAccessor();
			services.AddSession();

			services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));

			services.AddBreadcrumbs(GetType().Assembly, options =>
			{
				options.TagName = "nav";
				options.TagClasses = "";
				options.OlClasses = "breadcrumb";
				options.LiClasses = "breadcrumb-item";
				options.ActiveLiClasses = "breadcrumb-item active";
				options.SeparatorElement = "<li class=\"separator\">\u276F</li>";
			});

			var builder = services.AddRazorPages();
#if DEBUG
			if (HostEnvironment.IsDevelopment()) builder.AddRazorRuntimeCompilation();
#endif

			#region Dependency injection

			if (Core.Environment.Environment.DataProtocol == DataProtocol.gRPC)
			{
				services.AddSingleton(ServiceFactory.ProvideGrpcChannel);

				services.AddSingleton<Proto.UserService.UserServiceClient>();
				services.AddSingleton<Proto.ReferenceService.ReferenceServiceClient>();
				services.AddSingleton<Proto.ProductService.ProductServiceClient>();
				services.AddSingleton<Proto.SearchReferencesService.SearchReferencesServiceClient>();
				services.AddSingleton<Proto.SearchProductService.SearchProductServiceClient>();
				services.AddSingleton<Proto.AuthService.AuthServiceClient>();
				services.AddSingleton<Proto.MailService.MailServiceClient>();
				services.AddSingleton<Proto.InteractService.InteractServiceClient>();

				services.AddTransient<ISneakerProductRepository, SneakerProductsGrpcRepository>();
				services.AddTransient<ISneakerReferenceRepository, SneakerReferencesGrpcRepository>();
				services.AddTransient<IUserRepository, UserGrpcRepository>();

				services.AddSingleton<IAuthService, AuthServiceGRPC>();
				services.AddTransient<IMailService, MailServiceGRPC>();
				services.AddTransient<ILikeService, InteractServiceGRPC>();
				services.AddTransient<IReferenceSearchService, ReferenceSearchServiceGRPC>();

				services.AddSingleton<IQueryBuilder, QueryBuilderGRPC>();
			}
			else
			{
				services.AddTransient<IGatewayClient<IGatewayRestRequest>, RestfulClient>(ServiceFactory.ProvideRestClient);

				services.AddTransient<ISneakerProductRepository, SneakerProductsRestRepository>();
				services.AddTransient<ISneakerReferenceRepository, SneakerReferencesRestRepository>();
				services.AddTransient<IUserRepository, UserRestRepository>();

				services.AddSingleton<IAuthService, AuthServiceREST>();
				services.AddTransient<IMailService, MailServiceREST>();
				services.AddTransient<ILikeService, InteractServiceREST>();
				services.AddTransient<IReferenceSearchService, ReferenceSearchServiceREST>();

				services.AddSingleton<IQueryBuilder, QueryBuilderREST>();
			}

			// TODO REST -> Transient for RestfulClient
			services.AddSingleton<ICommonService<SneakerReference>, SneakerReferenceService>();
			services.AddSingleton<ICommonService<SneakerProduct>, SneakerProductService>();
			services.AddSingleton<ISneakerReferenceService, SneakerReferenceService>();
			services.AddSingleton<ISneakerProductService, SneakerProductService>();
			services.AddSingleton<IUserService, UserService>();

			services.AddTransient<FilterContentBuilder<SneakerReference>, ReferencesFilterContent>();
			services.AddTransient<FilterContentBuilder<SneakerProduct>, ProductsFilterContent>();

			services.AddTransient(ServiceFactory.ProvideShopMenuBuilder);
			services.AddTransient(ServiceFactory.ProvideMobileShopMenuBuilder);

			services.AddTransient<IUserStore<User>, UserStore>();
			services.AddTransient<SignInManager<User>, MiddlewareSignInManager>();
			services.AddTransient<IAuthorizationHandler, NotGuestHandler>();

			services.AddSingleton<ISecureDataFormat<AuthToken>, SecureDataFormat<AuthToken>>(ServiceFactory.ProvideSecureTokenFormat);

			#endregion

			#region Authentication

			services
				.AddAuthentication(ConfigureAuthOptions)
				.AddMiddlewareAuth(ConfigureAuthOptions)
				.AddFacebook(facebookOptions =>
				{
					facebookOptions.AppId = Environment.GetEnvironmentVariable("AUTH_FACEBOOK_ID");
					facebookOptions.AppSecret = Environment.GetEnvironmentVariable("AUTH_FACEBOOK_SECRET");
				})
				.AddGoogle(options =>
				{
					options.ClientId = Environment.GetEnvironmentVariable("AUTH_GOOGLE_ID");
					options.ClientSecret = Environment.GetEnvironmentVariable("AUTH_GOOGLE_SECRET");
				});

			services.AddAuthorization(ConfigureAuthOptions);

			services.AddDataProtection()
				.PersistKeysToFileSystem(new DirectoryInfo(@"/keys"))
				.SetApplicationName("kicksware");

			#endregion
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}
			app.UseHttpsRedirection();

			app.UseRouting();
			app.UseSession();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}"
				);
				endpoints.MapRazorPages();
			});

			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider =
					new PhysicalFileProvider(@"/source/storage"),
				RequestPath = new PathString("/storage"),
			});
			app.UseStaticFiles();

			app.UseAllElasticApm(Configuration);
		}

		#region Configuration handlers

		private static void ConfigureAuthOptions(AuthenticationOptions options)
		{
			options.DefaultScheme = MiddlewareAuthDefaults.AuthenticationScheme;
			options.DefaultSignInScheme = MiddlewareAuthDefaults.AuthenticationScheme;
			options.DefaultAuthenticateScheme = MiddlewareAuthDefaults.AuthenticationScheme;
			options.DefaultSignOutScheme = MiddlewareAuthDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = MiddlewareAuthDefaults.AuthenticationScheme;
			options.SchemeMap[IdentityConstants.ApplicationScheme].HandlerType = typeof(MiddlewareAuthHandler);
		}

		private static void ConfigureAuthOptions(IdentityOptions options)
		{
			options.SignIn.RequireConfirmedEmail = true;
			options.SignIn.RequireConfirmedPhoneNumber = false;

			options.Password.RequireUppercase = true;
			options.Password.RequireLowercase = true;
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequireDigit = true;
			options.Password.RequiredLength = 6;

			options.User.RequireUniqueEmail = true;
		}

		private static void ConfigureAuthOptions(AuthorizationOptions options)
		{
			options.AddPolicy("NotGuest", policy => policy.Requirements.Add(new NotGuestRequirement()));
		}

		private static void ConfigureAuthOptions(MiddlewareAuthOptions options)
		{

		}

		#endregion
	}
}
