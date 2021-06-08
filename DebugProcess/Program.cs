//Use this small Console app to debug apps that need to send Arguments to be able to see the arguments and write them out to a file in the same location as this apps exe file.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Globalization;
using System.Media;
using System.Diagnostics;

namespace DebugProcess
{
    class Program
    {

        static string GetTopBorder()
        {
            string[] outborder = { ".--.      .-'.      .--.      .--.      .--.      .--.      .`-.      .--.",
                                   ":::::.\\::::::::.\\::::::::.\\::::::::.\\::::::::.\\::::::::.\\::::::::.\\::::::::.\\",
                                   "'      `--'      `.-'      `--'      `--'      `--'      `-.'      `--'      `"  };
           return String.Join(Environment.NewLine, outborder);
        }
        
        static string GetOutText(string[] args )
        {
            var appExePath = Process.GetCurrentProcess().MainModule.FileName;
            var appdir = Path.GetDirectoryName(appExePath);

            DateTime dt = DateTime.Now;
            List<string> stringar = new List<string>();
            stringar.Add(GetTopBorder());
            stringar.Add(Environment.NewLine);
            stringar.Add(String.Format("EXE : {0}", appExePath));
            stringar.Add(String.Format("Dir : {0}", Path.GetDirectoryName( appExePath)));
            stringar.Add(String.Format("Output : {0}", Path.Combine(appdir, String.Format("DebugProcess_{0}.txt", dt.ToString("s", DateTimeFormatInfo.InvariantInfo).Replace(":", "")))));
            stringar.Add(String.Format("DateTime : {0}", dt.ToString("F", DateTimeFormatInfo.InvariantInfo)));
            var countt = 0;
            foreach(var arg in args)
            {
                var stringg = String.Format("Argument {0} : {1}", countt, arg);
                stringar.Add(stringg);
                    countt += 1;
            }
            stringar.Add(Environment.NewLine);
            stringar.Add(GetTopBorder());
            return String.Join(Environment.NewLine, stringar);
        }

        static int Main(string[] args)
        {
            DateTime dt = DateTime.Now;
            var appExePath = Process.GetCurrentProcess().MainModule.FileName;
            var appdir = Path.GetDirectoryName(appExePath);
            var OutText = GetOutText(args);
            File.WriteAllText(Path.Combine(appdir, String.Format("DebugProcess_{0}.txt", dt.ToString("s", DateTimeFormatInfo.InvariantInfo).Replace(":",""))), OutText);

            Console.WriteLine(GetOutText(args));
            SoundPlayer typewriter = new SoundPlayer();
            typewriter.SoundLocation = appdir + "/Success_Violin.wav";
            typewriter.PlaySync();
            return 1;
        }
    }
}
