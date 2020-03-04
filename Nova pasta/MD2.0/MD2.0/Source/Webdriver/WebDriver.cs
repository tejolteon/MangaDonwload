using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static System.Awaiter;

namespace MD2._0.Source.Webdriver
{
    public class WebDriverOptions : ChromeOptions
    {
        public WebDriverOptions(string[] options = null)
        {
            AddArgument("--disable-impl-side-painting");

            if (!(options is null))
                foreach (var option in options)
                    AddArgument("--" + option);
        }
    }

    public class WebDriver : ChromeDriver
    {
        public static Dictionary<IWebElement, WebDriver> Register { get; } = new Dictionary<IWebElement, WebDriver>();

        public WebDriver(string[] options) : base(".\\", new WebDriverOptions(options)) { }

        public WebDriver() : base(".\\", new WebDriverOptions()) { }

        public new Screenshot GetScreenshot()
        {
            FitWindow();
            return base.GetScreenshot();
        }

        public void FitWindow()
        {
            var width = Convert.ToInt32(ExecuteScript("return document.body.scrollWidth")) + 100;
            var height = Convert.ToInt32(ExecuteScript("return document.body.scrollHeight")) + 100;

            Manage().Window.Size = new Size(width, height);
        }

        public static WebDriver NI => new WebDriver();

        public bool ElementIsVisible(By by, int seconds, out IWebElement webElement)
        {
            try
            {
                Console.WriteLine("Verificando se o elemento {0} está visível", by);
                webElement = WaitElement(by, seconds);

                if (webElement.Displayed)
                    return false;
                else
                    return true;
            }
            catch
            {
                webElement = null;
                return false;
            }
        }

        public bool ElementIsVisible(By by) =>
            ElementIsVisible(by, 60, out var _);

        public bool ElementIsVisible(By by, out IWebElement webElement) =>
            ElementIsVisible(by, 60, out webElement);

        public void WaitFor(By by, Action<IWebElement, WebDriver> action, int seconds = 60) =>
            action(WaitElement(by, seconds), this);

        public void WaitFor(By by, Action<IWebElement> action, int seconds = 60) =>
            action(WaitElement(by, seconds));

        public IWebElement WaitElementAt(By by, int pos) =>
            WaitElements(by).ElementAtOrDefault(pos);

        public IWebElement WaitElement(By by, int seconds)
        {
            try
            {
                Console.WriteLine("Esperando o elemento {0} ficar visível", by);
                WebDriverWait wait = new WebDriverWait(this, TimeSpan.FromSeconds(seconds));
                var element = wait.Until(x => x.FindElement(by));

                if (Register.ContainsKey(element))
                {
                    Register[element] = this;
                }
                else
                {
                    Register.Add(element, this);
                }

                return element;
            }
            catch { return null; }
        }

        public IWebElement WaitElement(By by) =>
            WaitElement(by, 240);

        public IEnumerable<IWebElement> WaitElements(By by, int seconds)
        {
            var inc = 1;

            IEnumerable<IWebElement> elements = new List<IWebElement>();
            try
            {
                Console.WriteLine("Esperando os elementos {0} ficarem visíveis", by);
                WebDriverWait wait = new WebDriverWait(this, TimeSpan.FromSeconds(seconds));
                while (inc <= seconds)
                {
                    elements = wait.Until(x => x.FindElements(by).Select(element =>
                    {
                        if (Register.ContainsKey(element))
                        {
                            Register[element] = this;
                        }
                        else
                        {
                            Register.Add(element, this);
                        }

                        return element;
                    })) ?? new List<IWebElement>();

                    if (elements.Any())
                        break;

                    inc += 1;
                    DelaySeconds(1);
                }
            }
            catch
            {
                // ignore
            }

            return elements;
        }

        public IEnumerable<IWebElement> WaitElements(By by) =>
            WaitElements(by, 60);
    }
}
