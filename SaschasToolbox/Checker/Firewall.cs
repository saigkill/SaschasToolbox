using Ardalis.GuardClauses;

using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace SaschasToolbox.Checker
{

	/// <summary>
	/// Class for checking the firewall.
	/// </summary>
	public static class Firewall
	{
		/// <summary>
		/// Check if a port on a ip is open.
		/// </summary>
		/// <param name="ip">IP to check</param>
		/// <param name="portNumber">Port to check.</param>
		/// <returns>State if True or False.</returns>
		public static bool CheckIpAndPort(string ip, int portNumber)
		{
			Guard.Against.NullOrEmpty(ip);
			Guard.Against.Null(ip);

			var tcpClient = new TcpClient();

			try
			{
				tcpClient.Connect(ip, portNumber);
				tcpClient.Dispose();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>
		/// Pings an IP.
		/// </summary>
		/// <param name="ip">IP to ping.</param>
		/// <returns>True or false.</returns>
		public static bool PingIp(string ip)
		{
			Guard.Against.NullOrEmpty(ip);
			try
			{
				var ping = new Ping();
				var pingReply = ping.Send(ip);
				if (pingReply.Status == IPStatus.Success)
				{
					return true;
				}

				return false;
			}
			catch
			{
				return false;
			}
		}
	}
}
