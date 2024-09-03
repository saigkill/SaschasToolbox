using JetBrains.Annotations;

using Microsoft.Extensions.Logging;

using Moq;

using SasachasToolbox.Tests.Models;

using SaschasToolbox.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SasachasToolbox.Tests.Services
{

	[TestClass]
	[TestSubject(typeof(CsvService))]
	public class CsvServiceTest
	{
		private Mock<ILogger<CsvService>> _mockLogger;

		[TestInitialize]
		public void SetUp()
		{
			_mockLogger = new Mock<ILogger<CsvService>>();
		}

		[TestMethod]
		public async Task Write_SendsDataToFile()
		{
			var targetName = "test.csv";
			var sampleList = new List<Foo>
			{
				new Foo { Id = 1, Name = "Test1"},
				new Foo { Id = 2, Name = "Test2"}
			};

			// Arrange
			var service = new CsvService(_mockLogger.Object);

			// Act
			await service.WriteAsync(sampleList, targetName, ",");

			// Assert
			_mockLogger.Verify(m => m.Log(
				LogLevel.Debug,
				It.IsAny<EventId>(),
				It.IsAny<It.IsAnyType>(),
				It.IsAny<Exception>(),
				It.IsAny<Func<It.IsAnyType, Exception, string>>()));
		}

		[TestMethod]
		public async Task Write_ThrowsException_OnEmptyList()
		{
			var targetName = "test.csv";
			var sampleList = new List<string>();

			// Arrange
			var service = new CsvService(_mockLogger.Object);

			// Act / Assert
			await Assert.ThrowsExceptionAsync<ArgumentException>(() => service.WriteAsync(sampleList, targetName, ","));
		}

		[TestMethod]
		public async Task Write_ThrowsException_OnEmptyTarget()
		{
			var sampleList = new List<string> { "test1", "test2" };

			// Arrange
			var service = new CsvService(_mockLogger.Object);

			// Act / Assert
			await Assert.ThrowsExceptionAsync<ArgumentException>(() => service.WriteAsync(sampleList, string.Empty, ","));
		}

		[TestMethod]
		public async Task Write_ThrowsException_OnNullList()
		{
			var targetName = "test.csv";

			// Arrange
			var service = new CsvService(_mockLogger.Object);

			// Act / Assert
			// ReSharper disable once AssignNullToNotNullAttribute
			await Assert.ThrowsExceptionAsync<ArgumentNullException>(
				() => service.WriteAsync<List<string>>(null, targetName, ","));
		}

		[TestMethod]
		public async Task Write_ThrowsException_OnNullTarget()
		{
			var sampleList = new List<string> { "test1", "test2" };

			// Arrange
			var service = new CsvService(_mockLogger.Object);

			// Act / Assert
			// ReSharper disable once AssignNullToNotNullAttribute
			await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => service.WriteAsync(sampleList, null, ","));
		}
	}
}
