using HtmlAgilityPack;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace MangaDownload
{
    public static class Generic
    {
        public static string file;

        public static void Start(string url, string manga, bool navegador, int capitulo, int volume, string path, int volNumber)
        {
            try
            {
                if (navegador)
                    ViaSelenium.StartProcess(url, manga, capitulo, volume, path, volNumber);
                else
                    ViaCrawler.StartProcess(url, manga, capitulo, volume, path, volNumber);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void StartH(string url, string manga, bool navegador, string path)
        {
            try
            {
                if (navegador)
                    ViaSelenium.StartProcessH(url, manga, path);
                else
                    ViaCrawler.StartProcessH(url, manga, path);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        #region Selenium

        public static string GetCookieHeaderString(WebDriver driver)
        {
            var cookies = driver.Manage().Cookies.AllCookies;
            return string.Join("; ", cookies.Select(c => string.Format("{0}={1}", c.Name, c.Value)));
        }

        public static void DownloadFile(this WebDriver driver, string sourceURL, string destinationPathAndName)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.Cookie] = GetCookieHeaderString(driver);
                wc.DownloadFile(sourceURL, destinationPathAndName);
            }
        }

        public static void OpenCap(this WebDriver driver, string capUrl, string path)
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

        public static int OpenVol(this WebDriver driver, string capUrl, string path, int pageNumber)
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

        #endregion

        #region Crawler

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

        public static void DownloadFile(string sourceURL, string destinationPathAndName)
        {
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(sourceURL, destinationPathAndName);
            }
        }

        public static int OpenVol(string capUrl, string path, int pageNumber)
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
                newPath = path + "\\" + pageNumber.ToString("000") + ".jpg";
                DownloadFile(imageUrl[i], newPath);
                pageNumber++;
            }

            return pageNumber;
        }

        #endregion
    }
}
