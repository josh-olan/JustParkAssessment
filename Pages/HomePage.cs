using JustParkAssessment.GetDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustParkAssessment.Pages
{
    class HomePage : TestBase
    {
        public By PARKING_AT_INPUT_FIELD = By.Id("search-box");
        public By ARRIVING_ON_CALENDAR_FIELD = By.XPath("//span[@data-cy='booking-start-date']");
        public By DONE_BUTTONS = By.CssSelector("a.c-datepicker__close-bt");
        public By SHOW_PARKING_SPACES_BUTTON = By.ClassName("c-searchform__submit");
        public By AUTO_SUGGESTIVE_DROPDOWN = By.XPath("//div[@class='c-predictive-search-input__results-container']");

        public bool IsPageDisplayed => Driver.Title == "JustPark - The Parking App | Find parking in seconds";

        public HomePage(IWebDriver driver) : base(driver) { }

        internal void NavigateToHomePage()
        {
            Driver.Navigate().GoToUrl("https://www.justpark.com/");
            Driver.Manage().Window.Maximize();
            Wait.WaitUntilPageIsDisplayed(20);
        }

        internal void EnterParkingDetails()
        {
            UA.EnterTextInField(PARKING_AT_INPUT_FIELD, "London", "Parking At input field.");
            Wait.WaitUntilElementIsPresent(AUTO_SUGGESTIVE_DROPDOWN, "Auto suggestive dropdown");
            new Actions(Driver).SendKeys(Keys.Enter).Perform();
            UA.ClickOnElement(ARRIVING_ON_CALENDAR_FIELD, "Arriving on calendar dropdown field.");
            UA.ClickOnElement(DONE_BUTTONS, "Arriving on calendar dropdown field.");
        }

        internal SearchResultsPage ClickShowParkingSpacesButton()
        {
            UA.ClickOnElement(SHOW_PARKING_SPACES_BUTTON, "Show Parking spaces button.");
            return new SearchResultsPage(Driver);
        }
    }
}
