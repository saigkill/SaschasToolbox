using Ardalis.GuardClauses;

using System;
using System.IO;

namespace SaschasToolbox.Generators
{

	/// <summary>
	/// Generates a temporary file.
	/// </summary>
	public sealed class TemporaryFile : IDisposable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TemporaryFile"/> class.
		/// </summary>
		public TemporaryFile() : this(Path.GetTempPath()) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="TemporaryFile"/> class.
		/// </summary>
		/// <param name="directory">The directory.</param>
		public TemporaryFile(string directory)
		{
			Guard.Against.NullOrEmpty(directory);
			Create(Path.Combine(directory, Path.GetRandomFileName()));
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="TemporaryFile"/> class.
		/// </summary>
#pragma warning disable MA0055
		~TemporaryFile()
#pragma warning restore MA0055
		{
			Delete();
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Delete();
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Gets the file path.
		/// </summary>
		/// <value>The file path.</value>
		public string? FilePath { get; private set; }

		/// <summary>
		/// Creates the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		private void Create(string path)
		{
			Guard.Against.NullOrEmpty(path);
			FilePath = path;
			File.Create(FilePath);
		}

		/// <summary>
		/// Deletes this instance.
		/// </summary>
		private void Delete()
		{
			if (FilePath == null) return;
			File.Delete(FilePath);
			FilePath = null;
		}
	}
}
