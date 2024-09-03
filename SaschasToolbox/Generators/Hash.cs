using Ardalis.GuardClauses;

using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace SaschasToolbox.Generators
{

	/// <summary>
	/// Some methods for computing and decoding Hash.
	/// </summary>
	public static class Hash
	{
		/// <summary>
		/// Computes the SHA256 hash for the input string.
		/// </summary>
		/// <param name="inputString">The string to be hashed.</param>
		/// <returns>The computed hash as a byte array.</returns>
		/// <exception cref="TargetInvocationException">On the .NET Framework 4.6.1 and earlier versions only: The algorithm was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		/// <exception cref="ObjectDisposedException">The object has already been disposed.</exception>
		/// <exception cref="EncoderFallbackException">A fallback occurred (for more information, see Character Encoding in .NET)
		///  -and-
		///  <see cref="EncoderFallback" /> is set to <see cref="EncoderExceptionFallback" />.</exception>
		private static byte[] GetHash(string inputString)
		{
			Guard.Against.NullOrEmpty(inputString);
			using HashAlgorithm algorithm = SHA256.Create();
			return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
		}

		/// <summary>
		/// Computes the SHA256 hash for the input string and returns it as a hexadecimal string.
		/// </summary>
		/// <param name="inputString">The string to be hashed.</param>
		/// <returns>The computed hash as a hexadecimal string.</returns>
		/// <exception cref="TargetInvocationException">On the .NET Framework 4.6.1 and earlier versions only: The algorithm was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		/// <exception cref="EncoderFallbackException">A fallback occurred (for more information, see Character Encoding in .NET)
		///  -and-
		///  <see cref="EncoderFallback" /> is set to <see cref="EncoderExceptionFallback" />.</exception>
		/// <exception cref="ObjectDisposedException">The object has already been disposed.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		public static string GetHashString(string inputString)
		{
			Guard.Against.NullOrEmpty(inputString);
			var sb = new StringBuilder();
			foreach (var b in GetHash(inputString))
				sb.Append(b.ToString("X2"));

			return sb.ToString();
		}
	}
}
