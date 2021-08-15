using System;
using System.IO;
using System.Text;

namespace ConsoleClient
{
	internal sealed class Loger
	{
		private const string FILENAME = "logs.txt";

		public static void WriteException(Exception exception)
		{
			var sb = new StringBuilder();

			sb.AppendLine($"Message: {exception}");
			sb.AppendLine($"InnerException: {exception.InnerException}");
			sb.AppendLine($"StackTrace: {exception.StackTrace}");
			sb.AppendLine($"--------------{DateTime.Now}--------------");

			File.AppendAllText(FILENAME, sb.ToString());
		}
	}
}
