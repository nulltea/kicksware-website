namespace Infrastructure.Gateway.REST.Auth
{
	public class AuthGuestRequest : AuthBaseRequest
	{
		public AuthGuestRequest(string accessKey) : base("/guest")
		{
			AddQueryParameter("access", accessKey);
		}
	}
}