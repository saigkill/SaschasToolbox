using global::Microsoft.VisualStudio.TestTools.UnitTesting;

using JetBrains.Annotations;

using SaschasToolbox.Extensions;

using System;

namespace SasachasToolbox.Tests.Extensions
{

	[TestClass]
	[TestSubject(typeof(DateTimeExtensions))]
	public class DateTimeExtensionsTest
	{

		[TestMethod]
		public void GetDateTimeAsNumber()
		{
			// Arrange
			var dt = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Local);

			// Act
			var intDt = dt.ConvertDateToNumeric();

			// Assert
			Assert.AreEqual(20240522, intDt);
		}

		[TestMethod]
		public void GetDateTimeAsString()
		{
			// Arrange
			var dt = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Local);

			// Act
			var intDt = dt.ConvertDateTimeToString();

			// Assert
			Assert.AreEqual("2024-05-22 00:00:00Z", intDt);
		}
	}
}
