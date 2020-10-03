using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
	public static class Controllers
	{
		public static JsonResult FormSubmitResult(SubmitResult result, string message) => new JsonResult(new
		{
			Result = result.ToString().ToLower(),
			Message = message
		});

		public enum SubmitResult
		{
			Success,

			Error,

			Warning,

			Info,

			Question
		}
	}
}