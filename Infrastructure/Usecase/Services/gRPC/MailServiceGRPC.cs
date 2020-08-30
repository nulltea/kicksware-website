using System.Threading.Tasks;
using Core.Services;
using Proto;

namespace Infrastructure.Usecase
{
	public class MailServiceGRPC : IMailService
	{
		private readonly MailService.MailServiceClient _client;

		public MailServiceGRPC(MailService.MailServiceClient client) => _client = client;

		public bool SendEmailConfirmation(string userID, string callbackUrl) =>
			_client.SendEmailConfirmation(new MailRequest {UserID = userID, CallbackURL = callbackUrl}).Success;

		public async Task<bool> SendEmailConfirmationAsync(string userID, string callbackUrl) =>
			(await _client.SendEmailConfirmationAsync(new MailRequest {UserID = userID, CallbackURL = callbackUrl})).Success;

		public bool SendResetPasswordEmail(string userID, string callbackUrl) =>
			_client.SendResetPassword(new MailRequest {UserID = userID, CallbackURL = callbackUrl}).Success;

		public async Task<bool> SendResetPasswordEmailAsync(string userID, string callbackUrl) =>
			(await _client.SendResetPasswordAsync(new MailRequest {UserID = userID, CallbackURL = callbackUrl})).Success;

		public bool SendNotification(string userID, string msg) =>
			_client.SendNotification(new MailRequest {UserID = userID, MessageContent = msg}).Success;

		public async Task<bool> SendNotificationAsync(string userID, string msg) =>
			(await _client.SendNotificationAsync(new MailRequest {UserID = userID, MessageContent = msg})).Success;
	}
}