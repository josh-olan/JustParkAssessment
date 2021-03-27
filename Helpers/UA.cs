using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustParkAssessment.Helpers
{
    class UA
    {
        IWebDriver driver;
        Synchronization wait;

        public UA(IWebDriver driver)
        {
            this.driver = driver;
            wait = new Synchronization(driver);
        }

        public void EnterTextInField(By locator, string text, string elementName)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                wait.WaitUntilElementIsPresent(locator, elementName, 20);
                IWebElement element = driver.FindElement(locator);
                js.ExecuteScript("return arguments[0].value = arguments[1];", element, "");
                element.SendKeys(text);
            }
            catch (WebDriverException)
            {
                Console.WriteLine("An error occurred while entering text in the text field.");
            }
        }

        public void ClickOnElement(By locator, string elementName)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                wait.WaitUntilElementIsPresent(locator, elementName, 20);
                driver.FindElement(locator).Click();
            }
            catch (WebDriverException e)
            {
                if (e.InnerException is StaleElementReferenceException)
                    Console.WriteLine($"Element cannot be found on the page => {elementName}. {e.Message}");
                else if (e.InnerException is ElementClickInterceptedException)
                    Console.WriteLine("Unable to click on element.");
                else
                    Console.WriteLine($"An unknown error was thrown => {e.Message}");
            }
        }
    }
}
