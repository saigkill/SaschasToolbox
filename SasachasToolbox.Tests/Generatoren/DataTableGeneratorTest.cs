using JetBrains.Annotations;

using Microsoft.Extensions.Logging;

using Moq;

using SaschasToolbox.Generators;

using System;
using System.Collections.Generic;

namespace SasachasToolbox.Tests.Generatoren
{

	[TestClass]
	[TestSubject(typeof(DataTableGenerator<>))]
	public class DataTableGeneratorTest
	{
		private Mock<ILogger<DataTableGenerator<TestModel>>> _mockLogger;

		[TestInitialize]
		public void Initialize()
		{
			_mockLogger = new Mock<ILogger<DataTableGenerator<TestModel>>>();
		}

		[TestMethod]
		public void TestGenerateDataTableFromModelListWithId()
		{
			var modelList = new List<TestModel>
			{
				new TestModel() { Id = 1, Value = "Test 1" },
				new TestModel() { Id = 2, Value = "Test 2" }
			};

			var dtGenerator = new DataTableGenerator<TestModel>(_mockLogger.Object);
			var generatedDataTable = dtGenerator.GenerateDataTableFromModelList(modelList, true);

			foreach (var model in modelList)
			{
				Assert.AreEqual(model.Id, generatedDataTable.Rows[model.Id - 1]["Id"]);
				Assert.AreEqual(model.Value, generatedDataTable.Rows[model.Id - 1]["Value"]);
			}
			Assert.AreEqual(2, generatedDataTable.Columns.Count);
			Assert.AreEqual(2, generatedDataTable.Rows.Count);
			Assert.AreEqual("Id", generatedDataTable.Columns[0].ColumnName);
			Assert.AreEqual("Value", generatedDataTable.Columns[1].ColumnName);
		}

		[TestMethod]
		public void TestGenerateDataTableFromModelListWithoutId()
		{
			var modelList = new List<TestModel>
			{
				new TestModel() { Id = 1, Value = "Test 1" },
				new TestModel() { Id = 2, Value = "Test 2" }
			};

			var dtGenerator = new DataTableGenerator<TestModel>(_mockLogger.Object);
			var generatedDataTable = dtGenerator.GenerateDataTableFromModelList(modelList, false);

			Assert.AreEqual(1, generatedDataTable.Columns.Count);
			Assert.AreEqual(2, generatedDataTable.Rows.Count);
			Assert.AreEqual("Value", generatedDataTable.Columns[0].ColumnName);
		}

		[TestMethod]
		public void TestGenerateDataTableFromNullModelList()
		{
			var dtGenerator = new DataTableGenerator<TestModel>(_mockLogger.Object);
			// ReSharper disable once AssignNullToNotNullAttribute

			Assert.ThrowsException<ArgumentNullException>(() => dtGenerator.GenerateDataTableFromModelList(null, false));
		}

		public class TestModel
		{
			public int Id { get; set; }
			public string Value { get; set; }
		}
	}
}
