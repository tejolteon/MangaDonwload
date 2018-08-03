﻿using System;
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

            Manga.file = string.Empty;
        }

        public static void Download(string x, string y, bool n, int c)
        {
            try
            {
                if (n)
                    new Task(() => { Manga.ViaSelenium(x, y, c); }).Start();
                else
                    new Task(() => { Manga.ViaCrawler(x, y, c); }).Start();

                new Task(GetLog).Start();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
