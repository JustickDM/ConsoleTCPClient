using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ConsoleClient
{
	internal sealed class Program
	{
		const string IP = "127.0.0.1";
		const int PORT = 15000;

		static void Main(string[] args)
		{
			Console.Title = "Client";

			try
			{
				while (true)
				{
					using (var tcpClient = new TcpClient())
					{
						Console.WriteLine($"Connecting to {IP}:{PORT}...");

						tcpClient.Connect(new IPEndPoint(IPAddress.Parse(IP), PORT));

						if (tcpClient.Connected)
						{
							Console.WriteLine($"Connected");
							Console.WriteLine(string.Empty);

							Console.Write($"Your message: ");

							var clientMessage = Console.ReadLine();

							using (var networkStream = tcpClient.GetStream())
							{
								using (var streamWriter = new StreamWriter(networkStream) { AutoFlush = true })
								{
									streamWriter.WriteLine(clientMessage);

									var serverMessage = string.Empty;

									using (var streamReader = new StreamReader(networkStream))
									{
										serverMessage = streamReader.ReadLine();
									}

									Console.WriteLine($"Server message: {serverMessage}");
								}
							}

							Console.WriteLine(string.Empty);
						}
						else
							Console.WriteLine($"Not connected");
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				File.WriteAllText("logs.bin", $"Message: {ex.Message}. StackTrace: {ex.StackTrace}");
			}

			Console.ReadKey();
		}
	}
}