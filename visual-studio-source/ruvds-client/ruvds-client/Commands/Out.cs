using System;

namespace ruvds_client.Commands
{
    public struct Out
    {
        public Out(string[] args)
        {
            string message = "";

            if (args.Length > 1)
                for (ushort i = 1; i < args.Length; i++)
                {
                    message += args[i] + " ";
                }

            Console.WriteLine(message);
        }
    }
}
