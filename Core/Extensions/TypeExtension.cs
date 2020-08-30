using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Extension
{
	public static class TypeExtension
	{
		public static Dictionary<string, object> ToMap(this object source)
		{
			if (source is Dictionary<string, object> map) return map;
			return source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
				.ToDictionary(prop => prop.Name, prop => prop.GetValue(source, null));
		}

		public static TTarget CastExtend<TSource, TTarget>(this TSource entity) where TTarget : TSource, new()
		{
			var instance = Activator.CreateInstance<TTarget>();
			var type = entity.GetType();
			var properties = type.GetProperties();
			foreach (var property in properties)
			{
				if (property.CanWrite)
				{
					property.SetValue(instance, property.GetValue(entity, null), null);
				}
			}
			return instance;
		}

		public static List<TTarget> CastExtend<TSource, TTarget>(this List<TSource> entities) where TTarget : TSource, new()
		{
			return entities.Select(CastExtend<TSource, TTarget>).ToList();
		}
	}
}