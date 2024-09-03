using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaschasToolbox.Patterns
{

	/// <summary>
	/// Class Pipeline. Used to define the pipeline.
	/// More on https://medium.com/@martinstm/pipeline-pattern-c-e01e2dd7238c
	/// </summary>
	/// <typeparam name="T">Given Type</typeparam>
	public class Pipeline<T> : IPipeline<T> where T : class
	{
		/// <summary>
		/// The steps as List.
		/// </summary>
		private readonly List<IStep<T>> _steps = new List<IStep<T>>();

		/// <summary>
		/// Gets or sets the name of the Pipleine.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }

		/// <summary>
		/// Gets the steps.
		/// </summary>
		/// <value>The steps.</value>
		public IReadOnlyCollection<IStep<T>> Steps => _steps;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<Pipeline<T>> _logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="Pipeline{T}"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="logger">Instance logger</param>
		public Pipeline(string name, ILogger<Pipeline<T>> logger)
		{
			Name = name;
			_logger = logger;
		}

		/// <summary>
		/// Adds the step.
		/// </summary>
		/// <param name="step">The step.</param>
		public void WithStep(IStep<T> step)
		{
			_steps.Add(step);
		}

		/// <summary>
		/// Start as an asynchronous operation.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns>A Task&lt;T&gt; representing the asynchronous operation.</returns>
		public async Task<T> StartAsync(T data)
		{
			T result = data;
			foreach (var step in Steps)
			{
				try
				{
					result = await step.ExecuteAsync(result).ConfigureAwait(false);
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error in pipeline {Name} at step {Step}", Name, step.GetType().Name);
					throw;
				}
			}

			return result;
		}
	}
}
