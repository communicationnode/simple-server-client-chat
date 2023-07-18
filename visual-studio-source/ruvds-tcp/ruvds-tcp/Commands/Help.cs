using System.Net.Sockets;
using System.IO;

namespace ruvds_tcp.Commands
{
    public struct Help
    {
        public Help(in TcpClient sender)
        {
            try
            {
                string help = File.ReadAllText($"{System.Environment.CurrentDirectory}/Help/Help.txt");
                Program.SendMessage("out "+help, sender);
            }
            catch
            {
                Program.SendMessage("out server doen't have \"Help.txt\" file", sender);
            }
        }
    }
}
