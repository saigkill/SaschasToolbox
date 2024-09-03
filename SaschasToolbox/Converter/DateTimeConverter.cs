using Ardalis.GuardClauses;

using System;

namespace SaschasToolbox.Converter
{

	/// <summary>
	/// Class for converting DateTime objects.
	/// </summary>
	public static class DateTimeConverter
	{
		/// <summary>
		/// Enum for DateTime modes. Represents year, month or day.
		/// </summary>
		public enum DateTimeModes
		{
			/// <summary>
			/// The year
			/// </summary>
			Year,
			/// <summary>
			/// The month
			/// </summary>
			Month,
			/// <summary>
			/// The day
			/// </summary>
			Day
		}

		/// <summary>Extracts the Year, Month or Day depending on DateTimeModes Enum.</summary>
		/// <param name="dt">Source DateTime Object.</param>
		/// <param name="mode">Mode DateTimeModes.Year, DateTimeModes.Month oder DateTimeModes.Day.</param>
		/// <returns>Year, Month or day as Integer.</returns>
		public static int SplitDateByMode(DateTime dt, DateTimeModes mode)
		{
			Guard.Against.Null(dt);
			Guard.Against.Null(mode);
			return mode switch
			{
				DateTimeModes.Year => dt.Year,
				DateTimeModes.Month => dt.Month,
				DateTimeModes.Day => dt.Day,
				_ => 0
			};
		}
	}
}
