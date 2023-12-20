using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AutoTestMimovrste
{
    public static class Assist
    {
        public static void OpenBrowser(IWebDriver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Manage().Window.Maximize();
        }
        public static void OpenPage(IWebDriver driver, string url)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            driver.Navigate().GoToUrl($"https://www.mimovrste.com{url}");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(3);

            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
        }
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            if (by == null)
            {
                throw new ArgumentNullException(nameof(by));
            }

            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

                return wait.Until(ExpectedConditions.ElementIsVisible(by));
            }

            return driver.FindElement(by);
        }

    }
}