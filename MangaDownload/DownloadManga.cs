using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using OpenQA.Selenium;
using static System.Awaiter;

namespace MangaDownload
{
    public static class DownloadManga
    {
        public static string file;

        public static void ViaSelenium(string url, string nomeDoManga, int capitulo, int volume, string originalPath)
        {
            string[] opt = { "headless" };
            WebDriver driver = new WebDriver(opt);
            nomeDoManga = nomeDoManga.NormalizeFirstText();
            try
            {
                driver.Url = url;

                var searchBox = driver.WaitElement(By.XPath("//*[@id=\"pesquisa\"]"));
                searchBox.SendKeys(nomeDoManga);
                searchBox.Submit();

                driver.WaitElement(By.XPath("/html/body/div[1]/div/div[1]/div[6]/div[1]/a[2]")).Click();

                nomeDoManga = driver.WaitElement(By.XPath("/html/body/div[1]/div/div[1]/div[1]/div/h2")).Text;

                nomeDoManga = nomeDoManga.NormalizeText();

                var capitulos = driver.WaitElements(By.XPath("//*[@class='row lancamento-linha']/div[1]/a"));

                List<string> capUrl = new List<string>();
                List<string> capTitle = new List<string>();

                foreach (var item in capitulos)
                {
                    capUrl.Insert(0, item.GetAttribute("href"));
                    capTitle.Insert(0, item.Text);
                }

                string pathLog = Path.Combine(originalPath, nomeDoManga);

                if (volume > 0)
                {
                    int volCount = 1;

                    string path = Path.Combine(originalPath, nomeDoManga + "\\" + "Volume " + volCount);

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    int pageNumber = 1;

                    for (int i = 0; i < capUrl.Count - capitulo; i++)
                    {
                        pageNumber = OpenVol(driver, capUrl[i], path, pageNumber);

                        if ((i + 1) % volume == 0)
                        {
                            volCount++;
                            pageNumber = 1;
                            path = Path.Combine(originalPath, nomeDoManga + "\\" + "Volume " + volCount);

                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(path);
                        }
                    }
                }

                for (int i = 0; i <= capUrl.Count - capitulo; i++)
                {
                    string path = Path.Combine(originalPath, nomeDoManga + "\\" + capTitle[i]);

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    file = capTitle[i] + " - Iniciando Download";

                    OpenCap(driver, capUrl[i], path);

                    File.AppendAllText(pathLog + "\\log.txt", capTitle[i] + " Baixado \r\n");
                    file = capTitle[i] + " - Download Terminado";
                }

                file = "Fim da Execução";
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                driver.Dispose();
            }
        }

        public static void ViaCrawler(string url, string nomeDoManga, int capitulo)
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
                    capUrl.Add(item.Attributes["href"].Value);
                    capTitle.Add(item.InnerText);
                }

                string pathLog = Path.Combine(Environment.CurrentDirectory, @"Data\", nomeDoManga);

                for (int i = capUrl.Count - capitulo; i >= 0; i--)
                {
                    string path = Path.Combine(Environment.CurrentDirectory, @"Data\", nomeDoManga + "\\" + capTitle[i]);

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    file = capTitle[i] + " - Iniciando Download";

                    OpenCap(capUrl[i], path);

                    File.AppendAllText(pathLog + "\\log.txt", capTitle[i] + " Baixado \r\n");
                    file = capTitle[i] + " - Download Terminado";
                }

                file = "Fim da Execução";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        static string GetCookieHeaderString(WebDriver driver)
        {
            var cookies = driver.Manage().Cookies.AllCookies;
            return string.Join("; ", cookies.Select(c => string.Format("{0}={1}", c.Name, c.Value)));
        }

        private static void DownloadFile(this WebDriver driver, string sourceURL, string destinationPathAndName)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.Cookie] = GetCookieHeaderString(driver);
                wc.DownloadFile(sourceURL, destinationPathAndName);
            }
        }

        public static void DownloadFile(string sourceURL, string destinationPathAndName)
        {
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(sourceURL, destinationPathAndName);
            }
        }

        private static void OpenCap(this WebDriver driver, string capUrl, string path)
        {
            driver.Url = capUrl;

            var images = driver.WaitElements(By.XPath("//*[@id=\"leitor\"]/div[4]/div[4]/img"));
            List<string> imageUrl = new List<string>();

            foreach (var item in images)
            {
                imageUrl.Add(item.GetAttribute("src"));
            }

            string newPath;

            for (int i = 0; i < imageUrl.Count; i++)
            {
                newPath = path + "\\" + (i + 1).ToString("00") + ".jpg";
                DownloadFile(driver, imageUrl[i], newPath);
            }
        }

        private static int OpenVol(this WebDriver driver, string capUrl, string path, int pageNumber)
        {
            driver.Url = capUrl;

            var images = driver.WaitElements(By.XPath("//*[@id=\"leitor\"]/div[4]/div[4]/img"));
            List<string> imageUrl = new List<string>();

            foreach (var item in images)
            {
                imageUrl.Add(item.GetAttribute("src"));
            }

            string newPath;

            for (int i = 0; i < imageUrl.Count; i++)
            {
                newPath = path + "\\" + pageNumber.ToString("000") + ".jpg";
                DownloadFile(driver, imageUrl[i], newPath);
                pageNumber++;
            }

            return pageNumber;
        }

        public static void OpenCap(string capUrl, string path)
        {
            var get = new HtmlWeb();
            var page = get.Load(capUrl);

            var images = page.DocumentNode.SelectNodes("//*[@id=\"leitor\"]/div[4]/div[4]/img");
            List<string> imageUrl = new List<string>();

            foreach (var item in images)
            {
                imageUrl.Add(item.Attributes["src"].Value);

            }

            string newPath;

            for (int i = 0; i < imageUrl.Count; i++)
            {
                newPath = path + "\\" + (i + 1).ToString("00") + ".jpg";
                DownloadFile(imageUrl[i], newPath);
            }
        }
    }
}
