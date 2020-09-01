using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
	public class HealthController : Controller
	{
		[Route("health/live")]
		public IActionResult Live()
		{
			return Ok();
		}

		[Route("health/ready")]
		public IActionResult Ready()
		{
			return Ok();
		}
	}
}