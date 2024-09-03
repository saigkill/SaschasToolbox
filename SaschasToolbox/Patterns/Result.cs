using Ardalis.GuardClauses;

namespace SaschasToolbox.Patterns
{

	/// <summary>
	/// Base class for Result pattern.
	/// </summary>
	public class Result
	{
		/// <summary>
		/// Gets a value indicating whether this instance is success.
		/// </summary>
		/// <value><c>true</c> if this instance is success; otherwise, <c>false</c>.</value>
		public bool IsSuccess { get; }

		/// <summary>
		/// Gets the error message.
		/// </summary>
		/// <value>The error message.</value>
		public string ErrorMessage { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Result"/> class.
		/// </summary>
		/// <param name="isSuccess">if set to <c>true</c> [is success].</param>
		/// <param name="errorMessage">The error message.</param>
		protected Result(bool isSuccess, string errorMessage)
		{
			IsSuccess = isSuccess;
			ErrorMessage = errorMessage;
		}

		/// <summary>
		/// Successes this instance.
		/// </summary>
		/// <returns>Result.</returns>
		public static Result Success()
		{
			return new Result(true, string.Empty);
		}

		/// <summary>
		/// Failures the specified error message.
		/// </summary>
		/// <param name="errorMessage">The error message.</param>
		/// <returns>Result.</returns>
		public static Result Failure(string errorMessage)
		{
			Guard.Against.NullOrEmpty(errorMessage);
			return new Result(false, errorMessage);
		}
	}

	/// <summary>
	/// Class Result.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso href="https://medium.com/@davisaac8/an-alternative-to-try-catch-in-c-b0e5dfafa910" />
	public class Result<T> : Result
	{
		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>The value.</value>
		public T Value { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Result{T}"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="isSuccess">if set to <c>true</c> [is success].</param>
		/// <param name="errorMessage">The error message.</param>
		private Result(T value, bool isSuccess, string errorMessage)
			: base(isSuccess, errorMessage)
		{
			Value = value;
		}

		/// <summary>
		/// Successes the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>Result&lt;T&gt;.</returns>
		public static Result<T> Success(T value)
		{
			return new Result<T>(value, true, string.Empty);
		}

		/// <summary>
		/// Failures the specified error message.
		/// </summary>
		/// <param name="errorMessage">The error message.</param>
		/// <returns>Result&lt;T&gt;.</returns>
		public new static Result<T> Failure(string errorMessage)
		{
			Guard.Against.NullOrEmpty(errorMessage);
			return new Result<T>(default!, false, errorMessage);
		}
	}
}
