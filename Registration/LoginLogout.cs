using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;

namespace AutoTestMimovrste.Registration
{
    public class LoginLogout
    {

        private const int threadSeconds = 5;
        IWebDriver driver = new ChromeDriver();

        [OneTimeSetUp]
        public void OpenBrower()
        {
            Assist.OpenBrowser(driver);
            Assist.OpenPage(driver, "/");
            driver.FindElement(By.CssSelector("#above-header .legal-consent__buttons-wrapper > div:nth-child(1) > button")).Click();
        }

        [Test]
        public void LoginAccount()
        {
           
            Actions act = new Actions(driver);
            act.MoveToElement(driver.FindElement(By.CssSelector(".desktop-icons__item--user .drop-down__trigger")));
            act.Perform();
            driver.FindElement(By.CssSelector("#username")).SendKeys("ii5449020@gmail.com");
            driver.FindElement(By.CssSelector("#password")).SendKeys("Ivanov154");
            driver.FindElement(By.CssSelector("#header-icons   form > button > div > span")).Click();
        }

        [OneTimeTearDown]
        public void CloseDriver()
        {
            driver.Quit();
        }
    }
}
