using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MangaController
{
    using Manga = MangaDownload.DownloadManga;

    public class Controller
    {
        public static List<string> logs = new List<string>();

        public static void GetLog()
        {
            string l = "Processo Iniciado";
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

            Manga.file = string.Empty;
        }

        public static void Download(string url, string manga, bool navegador, int capitulo)
        {
            try
            {
                if (navegador)
                    new Task(() => { Manga.ViaSelenium(url, manga, capitulo); }).Start();
                else
                    new Task(() => { Manga.ViaCrawler(url, manga, capitulo); }).Start();

                new Task(GetLog).Start();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
