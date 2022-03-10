using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using System.Threading;

namespace SeleniumAPI
{
    public class Browser
    {
        private readonly CarInfo car;
        public Browser(CarInfo car)
        {
            this.car = car;
        }
        public IWebDriver LaunchBrowser()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");

            return new ChromeDriver(Environment.CurrentDirectory, options);
        }
        public CarInfo getData(IWebDriver driver)
        {
            try
            {
                var elements = driver.FindElements(By.XPath("Write full x-path here"));
                if (elements.Count == 0)
                {
                    throw new NoSuchElementException("No elements  ClickAndWaitForPageToLoad");
                }
                var element = elements.FirstOrDefault(e => e.Displayed);
                element.Click();
                Thread.Sleep(1000);
            }
            catch (NoSuchElementException)
            {
                throw new NoSuchElementException("Element with locator: was not found.");
            }

            return getCarInfo(driver);

        }
        private CarInfo getCarInfo(IWebDriver driver)
        {
            IWebElement sasiNo = driver.FindElement(By.XPath("x-path here"));
            car.ChasisNumber = sasiNo.Text;

            IWebElement model = driver.FindElement(By.XPath("x-path here"));
            car.Model = model.Text;
            return car;
        }
    }
}
