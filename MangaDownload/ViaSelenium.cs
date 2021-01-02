using System;
using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium;

namespace MangaDownload
{
    public static class ViaSelenium
    {
        public static void StartProcess(string url, string nomeDoManga, int capitulo, int volume, string originalPath, int volNumber)
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
                    int volCount = volNumber;

                    string path = Path.Combine(originalPath, nomeDoManga + "\\" + "Volume " + volCount);

                    Generic.CreateDirectory(path);

                    int pageNumber = 1;

                    for (int i = capitulo - 1; i < capUrl.Count; i++)
                    {
                        pageNumber = Generic.OpenVol(driver, capUrl[i], path, pageNumber);

                        if ((i + 1) % volume == 0)
                        {
                            volCount++;
                            pageNumber = 1;
                            path = Path.Combine(originalPath, nomeDoManga + "\\" + "Volume " + volCount);

                            if (i + 1 < capUrl.Count)
                                Generic.CreateDirectory(path);
                        }
                    }
                }
                else
                {
                    for (int i = capitulo - 1; i < capUrl.Count; i++)
                    {
                        string path = Path.Combine(originalPath, nomeDoManga + "\\" + capTitle[i]);

                        Generic.CreateDirectory(path);

                        Generic.file = capTitle[i] + " - Iniciando Download";

                        Generic.OpenCap(driver, capUrl[i], path);

                        File.AppendAllText(pathLog + "\\log.txt", capTitle[i] + " Baixado \r\n");
                        Generic.file = capTitle[i] + " - Download Terminado";
                    }
                }

                Generic.file = "Fim da Execução";
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

        public static void StartProcessH(string url, string cod, string originalPath)
        {
            string[] opt = { "headless" };
            WebDriver driver = new WebDriver(opt);

            try
            {
                driver.Url = url + cod;

                string nome = driver.WaitElement(By.XPath("//*[@id='info']/h1")).Text;

                nome = nome.NormalizeText();

                driver.WaitElement(By.XPath("//*[@id='cover']/a")).Click();

                string pathLog = Path.Combine(originalPath, nome);
                string path = Path.Combine(originalPath, nome);

                Generic.CreateDirectory(path);

                int contador = 1;
                Generic.file = "Iniciando Download";

                while (true)
                {
                    
                    url = driver.WaitElement(By.XPath("//*[@id='image-container']/a/img")).GetAttribute("src");

                    string newPath = path + "\\" + contador.ToString("00") + ".jpg";

                    Generic.DownloadFile(url, newPath);

                    Generic.file = "Página "+ contador.ToString("00") + " baixada";

                    if (driver.ElementIsVisible(By.XPath("//*[@class='reader-pagination']/*[@class='next']"), out var botao))
                        botao.Click();
                    else
                        break;

                    contador++;
                }
                Generic.file = "Download Concluído";
                Generic.file = "Fim da Execução";
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
    }
}
