using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Core.Attributes;
using Newtonsoft.Json.Converters;

namespace Core.Entities
{
	[EntityService(Resource = "orders")]
	public class Order: IOrder
	{
		public Order() { }

		public Order(string referenceID, string productID = default)
		{
			ReferenceID = referenceID;
			ProductID = productID;
		}

		public string UniqueID { get; set; }

		public string UserID { get; set; }

		public string ReferenceID { get; set; }

		public string ProductID { get; set; }

		public decimal Price { get; set; }

		[DataType(DataType.Url)]
		public string SourceURL { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public OrderStatus Status { get; set; }

		public DateTime? AddedAt { get; set; }
	}
}