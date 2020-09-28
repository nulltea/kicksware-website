using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Core.Entities.Users
{
	public class UserSettings
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public Theme Theme { get; set; } = Theme.Dark;

		[JsonConverter(typeof(StringEnumConverter))]
		public LayoutView LayoutView { get; set; } = LayoutView.Grid;
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum Theme
	{
		[EnumMember(Value = "dark")]
		Dark,

		[EnumMember(Value = "light")]
		Light
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum LayoutView
	{
		[EnumMember(Value = "grid")]
		Grid,

		[EnumMember(Value = "list")]
		List
	}
}