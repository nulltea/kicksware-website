using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.References;
using Core.Extension;
using Core.Reference;
using Google.Protobuf.WellKnownTypes;

namespace Infrastructure.Gateway.gRPC
{
	public static partial class ProtoCast
	{
		public static SneakerReference ToNative(this Proto.SneakerReference message)
		{
			return new SneakerReference
			{
				UniqueID = message.UniqueId,
				ManufactureSku = message.ManufactureSku,
				BrandName = message.BrandName,
				ModelName = message.ModelName,
				BaseModelName = message.BaseModelName,
				Description = message.Description,
				Color = message.Color,
				Gender = message.Gender.GetEnumByMemberValue<Gender>(),
				Nickname = message.Nickname,
				Materials = message.Materials.ToList(),
				Categories = message.Categories.ToList(),
				ReleaseDate = message.ReleaseDate.ToDateTime(),
				Price = Convert.ToDecimal(message.Price),
				ImageLink = message.ImageLink,
				ImageLinks = message.ImageLinks.ToList(),
				StadiumUrl = message.StadiumUrl,
			};
		}

		public static Proto.SneakerReference FromNative(this SneakerReference native)
		{
			return new Proto.SneakerReference
			{
				UniqueId = native.UniqueID,
				ManufactureSku = native.ManufactureSku,
				BrandName = native.BrandName,
				ModelName = native.ModelName,
				BaseModelName = native.BaseModelName,
				Description = native.Description,
				Color = native.Color,
				Gender = native.Gender.GetEnumMemberValue(),
				Nickname = native.Nickname,
				Materials = { native.Materials },
				Categories = { native.Categories },
				ReleaseDate = native.ReleaseDate.ToTimestamp(),
				Price = Convert.ToDouble(native.Price),
				ImageLink = native.ImageLink,
				ImageLinks = { native.ImageLinks },
				StadiumUrl = native.StadiumUrl,
			};
		}

		public static List<SneakerReference> ToNative(this List<Proto.SneakerReference> messages)
		{
			return messages.Select(ToNative).ToList();
		}

		public static List<Proto.SneakerReference> FromNative(this List<SneakerReference> natives)
		{
			return natives.Select(FromNative).ToList();
		}

		public static SneakerBrand ToNative(this Proto.SneakerBrand message)
		{
			return new SneakerBrand(message.Name)
			{
				Description = message.Description,
				Hero = message.Description,
				Logo = message.Logo
			};
		}

		public static Proto.SneakerBrand FromNative(this SneakerBrand native)
		{
			return new Proto.SneakerBrand
			{
				UniqueId = native.UniqueID,
				Name = native.Name,
				Description = native.Description,
				Hero = native.Description,
				Logo = native.Logo
			};
		}

		public static SneakerModel ToNative(this Proto.SneakerModel message)
		{
			return new SneakerModel(message.Name)
			{
				BaseModel = message.BaseModel,
				Brand = message.Brand,
				Description = message.Description,
				Hero = message.Description,
			};
		}

		public static Proto.SneakerModel FromNative(this SneakerModel native)
		{
			return new Proto.SneakerModel
			{
				UniqueId = native.UniqueID,
				Name = native.Name,
				BaseModel = native.BaseModel,
				Brand = native.Brand,
				Description = native.Description,
				Hero = native.Description,
			};
		}
	}
}