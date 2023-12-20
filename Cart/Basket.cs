using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutoTestMimovrste.Cart
{
    public class Basket
    {
        private const int threadSeconds = 5;
        IWebDriver driver = new ChromeDriver();

        [OneTimeSetUp]
        public void ClosingPopup()
        {
            Assist.OpenBrowser(driver);
            Assist.OpenPage(driver, "/");
            driver.FindElement(By.CssSelector("#above-header .legal-consent__buttons-wrapper > div:nth-child(1) > button")).Click();
           
        }

        [Test]
        public void AddProductCart()
        {
            Assist.OpenPage(driver, "/");
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            driver.FindElement(By.CssSelector(".product-box-simple__img-wrap")).Click();
            string nameProduct = driver.FindElement(By.CssSelector("#main-content article > h1")).Text;

            Thread.Sleep(TimeSpan.FromSeconds(threadSeconds));
            driver.FindElement(By.CssSelector("#main-content .add-to-cart")).Click();
            
            driver.FindElement(By.CssSelector("#header-icons > div:nth-child(3) .drop-down__trigger")).Click();
            string nameProductCart = driver.FindElement(By.CssSelector(".cart-overview-item-row__title-and-params > h3")).Text;

            Assert.AreEqual(nameProduct, nameProductCart);

            Delete();
        }
        [Test]
        public void DeleteProductCart()
        {
            Assist.OpenPage(driver, "/");
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            driver.FindElement(By.CssSelector(".product-box-simple__img-wrap")).Click();
            driver.FindElement(By.CssSelector("#main-content .add-to-cart")).Click();
            driver.FindElement(By.CssSelector("#header-icons > div:nth-child(3) .drop-down__trigger")).Click();

            IWebElement remove = driver.FindElement(By.CssSelector(".cart-layout__group-owner a"));
            remove.Click();

            string textCart = driver.FindElement(By.CssSelector(".cart-overview__wrapper .clearfix")).Text;
            Assert.IsTrue(string.Equals(textCart, "Trenutno nimate v košarici nobenega artikla."));
        }

        [Test]
         public void ChangingQuantityGoodsBasket()
         {
            AddProduct();
            string price = driver.FindElement(By.CssSelector(".cart-overview-item-row__count_and_price >div")).Text;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.querySelector('.cart-overview-item-row__count_and_price .article-counter__btn--plus').click();");
            Thread.Sleep(TimeSpan.FromSeconds(threadSeconds));

            string cardPrice = driver.FindElement(By.CssSelector(".cart-layout__price_and_submit >p")).Text;

            Assert.AreNotEqual(cardPrice, price);

            Delete();
         }

        [Test]
        public void AddQuantity()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            string cardPrice = driver.FindElement(By.CssSelector(".product-price__price")).Text;

            AddProduct();

            string pieces = driver.FindElement(By.CssSelector("#navigation-widget li:nth-child(3) .lay-relative.svg-box__wrapper .badge")).Text;

            IWebElement block = driver.FindElement(By.CssSelector(".cart-overview-item-row__count_and_price > span"));
            IWebElement quantity = block.FindElement(By.CssSelector("#cart-nav-state  .article-counter__btn.article-counter__btn--plus"));
            quantity.Click();
            Thread.Sleep(TimeSpan.FromSeconds(threadSeconds));

            string pieces_1 = driver.FindElement(By.CssSelector("#navigation-widget  li:nth-child(3) .badge")).Text;
            string price = driver.FindElement(By.CssSelector("#navigation-widget  li:nth-child(3)  span.wrapper > span")).Text;

            Assert.AreNotEqual(cardPrice, price);
            Assert.AreNotEqual(pieces, pieces_1);

            Delete();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        #region Private methods

        private void AddProduct()
        {
            Assist.OpenPage(driver, "/");

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            IWebElement buy = driver.FindElement(By.CssSelector("#NaN div:nth-child(2) > button"));
            buy.Click();

            driver.FindElement(By.CssSelector(".cross-sell__button__to-cart")).Click();
        }
        private void Delete()
        {
            IWebElement remove = driver.FindElement(By.CssSelector(".cart-layout__group-owner a"));
            remove.Click();
        }

        #endregion
    }
}