﻿using System.Collections.Generic;
using System.Linq;
using Core.Entities.Users;
using Core.Extension;

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
				Role = message.Role.GetEnumByMemberValue<UserRole>(),
				Provider = message.Provider.GetEnumByMemberValue<UserProvider>(),
			};
		}

		public static Proto.User FromNative(this User message)
		{
			return new Proto.User
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
				PaymentInfo = message.PaymentInfo.FromNative(),
				Liked = {message.Liked},
				Confirmed = message.Confirmed,
				Role = message.Role.GetEnumMemberValue(),
				Provider = message.Provider.GetEnumMemberValue(),
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
				CardNumber = native.CardNumber,
				Expires = new Proto.YearMonth {Year = native.Expires.Year, Month = native.Expires.Month},
				CVV = native.CVV,
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
				City = native.City,
				Country = native.Country,
				Region = native.Region,
				Address = native.Address,
				Address2 = native.Address2,
				PostalCode = native.PostalCode,
			};
		}
	}
}