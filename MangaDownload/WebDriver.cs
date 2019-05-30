using OpenQA.Selenium.Chrome;

namespace MangaDownload
{
    class WebDriver : ChromeDriver
    {
        public WebDriver()
        {
        }

        public WebDriver(ChromeOptions options) : base(options)
        {
        }
    }
}
