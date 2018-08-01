using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaSeleniumController
{
    using Manga = MangaSelenium.DownloadManga;
    public class Controller
    {
        static void Main(string[] args)
        {
        }
        public static List<string> logs = new List<string>();
        public static void GetLog()
        {
            string l = "Começou";
            logs.Add(l);

            while (true)
            {
                if (l != Manga.file)
                    l = Manga.file;

                if (l != null)
                    logs.Add(l);

                if (l == "Acabou")
                    break;
            }

        }

        public static void Download(string x, string y)
        {
            new Task(() => { Manga.Inicio(x, y); }).Start();
            new Task(GetLog).Start();
        }

        public static List<string> ReadLogs()
        {
            return logs;
        }
    }
}
