using JetBrains.Annotations;

using SaschasToolbox.Checker;

namespace SasachasToolbox.Tests.Checker
{

	[TestClass]
	[TestSubject(typeof(Firewall))]
	public class FirewallTest
	{
		[TestMethod]
		public void CheckIpAndPortTest()
		{
			// Arrange
			string ip = "46.30.63.183"; // infas.de
			int port = 80;

			// Act
			bool result = Firewall.CheckIpAndPort(ip, port);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void CheckIpAndPortTest_InvalidIp()
		{
			// Arrange
			string ip = "300.300.300.300";
			int port = 80;

			// Act
			bool result = Firewall.CheckIpAndPort(ip, port);

			// Assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void CheckIpAndPortTest_InvalidPort()
		{
			// Arrange
			string ip = "127.0.0.1";
			int port = -1;

			// Act
			bool result = Firewall.CheckIpAndPort(ip, port);

			// Assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void PingIpTest()
		{
			// Arrange
			string ip = "127.0.0.1";

			// Act
			bool result = Firewall.PingIp(ip);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void PingIpTest_InvalidIp()
		{
			// Arrange
			string ip = "300.300.300.300";

			// Act
			bool result = Firewall.PingIp(ip);

			// Assert
			Assert.IsFalse(result);
		}
	}
}
