using System.Net.Sockets;

namespace ruvds_tcp.Commands
{
    public struct Say
    {
        public Say(in string[] args, in TcpClient sender)
        {
            string message = "";

            if (args.Length > 1)
                for (ushort i = 1; i < args.Length; i++)
                {
                    message += args[i] + " ";
                }

            Program.clients.ForEach((sender) => 
            {
                Program.SendMessage("out "+message, sender);
            });
        }
    }
}
