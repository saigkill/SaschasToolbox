using JetBrains.Annotations;

using SaschasToolbox.Converter;

using System;

namespace SasachasToolbox.Tests.Converter
{

	[TestClass]
	[TestSubject(typeof(DateTimeConverter))]
	public class DateTimeConverterTest
	{

		[TestMethod]
		public void SplitDateByMode()
		{
			// Arrange
			var dt = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Local);

			// Act
			var year = DateTimeConverter.SplitDateByMode(dt, DateTimeConverter.DateTimeModes.Year);
			var month = DateTimeConverter.SplitDateByMode(dt, DateTimeConverter.DateTimeModes.Month);
			var day = DateTimeConverter.SplitDateByMode(dt, DateTimeConverter.DateTimeModes.Day);

			// Assert
			Assert.AreEqual(2024, year);
			Assert.AreEqual(5, month);
			Assert.AreEqual(22, day);
		}
	}
}
