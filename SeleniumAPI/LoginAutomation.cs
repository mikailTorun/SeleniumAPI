using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace SeleniumAPI
{
    public class LoginAutomation
    {
        public void Login(string username, string password,string url, IWebDriver webDriver)
        {
            webDriver.Url = url;

            var input = webDriver.FindElement(By.Id("write the username dom element id here"));
            input.SendKeys(username);

            input = webDriver.FindElement(By.Id("write the username dom element id here OR x-path. it s up to you :D"));
            input.SendKeys(password);
            
            try
            {
                ClickAndWaitForPageToLoad(webDriver, By.Id("login-button-id"));
            }
            catch (Exception ex)
            {
                
            }
        }

        private void ClickAndWaitForPageToLoad(IWebDriver driver, By elementLocator, int timeout = 3)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                var elements = driver.FindElements(elementLocator);
                if (elements.Count == 0)
                {
                    throw new NoSuchElementException(
                        "No elements " + elementLocator + " ClickAndWaitForPageToLoad");
                }
                var element = elements.FirstOrDefault(e => e.Displayed);
                element.Click();
                wait.Until(ExpectedConditions.StalenessOf(element));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine(
                    "Element with locator: '" + elementLocator + "' was not found.");
                throw;
            }
        }
    }
}
