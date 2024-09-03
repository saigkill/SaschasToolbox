using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaschasToolbox.Patterns
{

	/// <summary>
	/// This Interface is used to define the pipeline.
	/// More Details on: https://medium.com/@martinstm/pipeline-pattern-c-e01e2dd7238c
	/// </summary>
	/// <typeparam name="T">Given Type</typeparam>
	public interface IPipeline<T>
	{
		/// <summary>
		/// Gets or sets the name of the Pipleine.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }

		/// <summary>
		/// Gets the steps.
		/// </summary>
		/// <value>The steps.</value>
		public IReadOnlyCollection<IStep<T>> Steps { get; }

		/// <summary>
		/// Adds the step.
		/// </summary>
		/// <param name="step">The step.</param>
		void WithStep(IStep<T> step);

		/// <summary>
		/// Starts the asynchronous.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns>Task&lt;T&gt;.</returns>
		Task<T> StartAsync(T data);
	}
}
