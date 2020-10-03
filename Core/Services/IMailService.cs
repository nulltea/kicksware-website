using System.Threading.Tasks;
using Core.Model.Responses;

namespace Core.Services
{
	public interface IMailService
	{
		bool SendEmailConfirmation(string userID, string callbackUrl);

		Task<bool> SendEmailConfirmationAsync(string userID, string callbackUrl);

		bool SendResetPasswordEmail(string userID, string callbackUrl);

		CommonResponse Subscribe(string email);

		CommonResponse Unsubscribe(string email);

		Task<bool> SendResetPasswordEmailAsync(string userID, string callbackUrl);

		bool SendNotification(string userID, string msg);

		Task<bool> SendNotificationAsync(string userID, string msg);

		Task<CommonResponse> SubscribeAsync(string email);

		Task<CommonResponse> UnsubscribeAsync(string email);
	}
}