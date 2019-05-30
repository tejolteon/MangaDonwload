using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using MangaDownload;
using OpenQA.Selenium.Support.UI;

namespace OpenQA.Selenium
{
    static class Extensions
    {
        private static WebDriver ResolveWebDriver(this IWebElement element, WebDriver webDriver)
        {
            if (webDriver == null)
            {
                if (!WebDriver.Register.ContainsKey(element))
                    throw new ArgumentNullException(nameof(webDriver), "Web Driver não informado.");

                webDriver = WebDriver.Register[element];
            }

            return webDriver;
        }

        public static Bitmap TakeScreenShotFromFrame(this IWebElement element, Bitmap img, WebDriver webDriver = null)
        {
            webDriver = element.ResolveWebDriver(webDriver);

            var location = element.Location;
            var size = element.Size;

            var left = location.X;
            var top = location.Y;
            var right = location.X + size.Width;
            var bottom = location.Y + size.Height;

            var png = new Bitmap(size.Width, size.Height);

            using (var stream = new MemoryStream())
            {
                var pos = stream.Position;
                img.Save(stream, ImageFormat.Png);
                stream.Position = pos;

                using (var bmp = Image.FromStream(stream))
                {
                    using (var graph = Graphics.FromImage(png))
                    {
                        graph.DrawImage(bmp, -left, -top);
                        graph.Save();
                    }
                }
            }

            return png;
        }

        public static Bitmap TakeScreenShot(this IWebElement element, WebDriver webDriver = null, Margin margin = new Margin())
        {
            webDriver = element.ResolveWebDriver(webDriver);

            var img = webDriver.GetScreenshot();

            var location = element.Location;
            var size = element.Size;

            var left = location.X + margin.Left;
            var top = location.Y + margin.Top;

            var png = new Bitmap(size.Width, size.Height);

            using (var stream = new MemoryStream(img.AsByteArray))
            using (var bmp = Image.FromStream(stream))
            {
                using (var graph = Graphics.FromImage(png))
                {
                    graph.DrawImage(bmp, -left, -top);
                    graph.Save();
                }
            }

            return png;
        }

        public static void SetText(this IWebElement element, string text, WebDriver webDriver = null)
        {
            webDriver = element.ResolveWebDriver(webDriver);

            var id = element.GetOrSetId(webDriver);

            element.ExecuteScript(string.Format("document.getElementById('{0}').value='{1}';", id, text), webDriver);
            Console.WriteLine("Colocando o texto {0} no elemento {1}.", text, id);
        }

        public static void ExecuteScript(this IWebElement element, string script, WebDriver webDriver = null, params object[] args) =>
            element.ResolveWebDriver(webDriver).ExecuteScript(script, args);

        public static string GetOrSetId(this IWebElement element, WebDriver webDriver = null)
        {
            webDriver = element.ResolveWebDriver(webDriver);
            string id = element.GetAttribute("Id") ?? element.GetAttribute("ID") ?? element.GetAttribute("id");

            if (!string.IsNullOrEmpty(id))
            {
                return id;
            }

            string elementId = string.Format("element_{0:D}", Guid.NewGuid());

            element.SetAttributeValue("id", elementId, webDriver);

            return elementId;
        }

        public static void SetAttributeValue(this IWebElement element, string attributeName, string attributeValue, WebDriver webDriver = null)
        {
            webDriver = element.ResolveWebDriver(webDriver);
            element.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", webDriver, attributeName, attributeValue);
            Console.WriteLine("Colocando o atributo {0} no elemento {1} de valor {2}.", attributeName, element, attributeValue);
        }

        public static bool WaitDisapear(this IWebElement element, int seconds, WebDriver webDriver = null)
        {
            try
            {
                Console.WriteLine("Esperando o elemento {0} desaparecer", element);
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(seconds));
                return wait.Until(_ => !element.Displayed);
            }
            catch { return true; }
        }

        public static bool WaitDisapear(this IWebElement element, WebDriver webDriver = null) =>
            WaitDisapear(element, 60, webDriver);
    }

    public struct Margin
    {
        public int Left;
        public int Top;
    }
}
