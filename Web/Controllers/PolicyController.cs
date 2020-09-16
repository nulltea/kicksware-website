using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
	public class PolicyController : Controller
	{
		[Route("policy")]
		public IActionResult Policy() => RedirectToAction("Privacy");

		[Route("policy/privacy")]
		public IActionResult Privacy()
		{
			return View();
		}

		[Route("policy/terms")]
		public IActionResult Terms()
		{
			return View();
		}

		[Route("policy/cookie")]
		public IActionResult Cookie()
		{
			return View();
		}
	}
}