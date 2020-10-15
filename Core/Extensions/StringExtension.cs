using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.Extension
{
	public static class StringExtension
	{
		public static string ToTitleCase(this string source) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(source);

		public static string ToFormattedID(this string source, string separator = "-") =>
			new Regex("[\\n\\t;,.\\s()\\/]").Replace(source, separator);

		public static string TakeSentences(this string text, int count = 1)
		{
			var sentences = Regex.Split(text, @"(?<=[\.!\?])\s+");

			return !sentences.Any() ? text : string.Join(".", sentences[Range.EndAt(new [] {count, sentences.Length }.Min())]);
		}
	}
}