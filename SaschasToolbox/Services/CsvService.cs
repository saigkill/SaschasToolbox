using Ardalis.GuardClauses;

using CsvHelper;
using CsvHelper.Configuration;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace SaschasToolbox.Services
{

	/// <summary>Service for Writing a CSV.</summary>
	public class CsvService : ICsvService
	{
		private readonly ILogger<CsvService> _logger;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="logger">Klassenlogger</param>
		public CsvService(ILogger<CsvService> logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// Writes the CSV asynchronous.
		/// </summary>
		/// <typeparam name="T">Modeltype</typeparam>
		/// <param name="list">Listen</param>
		/// <param name="targetName">Path to CSV file.</param>
		/// <param name="delimiter">Delimiter, zB. ";"</param>
		/// <returns>Asynchroner Task.</returns>
		/// <exception cref="ArgumentException">If the given <paramref name="list"/> is empty.</exception>
		/// <exception cref="ArgumentNullException">If the <paramref name="list"/> or <paramref name="targetName"/> is empty </exception>
		/// <exception cref="UnauthorizedAccessException">Access is denied for <paramref name="list"></paramref> or <paramref name="targetName"/>.</exception>
		/// <exception cref="SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="DirectoryNotFoundException">The specified path in <paramref name="targetName"></paramref> is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="IOException"><paramref name="targetName" /> includes an incorrect or invalid syntax for file name, directory name, or volume label syntax.</exception>
		public async Task WriteAsync<T>(IList<T> list, string targetName, string delimiter)
		{
			Guard.Against.Null(list);
			Guard.Against.NullOrEmpty(targetName);
			Guard.Against.NullOrEmpty(delimiter);

			if (list.Count == 0) throw new ArgumentException("The given list was empty", nameof(list));

			var config = new CsvConfiguration(new CultureInfo("de-de"))
			{
				Delimiter = delimiter
			};
			using var writer = new StreamWriter(targetName);
			await using var csv = new CsvWriter(writer, config);
			try
			{
				await csv.WriteRecordsAsync(list).ConfigureAwait(false);
				_logger.LogInformation("CSV Datei erfolgreich erstellt.");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Fehler bei CSV Erstellung: {ExMessage}", ex.Message);
				throw;
			}

			await csv.FlushAsync().ConfigureAwait(false);

			_logger.LogInformation("Flushed");
			_logger.Log(LogLevel.Debug, "Flushed");
		}

		/// <summary>
		/// Reads the specified target name.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="targetName">Name of the target.</param>
		/// <param name="delimiter">The delimiter, like: ";"</param>
		/// <param name="map">The map.</param>
		/// <returns>List&lt;T&gt;.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		public IList<T> Read<T>(string targetName, string delimiter, ClassMap<T> map)
		{
			Guard.Against.NullOrEmpty(targetName);
			Guard.Against.NullOrEmpty(delimiter);

			var config = new CsvConfiguration(new CultureInfo("de-de"))
			{
				Delimiter = delimiter
			};
			using (var reader = new StreamReader(targetName))
			using (var csv = new CsvReader(reader, config))
			{
				try
				{
					csv.Context.RegisterClassMap(map);
					var records = csv.GetRecords<T>();
					_logger.LogInformation("Einlesen des CSV beendet");
					return records.ToList();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Fehler bei CSV Erstellung: {ExMessage}", ex.Message);
					throw;
				}
			}
		}
	}
}
