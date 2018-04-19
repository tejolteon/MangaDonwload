using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace MangaSelenium
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string unionMangas = "http://unionmangas.cc";
            string manga = "Ningyou no Kuni: APOSIMZ";
            Inicio(unionMangas,manga);
        }

        public static string file = null;

        public static void Inicio(string url, string nomeDoManga)
        {
            IWebDriver driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl(url);
                Console.WriteLine("Navegando até " + url);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                driver.FindElement(By.XPath("//*[@id=\"pesquisa\"]")).SendKeys(nomeDoManga);
                driver.FindElement(By.XPath("//*[@id=\"pesquisa\"]")).Submit();
                Console.WriteLine("Pesquisando Mangá " + nomeDoManga);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div[6]/div[1]/a[2]")).Click();
                Console.WriteLine("Abrindo Mangá");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                nomeDoManga = driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div[1]/div/h2")).Text;

                string[] source = nomeDoManga.Split(new char[] { '.', '?', '!', ';', ':', ',', '"', '<', '>', '=', '-', '\n', '&' }, StringSplitOptions.RemoveEmptyEntries);

                string firstElem = source.First();
                string restOfArray = string.Join(" ", source.Skip(1));
                nomeDoManga = firstElem + restOfArray;

                var capitulos = driver.FindElements(By.XPath("//*[@class='row lancamento-linha']/div[1]/a"));

                List<string> capUrl = new List<string>();
                List<string> capTitle = new List<string>();

                foreach (var item in capitulos)
                {
                    capUrl.Add(item.GetAttribute("href"));
                    capTitle.Add(item.Text);
                }

                string pathLog = Path.Combine(Environment.CurrentDirectory, @"Data\", nomeDoManga);

                for (int i = capUrl.Count - 1; i >= 0; i--)
                {
                    string path = Path.Combine(Environment.CurrentDirectory, @"Data\", nomeDoManga + "\\" + capTitle[i]);
                    

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    
                    OpenCap(driver, capUrl[i], nomeDoManga, path);
                    
                    
                    File.AppendAllText(pathLog + "\\log.txt", capTitle[i] + " Baixado \r\n");
                    Console.WriteLine(capTitle[i] + " Baixado");
                }
                Console.WriteLine("Fim da Execução");
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: " + e.Message);
                throw e;
            }
            finally
            {
                driver.Dispose();
            }
        }

        static string GetCookieHeaderString(IWebDriver driver)
        {
            var cookies = driver.Manage().Cookies.AllCookies;
            return string.Join("; ", cookies.Select(c => string.Format("{0}={1}", c.Name, c.Value)));
        }

        public static void DownloadFile(this IWebDriver driver, string sourceURL, string destinationPathAndName)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.Cookie] = GetCookieHeaderString(driver);
                wc.DownloadFile(sourceURL, destinationPathAndName);
            }
        }

        public static void OpenCap(this IWebDriver driver, string capUrl, string nomeDoManga, string path)
        {
            driver.Navigate().GoToUrl(capUrl);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            var images = driver.FindElements(By.XPath("//*[@id=\"leitor\"]/div[4]/div[4]/img"));
            List<string> imageUrl = new List<string>();

            foreach (var item in images)
            {
                imageUrl.Add(item.GetAttribute("src"));

            }
            string newPath;
            for (int i = 0; i < imageUrl.Count; i++)
            {
                newPath = path + "\\" + (i + 1) + ".jpg";
                DownloadFile(driver, imageUrl[i], newPath);
            } 
        }
    }
}
