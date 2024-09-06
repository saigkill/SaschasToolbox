using Ardalis.GuardClauses;

using System;
#pragma warning disable MA0011

namespace SaschasToolbox.Extensions
{

	/// <summary>
	/// Class DateTimeExtensions.
	/// </summary>
	public static class DateTimeExtensions
	{
		/// <summary> Converts a given DateTime Object to yyyMMdd. </summary>
		/// <param name="dt">DateTime Object</param>
		/// <returns>Integer numeric DateTime</returns>
		public static int ConvertDateToNumeric(this DateTime dt)
		{
			Guard.Against.Null(dt);
			return int.Parse(dt.ToString("yyyyMMdd"));
		}

		/// <summary>
		/// Converts a given DateTime Object to yyyy-MM-dd HH:mm:ssZ.
		/// </summary>
		/// <param name="dt">DateTime Object.</param>
		/// <returns>System.String.</returns>
		public static string ConvertDateTimeToString(this DateTime dt)
		{
			Guard.Against.Null(dt);
			return dt.ToString("yyyy-MM-dd HH:mm:ssZ");
		}
	}
}
