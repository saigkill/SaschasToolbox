using JetBrains.Annotations;

using SaschasToolbox.Patterns;

namespace SasachasToolbox.Tests.Checker
{

	[TestClass]
	[TestSubject(typeof(Result))]
	public class Result
	{
		public Result<string> GetUserNameById(int userId)
		{
			if (userId <= 0)
			{
				return Result<string>.Failure("Invalid user ID");
			}

			// We simulate obtaining a username
			string userName = "John Doe"; // This would be the actual logic to get the name

			return Result<string>.Success(userName);
		}

		[TestMethod]
		public void GetUserNameByIdTest()
		{
			// Arrange
			int userId = 1;
			Result<string> result;

			// Act
			result = GetUserNameById(userId);

			// Assert
			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual("John Doe", result.Value);
		}

		[TestMethod]
		public void GetUserNameByIdTest_InvalidId()
		{
			// Arrange
			int userId = 0;
			Result<string> result;

			// Act
			result = GetUserNameById(userId);

			// Assert
			Assert.IsFalse(result.IsSuccess);
			Assert.AreEqual("Invalid user ID", result.ErrorMessage);
		}
	}
}
