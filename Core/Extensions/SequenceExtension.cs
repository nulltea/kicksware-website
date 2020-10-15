using System;
using System.Collections.Generic;
using System.Linq;
using Core.Algorithms;

namespace Core.Extension
{
	public static class SequenceExtension
	{
		public static IOrderedEnumerable<TSource> OrderBySimilarity<TSource>(this IEnumerable<TSource> sequence, Func<TSource, string> selector, string pattern)
		{
			return sequence.OrderBy(item => LevenshteinDistance.Calculate(selector(item), pattern));
		}

		public static string JoinNotEmpty(this IEnumerable<object> parts, string separator)
		{
			return string.Join(separator, parts.Where(p => p != null && !string.IsNullOrEmpty(Convert.ToString(p))));
		}
	}
}