using Ardalis.GuardClauses;

namespace SaschasToolbox.Extensions
{

	/// <summary>
	/// Class StringExtensions.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Returns a salutation based on a given gender.
		/// </summary>
		/// <param name="gender">Gender</param>
		/// <returns>Herr oder Frau</returns>
		public static string GetSalutationText(this string gender)
		{
			Guard.Against.NullOrEmpty(gender);
			return gender switch
			{
				"Male" => "Herr",
				"Female" => "Frau",
				_ => ""
			};
		}

		/// <summary>
		/// Returns a integer based on a given gender.
		/// </summary>
		/// <param name="gender">Gender</param>
		/// <returns>Male = 1, Female = 2, Unknown = -1</returns>
		public static int ReturnGenderId(this string gender)
		{
			Guard.Against.NullOrEmpty(gender);
			return gender switch
			{
				"Male" => 1,
				"Female" => 2,
				_ => -1
			};
		}
	}
}
