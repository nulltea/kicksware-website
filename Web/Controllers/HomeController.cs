using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.References;
using Core.Gateway;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Web.Config;
using Web.Models;

namespace Web.Controllers
{
	[DefaultBreadcrumb("Home")]
	public class HomeController : Controller
	{
		public List<HomePageInfoViewModel> HomeFeedInfo { get; set; }

		[Authorize]
		public async Task<IActionResult> Index([FromServices]IOptions<AppConfig> options)
		{
			HomeFeedInfo ??= JsonConvert.DeserializeObject<List<HomePageInfoViewModel>>(await System.IO.File.ReadAllTextAsync(options.Value.HomeFeedContentPath))?.Where(post => !post.Outdated).ToList();
			ViewBag.FeaturedReferences = await GetFeatured();
			ViewBag.LatestReferences = await GetLatest();
			return View(HomeFeedInfo);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		private Task<List<SneakerReference>> GetFeatured()
		{
			var service = HttpContext.RequestServices.GetService<ISneakerReferenceService>();
			return service.GetFeaturedAsync(new[] {"Air Fear Of God 1", "LDWaffle", "Dunk High Premium SB", "Air Jordan 1 Mid SE (GS)", "Yeezy 700 V3", "Air Max 97", "Air Max 720 ISPA", "Joyride Envelope ISPA"},
				new RequestParams {Limit = 15,});
		}

		private Task<List<SneakerReference>> GetLatest()
		{
			var service = HttpContext.RequestServices.GetService<ISneakerReferenceService>();
			return service.GetLatestAsync(15);
		}
	}
}
