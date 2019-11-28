using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace MangaDownload
{
    class ViaCrawler
    {
        public static void StartProcess(string url, string nomeDoManga, int capitulo, int volume, string originalPath, int volNumber)
        {
            try
            {
                nomeDoManga = nomeDoManga.RemoveSpecialChar();
                nomeDoManga = nomeDoManga.Replace(" ", "-").ToLower();
                HtmlWeb get = new HtmlWeb();
                HtmlDocument page = get.Load(url + nomeDoManga);

                nomeDoManga = page.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div[1]/div[1]/div/h2").InnerText;

                nomeDoManga = nomeDoManga.NormalizeText();

                var chapters = page.DocumentNode.SelectNodes("//*[@class='row lancamento-linha']/div[1]/a");

                List<string> capUrl = new List<string>();
                List<string> capTitle = new List<string>();

                foreach (var item in chapters)
                {
                    capUrl.Insert(0, item.Attributes["href"].Value);
                    capTitle.Insert(0, item.InnerText);
                }

                string pathLog = Path.Combine(originalPath, nomeDoManga);

                if (volume > 0)
                {
                    int volCount = volNumber;

                    string path = Path.Combine(originalPath, nomeDoManga + "\\" + "Volume " + volCount);

                    Generic.CreateDirectory(path);

                    int pageNumber = 1;

                    for (int i = 0; i < capUrl.Count - capitulo; i++)
                    {
                        pageNumber = Generic.OpenVol(capUrl[i], path, pageNumber);

                        if ((i + 1) % volume == 0)
                        {
                            volCount++;
                            pageNumber = 1;
                            path = Path.Combine(originalPath, nomeDoManga + "\\" + "Volume " + volCount);

                            Generic.CreateDirectory(path);
                        }
                    }
                }

                for (int i = 0; i <= capUrl.Count; i++)
                {
                    string path = Path.Combine(originalPath, nomeDoManga + "\\" + capTitle[i]);

                    Generic.CreateDirectory(path);

                    Generic.file = capTitle[i] + " - Iniciando Download";

                    Generic.OpenCap(capUrl[i], path);

                    File.AppendAllText(pathLog + "\\log.txt", capTitle[i] + " Baixado \r\n");
                    Generic.file = capTitle[i] + " - Download Terminado";
                }

                Generic.file = "Fim da Execução";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void StartProcessH(string url, string nomeDoManga, int capitulo, int volume, string originalPath, int volNumber)
        {
            
        }
    }
}
