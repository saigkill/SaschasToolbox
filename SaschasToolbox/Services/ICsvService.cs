using CsvHelper.Configuration;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaschasToolbox.Services
{

	/// <summary>
	/// Interface ICsvService
	/// </summary>
	public interface ICsvService
	{
		/// <summary>
		/// Writes the asynchronous.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list">The list.</param>
		/// <param name="targetName">Name of the target.</param>
		/// <param name="delimiter">The delimiter.</param>
		/// <returns>Task.</returns>
		Task WriteAsync<T>(IList<T> list, string targetName, string delimiter);

		/// <summary>
		/// Reads the specified target name.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="targetName">Name of the target.</param>
		/// <param name="delimiter">The delimiter.</param>
		/// <param name="map">Class Map</param>
		/// <returns>List&lt;T&gt;.</returns>
		IList<T> Read<T>(string targetName, string delimiter, ClassMap<T> map);
	}
}
