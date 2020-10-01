using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Extension;
using Google.Protobuf.WellKnownTypes;

namespace Infrastructure.Gateway.gRPC
{
	public static partial class ProtoCast
	{
		public static Order ToNative(this Proto.Order message)
		{
			return new Order
			{
				UniqueID = message.UniqueID,
				UserID = message.UserID,
				ReferenceID = message.ReferenceID,
				ProductID = message.ProductID,
				Price = Convert.ToDecimal(message.Price),
				SourceURL = message.SourceURL,
				Status = message.Status.GetEnumByMemberValue<OrderStatus>(),
				AddedAt = message.AddedAt.ToDateTime()
			};
		}

		public static Proto.Order FromNative(this Order native)
		{
			return new Proto.Order
			{
				UniqueID = native.UniqueID ?? string.Empty,
				UserID = native.UserID ?? string.Empty,
				ReferenceID = native.ReferenceID,
				ProductID = native.ProductID ?? string.Empty,
				Price = Convert.ToDouble(native.Price),
				SourceURL = native.SourceURL  ?? string.Empty,
				Status = native.Status.GetEnumMemberValue(),
				AddedAt = native.AddedAt?.ToTimestamp() ?? Timestamp.FromDateTime(DateTime.UtcNow)
			};
		}

		public static List<Order> ToNative(this List<Proto.Order> messages)
		{
			return messages.Select(ToNative).ToList();
		}

		public static List<Proto.Order> FromNative(this List<Order> natives)
		{
			return natives.Select(FromNative).ToList();
		}
	}
}