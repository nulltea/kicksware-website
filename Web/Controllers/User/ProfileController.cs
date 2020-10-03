using System.Linq;
using System.Threading.Tasks;
using Core.Environment;
using Core.Entities.Users;
using Core.Extension;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using Web.Models;

namespace Web.Controllers
{
	[Authorize(Policy = "NotGuest")]
	public class ProfileController : Controller
	{
		[ViewData]
		public string HeroCoverPath { get; set; } = $"{Environment.FileStoragePath}/heroes/profile-hero.jpg";

		[ViewData]
		public string HeroBreadTitle { get; set; } = "Profile";

		[ViewData]
		public string HeroBreadSubTitle { get; set; } = "Profile";

		[ViewData]
		public string HeroLogoPath { get; set; }

		private const string ThemeCookieKey = "THEME_COOKIE";

		private const string LayoutCookieKey = "LAYOUT_COOKIE";

		private IUserService _service;

		private UserManager<User> _userManager;

		private SignInManager<User> _authManager;

		public ProfileController(IUserService service, UserManager<User> userManager, SignInManager<User> authManager)
		{
			_service = service;
			_userManager = userManager;
			_authManager = authManager;
		}

		[HttpGet]
		[Authorize]
		[Route("profile/{mode?}")]
		[Breadcrumb("Shop", FromAction = "Index", FromController = typeof(HomeController))]
		public async Task<IActionResult> Profile()
		{
			var user = await _userManager.GetUserAsync(HttpContext.User);

			if (user is null) return View(new User());

			if (!string.IsNullOrEmpty(user.FirstName) || !string.IsNullOrEmpty(user.LastName))
			{
				HeroBreadSubTitle = string.Join(" ", user.FirstName, user.LastName);
			}
			else
			{
				HeroBreadSubTitle = user.Username ?? user.Email;
			}

			if (!string.IsNullOrEmpty(user.Avatar)) HeroLogoPath = user.Avatar;

			return View(user);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Account(UserViewModel user)
		{
			var result = await _userManager.UpdateAsync(user);

			var updateResult = result.Succeeded
				? Controllers.FormSubmitResult(Controllers.SubmitResult.Success, "Great! Account information was successfully updated")
				: Controllers.FormSubmitResult(Controllers.SubmitResult.Error, result.Errors.Select(err => err.Description).FirstOrDefault());

			await SetTheme(user.Settings.Theme);
			await SetLayoutView(user.Settings.LayoutView);

			if (!result.Succeeded) return updateResult;

			if (!string.IsNullOrWhiteSpace(user.NewPassword))
			{
				if (!user.NewPassword.Equals(user.ConfirmPassword))
				{
					return Controllers.FormSubmitResult(Controllers.SubmitResult.Error, "Password confirmation and Password must match");
				}

				result = await _userManager.ChangePasswordAsync(user, user.CurrentPassword, user.NewPassword);

				return result.Succeeded
					? Controllers.FormSubmitResult(Controllers.SubmitResult.Success, "Nice! Got yourself a new secret password")
					: Controllers.FormSubmitResult(Controllers.SubmitResult.Error, result.Errors.FirstOrDefault()?.Description);
			}



			return updateResult;
		}

		public async Task<IActionResult> SetTheme(Theme theme)
		{
			Response.Cookies.Append(ThemeCookieKey, theme.GetEnumMemberValue());

			var user = await _userManager.GetUserAsync(HttpContext.User);
			if (user == null) return Json(new {Success = true});
			user.Settings.Theme = theme;
			await _userManager.UpdateAsync(user);

			return Json(new {Success = true});
		}

		public async Task<IActionResult> GetTheme()
		{
			var user = await _userManager.GetUserAsync(HttpContext.User);

			return user is null ? Json(new { Theme = "dark" }) : Json(new { user.Settings.Theme });
		}

		public static Theme GetCookieTheme(HttpRequest context) => context.Cookies[ThemeCookieKey]?.GetEnumByMemberValue<Theme>() ?? Theme.Dark;

		public async Task<IActionResult> SetLayoutView(LayoutView view)
		{
			Response.Cookies.Append(LayoutCookieKey, view.GetEnumMemberValue());

			var user = await _userManager.GetUserAsync(HttpContext.User);
			if (user == null) return Json(new {Success = true});
			user.Settings.LayoutView = view;
			await _userManager.UpdateAsync(user);

			return Json(new {Success = true});
		}

		public async Task<IActionResult> GetLayoutView()
		{
			var user = await _userManager.GetUserAsync(HttpContext.User);

			return user is null ? Json(new { Theme = "grid" }) : Json(new { user.Settings.LayoutView });
		}

		public static LayoutView GetCookieLayoutView(HttpRequest context)
		{
			var view = context.Cookies[LayoutCookieKey]?.GetEnumByMemberValue<LayoutView>();
			return view ?? LayoutView.Grid;
		}
	}
}