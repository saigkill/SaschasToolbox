using Ardalis.GuardClauses;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace SaschasToolbox.Generators
{

	/// <summary>
	/// Class for generating DataTables from a List of Model objects.
	/// </summary>
	/// <typeparam name="T">Modeltyp</typeparam>
	public class DataTableGenerator<T>
	{
		private readonly ILogger<DataTableGenerator<T>> _logger;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="logger">Klassenlogger</param>
		public DataTableGenerator(ILogger<DataTableGenerator<T>> logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// Generates a DataTable from a List of Model objects.
		/// </summary>
		/// <param name="modelList">List model</param>
		/// <param name="withId">Should a ID Field generated.</param>
		/// <returns>DataTable</returns>
		public DataTable GenerateDataTableFromModelList(IList<T> modelList, bool withId)
		{
			Guard.Against.Null(modelList);

			DataTable dataTable = new DataTable();

			// Creating the columns of the DataTable based on the properties of the model
			var properties = typeof(T).GetProperties();
			foreach (var property in properties)
			{
				AddPropertyToDataTableColumn(withId, property, dataTable);
			}

			// Filling the DataTable with the model data
			foreach (var model in modelList)
			{
				var row = dataTable.NewRow();
				foreach (var property in properties)
				{
					AddValueToDataRow(withId, model, property, row);
				}

				dataTable.Rows.Add(row);
			}

			_logger.LogInformation("Datatable created.");
			return dataTable;
		}

		/// <summary>
		/// Adds a property to a DataTable column.
		/// </summary>
		/// <param name="withId">Should a ID field generated or not.</param>
		/// <param name="property">Propertyname</param>
		/// <param name="dataTable">DataTable.</param>
		// ReSharper disable once FlagArgument
		private void AddPropertyToDataTableColumn(bool withId, PropertyInfo property, DataTable dataTable)
		{
			Guard.Against.Null(property);
			Guard.Against.Null(dataTable);
			try
			{
				if (withId || (!property.Name.Equals("Id")))
				{
					dataTable.Columns.Add(property.Name,
						Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
				}
			}
#pragma warning disable S2139
			catch (Exception ex)
#pragma warning restore S2139
			{
				_logger.LogError(ex, "Error while working with {PropertyName}, Error: {ExMessage}", property.Name, ex.Message);
				throw;
			}
		}

		/// <summary>
		/// Adds a value to a DataRow.
		/// </summary>
		/// <param name="withId">Should ID Field used or not.</param>
		/// <param name="model">Model</param>
		/// <param name="property">Property.</param>
		/// <param name="row">DataRow.</param>
		// ReSharper disable once FlagArgument
		private void AddValueToDataRow(bool withId, T model, PropertyInfo property, DataRow row)
		{
			Guard.Against.Null(property);
			Guard.Against.Null(model);
			Guard.Against.Null(row);
			try
			{
				if (withId || (!property.Name.Equals("Id")))
				{
					row[property.Name] = (property.GetValue(model) is null) ? DBNull.Value : property.GetValue(model);
				}
			}
#pragma warning disable S2139
			catch (Exception ex)
#pragma warning restore S2139
			{
				_logger.LogError(ex, "Error while working with {PropertyName}, Error: {ExMessage}", property.Name, ex.Message);
				throw;
			}
		}
	}
}
