using System.IO;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using OpenQA.Selenium;
using MD2._0.Source.Webdriver;

namespace MD2._0.Source.Download
{
    public static class Generic
    {
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static string GetCookieHeaderString(WebDriver driver)
        {
            var cookies = driver.Manage().Cookies.AllCookies;
            return string.Join("; ", cookies.Select(c => string.Format("{0}={1}", c.Name, c.Value)));
        }

        public static void DownloadFile(this WebDriver driver, string sourceURL, string destinationPathAndName)
        {
            using WebClient wc = new WebClient();
            wc.Headers[HttpRequestHeader.Cookie] = GetCookieHeaderString(driver);
            wc.DownloadFile(sourceURL, destinationPathAndName);
        }

        public static void OpenChapter(this WebDriver driver, string capUrl, string path)
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

        public static int OpenVolume(this WebDriver driver, string capUrl, string path, int pageNumber)
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
    }
}
