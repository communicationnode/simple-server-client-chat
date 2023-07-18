using System;
using ruvds_client.Commands;

namespace ruvds_client
{
    /// <summary> Читает входящий текст и пытается на его основе подобрать команду </summary>
    public static class MessageHandler
    {
        /// <summary> Подобрать команду на основе входящего сообщения </summary>
        public static void Handle(in string message)
        {
            string[] args = message.Split(' ');
            switch (args[0])
            {

                case "out":
                    new Out(args);
                    break;
                default:
                    Console.WriteLine($"({Program.client.Client.RemoteEndPoint}) non signed command: {message}");
                    break;
            }
        }
    }
}
