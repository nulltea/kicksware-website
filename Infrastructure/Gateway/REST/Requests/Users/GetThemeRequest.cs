using Infrastructure.Gateway.REST.Users;
using RestSharp;

namespace Infrastructure.Gateway.REST.Requests.Users
{
	public class GetThemeRequest : UserBaseRequest
	{
		public GetThemeRequest(string userID) : base("/{userID}")
		{
			AddParameter("userID", userID, ParameterType.UrlSegment);
		}
	}
}