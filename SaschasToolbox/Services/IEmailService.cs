using MimeKit;

using System.Threading.Tasks;

namespace SaschasToolbox.Services
{

	/// <summary>
	/// Interface IEmailService
	/// </summary>
	public interface IEmailService
	{
		/// <summary>
		/// Sends the message asynchronous.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns>Task.</returns>
		Task SendMessageAsync(MimeMessage message);
	}
}
