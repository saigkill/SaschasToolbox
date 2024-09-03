using Ardalis.GuardClauses;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System;
using System.IO;
using System.Threading.Tasks;

using WebDav;

namespace SaschasToolbox.Services
{

	/// <summary>Service for using WebDav.</summary>
	public class WebDavService : IWebDavService
	{
		private readonly ILogger<WebDavService> _logger;
		private readonly string _password;
		private readonly string _url;
		private readonly string _username;

		/// <summary>
		/// Constructor for WebDavServiceOptions.
		/// </summary>
		/// <param name="configuration">IConfiguration</param>
		/// <param name="logger">logger</param>
		public WebDavService(IConfiguration configuration, ILogger<WebDavService> logger)
		{
			Guard.Against.Null(configuration);
			_url = Guard.Against.NullOrEmpty(configuration.GetValue<string>("WebDavServer:Url"));
			_username = Guard.Against.NullOrEmpty(configuration.GetValue<string>("WebDavServer:Username"));
			_password = Guard.Against.NullOrEmpty(configuration.GetValue<string>("WebDavServer:Password"));
			_logger = Guard.Against.Null(logger);
		}

		/// <summary>
		/// Returns the WebDavClientParams object.
		/// </summary>
		/// <returns>WebDavClientParamsobject.</returns>
		WebDavClientParams IWebDavService.GetParams()
		{
			var clp = new WebDavClientParams
			{
				BaseAddress = new Uri($"{_url}/remote.php/dav/files/{_username}/"),
				Credentials = new System.Net.NetworkCredential(_username, _password)
			};
			_logger.LogDebug("Benutzte BasseAddress: {ClpBaseAddress} ", clp.BaseAddress);
			_logger.LogDebug("Benutzte Credentials: {Username} {Password}", _username, _password);
			_logger.LogInformation("Gotten params");
			return clp;
		}

		/// <summary>Downloads a file.</summary>
		/// <param name="remoteFilepath">Path where the file should be placed..</param>
		/// <param name="localFilepath">Local File Path.</param>
		/// <returns>True oder False, jenachdem ob erfolgreich.</returns>
		/// <exception cref="Exception">Condition.</exception>
		/// <exception cref="ArgumentException">Wenn <em>remoteFilepath</em> oder <em>localFilepath</em> null ist.</exception>
		// ReSharper disable once MethodTooLong
		public async Task<bool> DownloadFileAsync(string remoteFilepath, string localFilepath)
		{
			Guard.Against.NullOrEmpty(remoteFilepath);
			Guard.Against.NullOrEmpty(localFilepath);

			try
			{
				using var client = new WebDavClient(((IWebDavService)this).GetParams());
				using var response = await client.GetRawFile(remoteFilepath).ConfigureAwait(false);
				if (!response.IsSuccessful)
				{
					_logger.LogInformation("Gotten negative Statuscode");
					return false;
				}

				using (var fileStream = File.OpenWrite(localFilepath))
				{
					await response.Stream.CopyToAsync(fileStream).ConfigureAwait(false);
				}

				_logger.LogInformation("File: {Fpath} downloaded.", localFilepath);

				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error while downloading: {ExMessage}", ex.Message);
				throw;
			}
		}

		/// <summary>
		/// Deletes a file from server.
		/// </summary>
		/// <param name="remoteFilepath">Path to file.</param>
		/// <returns>True oder False.</returns>
		public async Task<bool> DeleteFileAsync(string remoteFilepath)
		{
			Guard.Against.NullOrEmpty(remoteFilepath);

			using var client = new WebDavClient(((IWebDavService)this).GetParams());
			try
			{
				var status = await client.Delete(remoteFilepath).ConfigureAwait(false);
				if (!status.IsSuccessful)
				{
					_logger.LogInformation("Gotten negative status code.");
					return false;
				}
				_logger.LogInformation("File deleted: {RPath}", remoteFilepath);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error while deleting {RemoteFilepath}. Error: {ExMessage}", remoteFilepath, ex.Message);
				throw;
			}

			return true;
		}

		/// <summary>
		/// Uploads a file
		/// </summary>
		/// <param name="remoteFilepath">Remote Path where the file should be placed.</param>
		/// <param name="localFilepath">Local Filepath.</param>
		/// <exception cref="DirectoryNotFoundException">The specified path <paramref name="localFilepath"/> or <paramref name="remoteFilepath"/> is invalid, (for example, it is on an unmapped drive).</exception>
		/// <exception cref="IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="UnauthorizedAccessException"><paramref name="localFilepath"/> or <paramref name="remoteFilepath"/> specified a directory.
		///  -or-
		///  The caller does not have the required permission.</exception>
		/// <exception cref="FileNotFoundException">The file specified in <paramref name="localFilepath"/> or <paramref name="remoteFilepath"/> was not found.</exception>
		/// <exception cref="ArgumentException">.<paramref name="localFilepath" /> or <paramref name="remoteFilepath"/> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="NotSupportedException"><paramref name="localFilepath" /> or <paramref name="remoteFilepath"/> is in an invalid format.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="localFilepath" /> or <paramref name="remoteFilepath"/> is <see langword="null" />.</exception>
		/// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		public async Task<bool> UploadFileAsync(string localFilepath, string remoteFilepath)
		{
			Guard.Against.NullOrEmpty(localFilepath);
			Guard.Against.NullOrEmpty(remoteFilepath);

			var filestream = File.OpenRead(localFilepath);
			try
			{
				using var client = new WebDavClient(((IWebDavService)this).GetParams());
				var response = await client.PutFile(remoteFilepath, filestream, "text/plain").ConfigureAwait(false);
				if (!response.IsSuccessful)
				{
					_logger.LogInformation("Negativen StatusCode erhalten.");
					return false;
				}

				_logger.LogInformation("Datei hochgeladen: {RemoteFilepath}", remoteFilepath);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Fehler beim Hochladen: {ExMessage}", ex.Message);
				throw;
			}
		}
	}

}
