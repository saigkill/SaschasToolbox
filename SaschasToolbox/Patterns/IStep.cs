using System.Threading.Tasks;

namespace SaschasToolbox.Patterns
{

	/// <summary>
	/// Interface IStep. It is used to define the step in the pipeline.
	/// </summary>
	/// <typeparam name="T">Given Type</typeparam>
	public interface IStep<T>
	{
		/// <summary>
		/// Executes the asynchronous.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns>Task&lt;T&gt;.</returns>
		Task<T> ExecuteAsync(T data);
	}
}
