using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

namespace SaschasToolbox.Exceptions
{

	public class ExceptionHandler
	{
		private readonly ILogger<ExceptionHandler> _logger;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="logger"></param>
		public ExceptionHandler(ILogger<ExceptionHandler> logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// Standard exception handling Async
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="action"></param>
		/// <param name="errorMessage"></param>
		/// <returns></returns>
		public async Task<T> ExecuteWithExceptionHandlingAsync<T>(Func<Task<T>> action, string errorMessage)
		{
			try
			{
				return await action().ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "{ErrorMessage}", ex.Message);
				throw;
			}
		}

		/// <summary>
		/// Standard exception handling
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="action"></param>
		/// <param name="errorMessage"></param>
		/// <returns></returns>
		public T ExecuteWithExceptionHandling<T>(Func<T> action, string errorMessage)
		{
			try
			{
				return action();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "{ErrorMessage}: {ExceptionMessage}", errorMessage, ex.Message);
				throw;
			}
		}
	}
}
