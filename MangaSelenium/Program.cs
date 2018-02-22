using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace MangaSelenium
{
    static class Program
    {
        static void Main(string[] args)
        {
            Inicio();
        }

        public static void Inicio()
        {
            IWebDriver driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl("http://www.google.com");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                driver.FindElement(By.XPath("//*[@id=\"gbw\"]/div/div/div[1]/div[2]/a")).Click();
                driver.FindElement(By.XPath("//*[@id=\"lst-ib\"]")).SendKeys("Google");
                driver.FindElement(By.XPath("//*[@id=\"lst-ib\"]")).Submit();
                string att = driver.FindElement(By.XPath("//*[@id=\"rg_s\"]/div[1]/a/img")).GetAttribute("src");
                driver.Navigate().GoToUrl(att);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                DownloadFile(driver, att, @"C:\Users\dev.tiago\Documents\Projects\MangaSelenium\prints\image2.png");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.Read();
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


    }
}
