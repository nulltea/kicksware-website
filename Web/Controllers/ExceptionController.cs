using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
	public class ExceptionController : Controller
	{
		[Route("/exception")]
		public IActionResult Exception()
		{
			var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
			var exception = context?.Error;
			ViewBag.FromUrl = HttpContext.Request.Headers["Referer"].ToString();

			if (Response.StatusCode == 408 || Response.StatusCode == 504){ return View("Timeout", exception);}
			if (400 <= Response.StatusCode && Response.StatusCode < 500) return View("NotFound", exception);

			return View("InternalError", exception);
		}
	}
}