using System;

namespace Core.Entities.Users
{
	public class PaymentInfo
	{
		public string CardNumber { get; set; }

		public YearMonth Expires { get; set; }

		public string CVV { get; set; }

		public AddressInfo BillingInfo { get; set; } = new AddressInfo();
	}

	public struct YearMonth
	{
		public int Year { get; set; }

		public int Month { get; set; }

		public YearMonth(int year, int month) => (Year, Month) = (year, month);

		public static implicit operator YearMonth(DateTime date) => new YearMonth(date.Year, date.Month);

		public static implicit operator DateTime(YearMonth property) => new DateTime(property.Year, property.Month, 0);
	}
}