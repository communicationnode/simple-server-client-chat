using System;
using System.Text;

namespace ruvds_tcp
{
    /// <summary> Преобразует текст в Hex-Текст и наоборот </summary>
    public static class HexNetMessage
    {

        /// <summary> Преобразовать текст в Hex-Текст </summary>
        public static string Write(in string message)
        {
            //create hex-string
            byte[] hexed = Encoding.UTF8.GetBytes(message);
            return Convert.ToHexString(hexed);
        }

        /// <summary> Преобразовать Hex-Текст в текст </summary>
        public static string Read(in string message)
        {
            //create default-string
            byte[] unhexed = Convert.FromHexString(message);
            return Encoding.UTF8.GetString(unhexed);
        }
    }
}
