using System.Collections.Generic;
using System.Linq;
using Core.Entities.Users;
using Core.Extension;
using AddressInfo = Core.Entities.Users.AddressInfo;
using PaymentInfo = Core.Entities.Users.PaymentInfo;
using User = Core.Entities.Users.User;
using YearMonth = Core.Entities.Users.YearMonth;

namespace Infrastructure.Gateway.gRPC
{
	public static partial class ProtoCast
	{
		public static User ToNative(this Proto.User message)
		{
			return new User
			{
				UniqueID = message.UniqueID,
				Username = message.Username,
				UsernameN = message.UsernameN,
				Email = message.Email,
				EmailN = message.EmailN,
				PasswordHash = message.PasswordHash,
				FirstName = message.FirstName,
				LastName = message.LastName,
				PhoneNumber = message.PhoneNumber,
				Avatar = message.Avatar,
				Location = message.Location,
				PaymentInfo = message.PaymentInfo.ToNative(),
				Liked = message.Liked.ToArray(),
				Confirmed = message.Confirmed,
				Settings = message.Settings.ToNative(),
				Role = message.Role.GetEnumByMemberValue<UserRole>(),
				Provider = message.Provider.GetEnumByMemberValue<UserProvider>(),
				ConnectedProviders = message.ConnectedProviders.ToDictionary(
					kvp => kvp.Key.GetEnumByMemberValue<UserProvider>(),
					kvp => kvp.Value
				)
			};
		}

		public static Proto.User FromNative(this User message)
		{
			return new Proto.User
			{
				UniqueID = message.UniqueID,
				Username = message.Username ?? string.Empty,
				UsernameN = message.UsernameN ?? string.Empty,
				Email = message.Email ?? string.Empty,
				EmailN = message.EmailN ?? string.Empty,
				PasswordHash = message.PasswordHash ?? string.Empty,
				FirstName = message.FirstName ?? string.Empty,
				LastName = message.LastName ?? string.Empty,
				PhoneNumber = message.PhoneNumber ?? string.Empty,
				Avatar = message.Avatar ?? string.Empty,
				Location = message.Location ?? string.Empty,
				PaymentInfo = message.PaymentInfo.FromNative(),
				Confirmed = message.Confirmed,
				Settings = message.Settings.FromNative(),
				Role = message.Role.GetEnumMemberValue(),
				Provider = message.Provider.GetEnumMemberValue(),
				ConnectedProviders = { message.ConnectedProviders.ToDictionary(
					kvp => kvp.Key.GetEnumMemberValue(),
					kvp => kvp.Value
				) }
			};
		}

		public static List<User> ToNative(this List<Proto.User> messages)
		{
			return messages.Select(ToNative).ToList();
		}

		public static List<Proto.User> FromNative(this List<User> natives)
		{
			return natives.Select(FromNative).ToList();
		}

		public static PaymentInfo ToNative(this Proto.PaymentInfo message)
		{
			return new PaymentInfo
			{
				CardNumber = message.CardNumber,
				Expires = new YearMonth(message.Expires.Year, message.Expires.Month),
				CVV = message.CVV,
				BillingInfo = message.BillingInfo.ToNative(),
			};
		}

		public static Proto.PaymentInfo FromNative(this PaymentInfo native)
		{
			return new Proto.PaymentInfo
			{
				CardNumber = native.CardNumber ?? string.Empty,
				Expires = new Proto.YearMonth {Year = native.Expires.Year, Month = native.Expires.Month},
				CVV = native.CVV ?? string.Empty,
				BillingInfo = native.BillingInfo.FromNative(),
			};
		}

		public static AddressInfo ToNative(this Proto.AddressInfo message)
		{
			return new AddressInfo
			{
				City = message.City,
				Country = message.Country,
				Region = message.Region,
				Address = message.Address,
				Address2 = message.Address2,
				PostalCode = message.PostalCode,
			};
		}

		public static Proto.AddressInfo FromNative(this AddressInfo native)
		{
			return new Proto.AddressInfo
			{
				City = native.City  ?? string.Empty,
				Country = native.Country  ?? string.Empty,
				Region = native.Region  ?? string.Empty,
				Address = native.Address  ?? string.Empty,
				Address2 = native.Address2  ?? string.Empty,
				PostalCode = native.PostalCode  ?? string.Empty,
			};
		}

		public static UserSettings ToNative(this Proto.UserSettings message)
		{
			return new UserSettings
			{
				Theme = message.Theme.GetEnumByMemberValue<Theme>(),
				LayoutView = message.LayoutView.GetEnumByMemberValue<LayoutView>()
			};
		}

		public static Proto.UserSettings FromNative(this UserSettings native)
		{
			return new Proto.UserSettings
			{
				Theme = native.Theme.GetEnumMemberValue(),
				LayoutView = native.LayoutView.GetEnumMemberValue()
			};
		}
	}
}