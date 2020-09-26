using System;
using Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Web.Handlers.Authentication
{
	public static class MiddlewareAuthExtensions
	{
		public static AuthenticationBuilder AddMiddlewareAuth(this AuthenticationBuilder builder)
			=> builder.AddMiddlewareAuth(MiddlewareAuthDefaults.AuthenticationScheme, _ => { });

		public static AuthenticationBuilder AddMiddlewareAuth(this AuthenticationBuilder builder,
																		Action<MiddlewareAuthOptions> configureOptions)
			=> builder.AddMiddlewareAuth(MiddlewareAuthDefaults.AuthenticationScheme, configureOptions);

		public static AuthenticationBuilder AddMiddlewareAuth(this AuthenticationBuilder builder, string authenticationScheme,
																		Action<MiddlewareAuthOptions> configureOptions)
			=> builder.AddMiddlewareAuth(authenticationScheme, MiddlewareAuthDefaults.AuthenticationScheme, configureOptions);

		public static AuthenticationBuilder AddMiddlewareAuth(this AuthenticationBuilder builder, string authenticationScheme,
																		string displayName, Action<MiddlewareAuthOptions> configureOptions)
		{
			builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<MiddlewareAuthOptions>, MiddlewareAuthPostConfigureOptions>());
			return builder.AddScheme<MiddlewareAuthOptions, MiddlewareAuthHandler>(authenticationScheme, displayName, configureOptions);
		}
	}
}