using System;
using System.Net.Sockets;
using ruvds_tcp.Commands;
namespace ruvds_tcp
{
    /// <summary> Читает входящий текст и пытается на его основе подобрать команду </summary>
    public static class MessageHandler
    {
        /// <summary> Подобрать команду на основе входящего сообщения </summary>
        public static void Handle(in string message, in TcpClient sender)
        {
            string[] args = message.Split(' ');
            switch (args[0])
            {
                case "help":
                    new Help(sender);
                    break;
                case "say":
                    new Say(args, sender);
                    break;
                default:
                    Console.WriteLine($"({sender.Client.RemoteEndPoint}) non signed command: {message}");
                    break;
            }
        }
    }
}
