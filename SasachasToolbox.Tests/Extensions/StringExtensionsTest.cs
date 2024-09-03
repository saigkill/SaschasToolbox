using JetBrains.Annotations;

using SaschasToolbox.Extensions;

namespace SasachasToolbox.Tests.Extensions
{

	[TestClass]
	[TestSubject(typeof(StringExtensions))]
	public class StringExtensionsTest
	{

		[TestMethod]
		public void GetSalutation()
		{
			// Arrange
			var test = "Male";

			// Act
			var result = test.GetSalutationText();

			// Assert
			Assert.AreEqual("Herr", result);
		}

		[TestMethod]
		public void GetGenderId()
		{
			// Arrange
			var test = "Male";

			// Act
			var result = test.ReturnGenderId();

			// Assert
			Assert.AreEqual(1, result);
		}
	}
}
