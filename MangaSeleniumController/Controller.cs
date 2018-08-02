using System.Collections.Generic;
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
                if (l != Manga.file && Manga.file != null)
                {
                    l = Manga.file;
                    logs.Add(l);
                }

                if (l == "Fim da Execução")
                    break;
            }

        }

        public static void Download(string x, string y, bool n)
        {
            if (n)
                new Task(() => { Manga.ViaSelenium(x, y); }).Start();
            else
                new Task(() => { Manga.ViaCrawler(x, y); }).Start();

            new Task(GetLog).Start();
        }
    }
}
