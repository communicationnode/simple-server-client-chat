using System;
using System.Text;

namespace ruvds_client
{
    public static class HexNetMessage
    {
        public static string Write(in string message)
        {
            //create hex-string
            byte[] hexed = Encoding.UTF8.GetBytes(message);
            return Convert.ToHexString(hexed);
        }

        public static string Read(in string message)
        {
            //create default-string
            byte[] unhexed = Convert.FromHexString(message);
            return Encoding.UTF8.GetString(unhexed);
        }
    }
}
