using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.Products;
using Core.Extension;
using Core.Reference;
using Google.Protobuf.WellKnownTypes;

namespace Infrastructure.Gateway.gRPC
{
	public static partial class ProtoCast
	{
		public static SneakerProduct ToNative(this Proto.SneakerProduct message)
		{
			return new SneakerProduct
			{
				UniqueID = message.UniqueId,
				ReferenceID = message.ReferenceId,
				BrandName = message.BrandName,
				ModelName = message.ModelName,
				ModelSKU = message.ModelSKU,
				Description = message.Description,
				Color = message.Color,
				Price = Convert.ToDecimal(message.Price),
				Type = message.Type.GetEnumByMemberValue<SneakerType>(),
				AddedAt = message.AddedAt.ToDateTime(),
				Size = message.Size.ToNative(),
				Condition = message.Condition.GetEnumByMemberValue<SneakerCondition>(),
				ConditionIndex = Convert.ToDecimal(message.ConditionIndex),
				Owner = message.Owner,
			};
		}

		public static Proto.SneakerProduct FromNative(this SneakerProduct native)
		{
			return new Proto.SneakerProduct
			{
				UniqueId = native.UniqueID,
				ReferenceId = native.ReferenceID,
				BrandName = native.BrandName,
				ModelName = native.ModelName,
				ModelSKU = native.ModelSKU,
				Description = native.Description,
				Color = native.Color,
				Price = Convert.ToDouble(native.Price),
				Type = native.Type.GetEnumMemberValue(),
				AddedAt = native.AddedAt.ToTimestamp(),
				Size = native.Size.FromNative(),
				Condition = native.Condition.GetEnumMemberValue(),
				ConditionIndex = Convert.ToDouble(native.ConditionIndex),
				Owner = native.Owner,
			};
		}

		public static List<SneakerProduct> ToNative(this List<Proto.SneakerProduct> messages)
		{
			return messages.Select(ToNative).ToList();
		}

		public static List<Proto.SneakerProduct> FromNative(this List<SneakerProduct> natives)
		{
			return natives.Select(FromNative).ToList();
		}

		public static SneakerSize ToNative(this Proto.SneakerSize message)
		{
			return new SneakerSize
			{
				Centimeters = Convert.ToDecimal(message.Centimeters),
				Europe = Convert.ToDecimal(message.Europe),
				UnitedStates = Convert.ToDecimal(message.UnitedStates),
				UnitedKingdom = Convert.ToDecimal(message.UnitedKingdom),
			};
		}

		public static Proto.SneakerSize FromNative(this SneakerSize native)
		{
			return new Proto.SneakerSize
			{
				Centimeters = Convert.ToDouble(native.Centimeters),
				Europe = Convert.ToDouble(native.Europe),
				UnitedStates = Convert.ToDouble(native.UnitedStates),
				UnitedKingdom = Convert.ToDouble(native.UnitedKingdom),
			};
		}
	}
}