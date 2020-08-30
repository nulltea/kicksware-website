using System.Threading.Tasks;
using Core.Entities.Users;
using Core.Services;
using Google.Protobuf.WellKnownTypes;
using Infrastructure.Gateway.gRPC;

namespace Infrastructure.Usecase
{
	public class AuthServiceGRPC : IAuthService
	{
		private readonly Proto.AuthService.AuthServiceClient _client;

		public AuthServiceGRPC(Proto.AuthService.AuthServiceClient client) => _client = client;

		public bool SingUp(User user, AuthCredentials credentials, out AuthToken token) =>
			string.IsNullOrEmpty(token = _client.SignUp(user.FromNative())?.ToNative());

		public bool Login(AuthCredentials credentials, out AuthToken token) =>
			string.IsNullOrEmpty(token = _client.SignUp(credentials.FromNative())?.ToNative());

		public bool Remote(User user, out AuthToken token) =>
			string.IsNullOrEmpty(token = _client.Remote(user.FromNative())?.ToNative());

		public bool Guest(out AuthToken token) =>
			string.IsNullOrEmpty(token = _client.Guest(new Empty())?.ToNative());

		public void Logout(AuthToken token) => _client.Logout(token.FromNative());

		public bool RefreshToken(ref AuthToken token) =>
			(token = _client.Refresh(token.FromNative()).ToNative())?.Success ?? false;

		public bool ValidateToken(AuthToken token) => true; // TODO ValidateToken

		public async Task<AuthToken> SingUpAsync(User user) =>
			(await _client.SignUpAsync(user.FromNative()))?.ToNative();

		public async Task<AuthToken> LoginAsync(AuthCredentials credentials) =>
			(await _client.LoginAsync(credentials.FromNative()))?.ToNative();

		public async Task<AuthToken> RemoteAsync(User user) =>
			(await _client.RemoteAsync(user.FromNative()))?.ToNative();

		public async Task<AuthToken> GuestAsync() => (await _client.GuestAsync(new Empty()))?.ToNative();

		public Task LogoutAsync(AuthToken token) => _client.LogoutAsync(token.FromNative()).ResponseAsync;

		public async Task<AuthToken> RefreshTokenAsync(AuthToken token) =>
			(await _client.RefreshAsync(token.FromNative()))?.ToNative();

		public Task<bool> ValidateTokenAsync(AuthToken token) => Task.FromResult(true); // TODO ValidateToken
	}
}