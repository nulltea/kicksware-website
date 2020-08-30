using System.Threading.Tasks;

namespace Core.Services
{
	public interface IMailService
	{
		bool SendEmailConfirmation(string userID, string callbackUrl);

		Task<bool> SendEmailConfirmationAsync(string userID, string callbackUrl);

		bool SendResetPasswordEmail(string userID, string callbackUrl);

		Task<bool> SendResetPasswordEmailAsync(string userID, string callbackUrl);

		public bool SendNotification(string userID, string msg);

		public Task<bool> SendNotificationAsync(string userID, string msg);
	}
}