using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Extension;
using Core.Gateway;
using Core.Reference;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;

namespace Infrastructure.Gateway.gRPC
{
	public static partial class ProtoCast
	{
		public static RequestParams ToNative(this Proto.RequestParams message)
		{
			return new RequestParams
			{
				Limit = message.Limit,
				Offset = message.Offset,
				SortBy = message.SortBy,
				SortDirection = message.SortDirection?.GetEnumByMemberValue<SortDirection>(),
			};
		}

		public static Proto.RequestParams FromNative(this RequestParams native)
		{
			return new Proto.RequestParams
			{
				Limit = native.Limit ?? 0,
				Offset = native.Offset ?? 0,
				SortBy = native.SortBy,
				SortDirection = native.SortDirection?.GetEnumMemberValue(),
			};
		}

		public static RepeatedField<T> AsRepeatedField<T>(this IEnumerable<T> list)
		{
			var repeated = new RepeatedField<T>();
			repeated.AddRange(list);
			return repeated;
		}

		public static Struct AsStruct(this object obj)
		{
			var str = new Struct();

			var map = obj.ToMap();
			if (map.Count > 0)
			{
				foreach (var kvp in map)
				{
					str.Fields.Add(kvp.Key, kvp.Value.AsValue());
				}
			}
			return str;
		}

		public static Value AsValue(this object obj)
		{
			var value = new Value();
			switch (obj)
			{
				case Value v:
					value = v;
					break;
				case int n:
					value.NumberValue = n;
					break;
				case long n:
					value.NumberValue = n;
					break;
				case decimal n:
					value.NumberValue = (double)n;
					break;
				case float n:
					value.NumberValue = n;
					break;
				case double n:
					value.NumberValue = n;
					break;
				case bool b:
					value.BoolValue = b;
					break;
				case string s:
					value.StringValue = s;
					break;
				case Struct st:
					value.StructValue = st;
					break;
				case IEnumerable<Value> lv:
					value = Value.ForList(lv.ToArray());
					break;
				case Dictionary<string, object> m:
					value.StructValue = m.AsStruct();
					break;
				case IEnumerable l:
					value.ListValue = new ListValue();
					foreach (var e in l)
					{
						value.ListValue.Values.Add(e.AsValue());
					}
					break;
				default:
					value.StructValue = obj.AsStruct();
					break;
			}
			return value;
		}
	}
}