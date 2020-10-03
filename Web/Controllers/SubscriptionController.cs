using System.Threading.Tasks;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
	public class SubscriptionController : Controller
	{
		private IUserService _service;

		public SubscriptionController(IUserService service)
		{
			_service = service;
		}

		[Route("mail/subscribe")]
		public async Task<IActionResult> Subscribe(string email)
		{
			try
			{
				await _service.SubscribeAsync(email);
			}
			catch
			{
				return Controllers.FormSubmitResult(Controllers.SubmitResult.Warning,
					"Either you are already subscribed or something went wrong while performing your subscription, if so consider trying again soon");
			}
			return Controllers.FormSubmitResult(Controllers.SubmitResult.Success,
				"Thank you! You are successfully subscribed to the Kicksware newsletter. We won't spam");
		}

		[Route("mail/unsubscribe")]
		public IActionResult Unsubscribe(string email)
		{
			_service.UnsubscribeAsync(email);
			return Controllers.FormSubmitResult(Controllers.SubmitResult.Success,
				"Good buy, you are now unsubscribed from Kicksware newsletter");
		}
	}
}