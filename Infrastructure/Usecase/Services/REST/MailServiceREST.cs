using System.Threading.Tasks;
using Core.Gateway;
using Core.Services;
using Infrastructure.Gateway.REST;
using Infrastructure.Gateway.REST.Mail;

namespace Infrastructure.Usecase
{
	public class MailServiceREST : IMailService
	{
		private readonly IGatewayClient<IGatewayRestRequest> _client;

		public MailServiceREST(IGatewayClient<IGatewayRestRequest> client) => _client = client;

		public bool SendEmailConfirmation(string userID, string callbackUrl) =>
			_client.Request(new PostEmailConfirmationRequest(userID, callbackUrl));

		public Task<bool> SendEmailConfirmationAsync(string userID, string callbackUrl) =>
			_client.RequestAsync(new PostEmailConfirmationRequest(userID, callbackUrl));

		public bool SendResetPasswordEmail(string userID, string callbackUrl) =>
			_client.Request(new PostPasswordResetRequest(userID, callbackUrl));

		public Task<bool> SendResetPasswordEmailAsync(string userID, string callbackUrl) =>
			_client.RequestAsync(new PostPasswordResetRequest(userID, callbackUrl));

		public bool SendNotification(string userID, string callbackUrl) => throw new System.NotImplementedException();

		public Task<bool> SendNotificationAsync(string userID, string callbackUrl) => throw new System.NotImplementedException();
	}
}