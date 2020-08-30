using System;
using Core.Entities.Users;
using Google.Protobuf.WellKnownTypes;

namespace Infrastructure.Gateway.gRPC
{
	public static partial class ProtoCast
	{
		public static AuthToken ToNative(this Proto.AuthToken message)
		{
			return new AuthToken
			{
				Token = message.Token,
				Success = message.Success,
				Expires = message.Expires.ToDateTime(),
			};
		}

		public static Proto.AuthToken FromNative(this AuthToken native)
		{
			return new Proto.AuthToken
			{
				Token = native.Token,
				Success = native.Success,
				Expires = (native.Expires ?? DateTime.MaxValue).ToTimestamp(),
			};
		}

		public static Proto.User FromNative(this AuthCredentials native)
		{
			return new Proto.User
			{
				Email = native.Email,
				EmailN = native.Email,
				PasswordHash = native.PasswordHash,
			};
		}
	}
}