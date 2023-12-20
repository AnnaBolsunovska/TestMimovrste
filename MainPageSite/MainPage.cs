using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Net;

namespace AutoTestMimovrste.MainPageSite
{
    public class MainPage
    {
        private const int ThreadSeconds = 5;

        private const string InputValue = "darila";

        IWebDriver driver = new ChromeDriver();

        [OneTimeSetUp]
        public void ClosingPopup()
        {
            Assist.OpenBrowser(driver);
            Assist.OpenPage(driver, "/");
            Thread.Sleep(TimeSpan.FromSeconds(ThreadSeconds));
            driver.FindElement(By.CssSelector("#above-header .legal-consent__buttons-wrapper > div:nth-child(1) > button")).Click();
        }

        [Test]
        public void HeaderLinks()
        {
            Assist.OpenPage(driver, "/");

            IList<IWebElement> headerLinks = driver.FindElements(By.CssSelector(".list.horizontal-menu.list--horizontal a"));

            foreach (IWebElement element in headerLinks)
            {
                string url = element.GetAttribute("href");

                driver.SwitchTo().NewWindow(WindowType.Tab);
                driver.Navigate().GoToUrl(url);

                Assert.AreEqual(url, driver.Url);

                driver.Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
            }
        }

        [Test]
        public async Task MainMenuResponseOk()
        {
            Assist.OpenPage(driver, "/");

            IList<IWebElement> links = driver.FindElements(By.CssSelector(".desktop-menu__list a"));

            bool allLinksOk = true;
            HttpClient httpClient = new HttpClient();
            
            foreach (IWebElement element in links)
            {
                string urlToCheck = element.GetAttribute("href");

                if (string.IsNullOrEmpty(urlToCheck))
                {
                    Console.WriteLine($"{element.Text}  ---  URL is null");
                    allLinksOk = false;
                }
                else
                {
                    using HttpResponseMessage response = await httpClient.GetAsync(urlToCheck);
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            Console.WriteLine($"{urlToCheck}  ---  Status FAILED");
                            allLinksOk = false;
                        }
                    }
                }
            }

            Assert.IsTrue(allLinksOk);
        }

        [Test]
        public void SearchFunction()
        {
            Assist.OpenPage(driver, "/");

            IWebElement container = driver.FindElement(By.CssSelector(".header__col.header__col__bot-center"));
            IWebElement inputElement = container.FindElement(By.Id("site-search-input"));
            inputElement.Click();

            Thread.Sleep(TimeSpan.FromSeconds(ThreadSeconds));
            container.FindElement(By.Id("site-search-input")).SendKeys(InputValue);

            IWebElement searchButton = driver.FindElement(By.CssSelector("#site-search-suggestions #search-button"));
            searchButton.Click();

            Thread.Sleep(TimeSpan.FromSeconds(ThreadSeconds));
            IWebElement h1Element = driver.FindElement(By.CssSelector(".search__right-wide h1"));

            Assert.IsTrue(h1Element.Text.Equals(InputValue, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public void CouponsChartLinks()
        {
            Assist.OpenPage(driver, "/");

            IWebElement banner = driver.FindElement(By.CssSelector(".hp-banner__main-slide-wrapper"));
            string bannerUrl = banner.GetAttribute("href");
            banner.Click();
            Thread.Sleep(TimeSpan.FromSeconds(ThreadSeconds));

            Assert.AreEqual(bannerUrl, driver.Url);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
