using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace Core.Entities
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum OrderStatus
	{
		[EnumMember(Value="draft")]
		Draft,

		[EnumMember(Value="processing")]
		Processing,

		[EnumMember(Value="on_hold")]
		OnHold,

		[EnumMember(Value="delivering")]
		Delivering,

		[EnumMember(Value="complete")]
		Complete,

		[EnumMember(Value="archive")]
		Archive
	}
}