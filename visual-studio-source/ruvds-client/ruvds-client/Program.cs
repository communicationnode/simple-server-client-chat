using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ruvds_client;

namespace ruvds_client
{
    public class Program
    {
        public static TcpClient client;
        public static void Main(string[] args)
        {
            client = new TcpClient();
            Console.WriteLine("Enter IpAddress");
            try
            {
                client.Connect(IPAddress.Parse(Console.ReadLine()), 26950);
            }
            catch
            {
                client.Connect(IPAddress.Parse("127.0.0.1"), 26950);
            }

            client.NoDelay = true;

            Task.Run(ReadClient);
        
            while (true)
            {
                SendMessage(Console.ReadLine());
            }           
        }
        private static Task ReadClient()
        {
            try
            {
                while (true)
                {
                    // буфер, принимаемый от клиента
                    // его размер всегда равен 256
                    byte[] buffer = new byte[256];
                    int readed = client.GetStream().Read(buffer, 0, buffer.Length);

                    // обрезание буфера до фактического кол-ва используемых байт. пустая часть буфера будет отрезана.
                    // так 256 байт превратятся в 12, если клиент выдаст сообщение "hello,world!"
                    byte[] data = new byte[readed];
                    Buffer.BlockCopy(buffer, 0, data, 0, readed);

                    // вывод
                    string gettedMessage = Encoding.UTF8.GetString(data);

                    MessageHandler.Handle(HexNetMessage.Read(gettedMessage));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Task.CompletedTask;
        }
        public static void SendMessage(in string message)
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
