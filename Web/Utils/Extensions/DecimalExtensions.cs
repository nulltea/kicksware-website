using System.ComponentModel.DataAnnotations;
using Core.Extension;
using Core.Reference;

namespace Web.Utils.Extensions
{
	public static class DecimalExtensions
	{
		public static string FormatCurrency(this decimal price, Currency currency = Currency.UsDollar)
		{
			var sign =  currency.GetEnumAttribute<DisplayAttribute>()?.ShortName ?? "$";
			return $"{sign}{price:F}";
		}
	}
}