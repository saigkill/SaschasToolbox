using System.Collections.Generic;
using System.Linq;

namespace SaschasToolbox.Extensions
{

	/// <summary>
	/// Class for IEnumerable Extensions
	/// </summary>
	public static class IEnumerableExtensions
	{
		/// <summary>
		/// Checks if the IEnumerable is null or empty
		/// </summary>
		/// <typeparam name="T">Type of Source</typeparam>
		/// <param name="source">IEnumeration to check.</param>
		/// <returns>true or false</returns>
		public static bool IsEmpty<T>(this IEnumerable<T>? source)
		{
			if (source == null) return true;
			return !source.Any();
		}

		/// <summary>
		/// Checks if the IEnumerable is not null and not empty
		/// </summary>
		/// <typeparam name="T">Type of Source</typeparam>
		/// <param name="source">IEnumeration to check</param>
		/// <returns>true or false</returns>
		public static bool IsNotEmpty<T>(this IEnumerable<T>? source)
		{
			if (source != null && source.Any()) return true;
			return false;
		}
	}
}
