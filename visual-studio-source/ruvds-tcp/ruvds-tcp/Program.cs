using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace ruvds_tcp
{
    public sealed class Program
    {
        public static TcpListener listener;
        public static List<TcpClient> clients = new List<TcpClient>();

        public static string serverName = "UROD Engine";


        [MTAThread]
        public static void Main(string[] args)
        {
            HelpFileManager.Execute();
            SetArguments(out string ip, out ushort port);

            StartServer(ip, port);

            Task.Run(SearchClients);
            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Waiting for clients.");
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            while (true)
            {
                continue;
            }
        }

        private static void SetArguments(out string ip, out ushort port)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("══════════════════════════════════════════════════════════════════════\n" +
                "Started TCP server.\nEnter internal IPv4:");

            Console.ForegroundColor = ConsoleColor.Cyan;
            ip = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"══════════════════════════════════════════════════════════════════════\n" +
                $"ip = {ip}.\nEnter port:");

            Console.ForegroundColor = ConsoleColor.Cyan;
            ushort.TryParse(Console.ReadLine(), out  port);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"port = {port}\n");
        }
        private static void StartServer(in string ip, in ushort port)
        {
            try
            {
                listener = new TcpListener(IPAddress.Parse(ip), port);
            }
            catch
            {
                listener = new TcpListener(IPAddress.Any, port);
            }

            listener.Start();
            listener.Server.NoDelay = true;
            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
            Console.WriteLine($"Start TCP server. args: ip={listener.Server.LocalEndPoint}, port={port}");
        }

        private static Task SearchClients()
        {
            while (true)
            {
                TcpClient newClient = listener.AcceptTcpClient();
                clients.Add(newClient);

                newClient.NoDelay = true;
                Console.WriteLine($"New client has appeared: {newClient.Client.RemoteEndPoint}");
                SendMessage($"out Добро пожаловать на сервер \"{serverName}\"", newClient);

                Task.Run(()=> { ReadClient(newClient); });
            }
        }
        private static Task ReadClient(in TcpClient newClient)
        {
            try
            {
                while (true)
                {
                    // буфер, принимаемый от клиента
                    // его размер всегда равен 256
                    byte[] buffer = new byte[256];
                    int readed = newClient.GetStream().Read(buffer, 0, buffer.Length);

                    if (readed <= 0)
                    {
                        continue;
                    }

                    // обрезание буфера до фактического кол-ва используемых байт. пустая часть буфера будет отрезана.
                    // так 256 байт превратятся в 12, если клиент выдаст сообщение "hello,world!"
                    byte[] data = new byte[readed];
                    Buffer.BlockCopy(buffer, 0, data, 0, readed);

                    // вывод
                    string gettedMessage = Encoding.UTF8.GetString(data);

                    // обработка сообщения.
                    MessageHandler.Handle(HexNetMessage.Read(gettedMessage), newClient);
                }
            }
            catch (Exception e)
            {
                clients.Remove(newClient);
                Console.WriteLine(e.Message);
            }
            return Task.CompletedTask;
        }

        public static void SendMessage(in string message, in TcpClient client)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes($"{HexNetMessage.Write(message)}");

                client.GetStream().Write(buffer, 0, buffer.Length);
            }
            catch
            {
                return;
            }
        }
    }
}
