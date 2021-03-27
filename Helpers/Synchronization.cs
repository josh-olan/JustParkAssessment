using JustParkAssessment.GetDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustParkAssessment.Helpers
{
    class Synchronization
    {
        IWebDriver driver;

        public Synchronization(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void WaitUntilPageIsDisplayed(int waitTimeInSeconds = 60)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                new WebDriverWait(driver, TimeSpan.FromSeconds(waitTimeInSeconds)).Until((d) =>
                {
                    return js.ExecuteScript("return document.readyState == 'complete';");
                });
            }
            catch (TimeoutException e)
            {
                Console.WriteLine($"Page not loading after waiting for {waitTimeInSeconds} seconds. Message => {e.Message}");
            }
        }

        public void WaitUntilElementIsPresent(By locator, string elementName, int waitTimeInSeconds = 20)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(waitTimeInSeconds)).Until((d) =>
                {
                    return d.FindElement(locator);
                });
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"Tried to find element {elementName} but could not find it in {waitTimeInSeconds} seconds.");
            }
            catch (TimeoutException)
            {
                Console.WriteLine($"Tried to find element {elementName} but timed out in {waitTimeInSeconds} seconds.");
            }
        }
    }
}
