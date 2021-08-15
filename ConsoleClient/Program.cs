using System;
using System.IO;
using System.Net.Sockets;

namespace ConsoleClient
{
	internal sealed class Program
	{
		private static void Main(string[] args)
		{
			Console.Title = "Client";
			Console.Write($"Write hostname to connect: ");

			var hostname = Console.ReadLine();

			Console.Write($"Write port to connect: ");

			var portString = Console.ReadLine();

			Console.WriteLine(string.Empty);

			if (!string.IsNullOrWhiteSpace(hostname))
			{
				if (!string.IsNullOrWhiteSpace(portString) && int.TryParse(portString, out var port))
				{
					try
					{
						while (true)
						{
							using var tcpClient = new TcpClient();

							Console.WriteLine($"Connecting to {hostname}:{port}...");

							tcpClient.Connect(hostname, port);

							if (tcpClient.Connected)
							{
								Console.WriteLine($"Connected");
								Console.WriteLine(string.Empty);
								Console.Write($"Your message: ");

								var clientMessage = Console.ReadLine();

								using var networkStream = tcpClient.GetStream();
								using var streamWriter = new BinaryWriter(networkStream);
								using var streamReader = new BinaryReader(networkStream);

								streamWriter.Write(clientMessage);
								streamWriter.Flush();

								var serverMessage = streamReader.ReadString();

								Console.WriteLine($"Server message: {serverMessage}");
								Console.WriteLine(string.Empty);
							}
							else
								Console.WriteLine($"Not connected");
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
						Loger.WriteException(ex);
					}
				}
				else
					Console.WriteLine($"Empty or incorrect port");
			}
			else
				Console.WriteLine($"Empty hostname");

			Console.ReadKey();
		}
	}
}