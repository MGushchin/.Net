using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentHabrPageTests
{
    public class Logging
    {
        public static void Log(string logText)
        {
            Console.WriteLine(logText);
            using (StreamWriter writer = new StreamWriter(TestSettings.LogPath, true))
            {
                writer.WriteLine(logText);
            }
        }
    }
}
