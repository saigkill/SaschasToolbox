using System.IO;

namespace SaschasToolbox.Generators
{

	/// <summary>
	/// Ein paar Methoden um temporäre Dateien zu erstellen.
	/// </summary>
	public static class TempTools
	{
		/// <summary>
		/// Erstellt einen temporären Ordner und gibt den Pfad zurück.
		/// </summary>
		/// <returns>Pfad zum temporären Ordner.</returns>
		public static string GetTemporaryDirectory()
		{
			string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

			if (Directory.Exists(tempDirectory))
			{
				return GetTemporaryDirectory();
			}
			else
			{
				Directory.CreateDirectory(tempDirectory);
				return tempDirectory;
			}
		}
	}
}
