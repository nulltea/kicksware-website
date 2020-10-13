using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using Environment = Core.Environment.Environment;

namespace Web.Controllers
{
	public class AboutController : Controller
	{
		[Route("about")]
		[Breadcrumb("About", FromAction = "Index", FromController = typeof(HomeController))]
		public IActionResult About()
		{
			return View(new List<string>
			{
				$"{Environment.FileStoragePath}/heroes/about-hero.jpg",
				$"{Environment.FileStoragePath}/heroes/contact-hero.jpg",
				$"{Environment.FileStoragePath}/heroes/nike-kobe-hero.jpg",
				$"{Environment.FileStoragePath}/heroes/seller-hero.jpg",
				$"{Environment.FileStoragePath}/heroes/shop-hero.jpg",
				$"{Environment.FileStoragePath}/heroes/seller-hero2.jpg",
			});
		}
	}
}