using System;
using System.IO;

namespace ruvds_tcp
{
    /// <summary> Класс, определеющий файл справки в системе, который находится в директории сервер-приложения </summary>
    public static class HelpFileManager
    {
        /// <summary> Создать файл справки, к которой клиенты будут обращаться, используя команду help </summary>
        public static void Execute()
        {
            if (!Directory.Exists($"{Environment.CurrentDirectory}/Help"))
            {
                Directory.CreateDirectory($"{Environment.CurrentDirectory}/Help");
            }

            if (!File.Exists($"{Environment.CurrentDirectory}/Help/Help.txt"))
            {
                File.WriteAllText($"{Environment.CurrentDirectory}/Help/Help.txt", 
                    "Commands:\n" +
                    "1. say <message>\n" +
                    "2. help\n");
            }
        }
    }
}
