using JustParkAssessment.GetDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustParkAssessment.Pages
{
    class SearchResultsPage : TestBase
    {
        HomePage homePage;

        public By LEAVING_ON_INPUT_FIELD = By.XPath("//span[text()='Leaving on']/../../span");
        public By ACTIVE_MONTH_AND_YEAR = By.XPath("//div[contains(@class, 'CalendarMonth_caption_1')]/parent::div[@data-visible='true']//strong");
        public By NEXT_MONTH_ICON = By.CssSelector("button.DayPickerNavigation_rightButton__horizontal_4");
        public By UPWARD_HOUR_SCROLLER = By.XPath("//div[@class='c-timeWheelSelect']/div[1]/button[1]");
        public By UPWARD_MINUTE_SCROLLER = By.XPath("//div[@class='c-timeWheelSelect']/div[2]/button[1]");
        public By DONE_BUTTON_FOR_LEAVING_ON = By.XPath("//strong[contains(text(), 'Leaving')]/../a");
        public By MONTHLY_SAVER_MODAL = By.XPath("//div[contains(@class, 'c-monthlyUpgradeModal')]//h1/..");
        public By SEARCH_BUTTON = By.ClassName("c-searchform__submit");

        public SearchResultsPage(IWebDriver driver) : base(driver) 
        {
            homePage = new HomePage(Driver);
        }

        public bool IsPageDisplayed()
        {
            Wait.WaitUntilPageIsDisplayed();
            return Driver.Title == "London Parking | Guaranteed Spaces";
        }

        internal void EnterDateLesserThanTwentyEight()
        {
            UA.ClickOnElement(LEAVING_ON_INPUT_FIELD, "Arriving On input field.");
            FilterDate();
            UA.ClickOnElement(DONE_BUTTON_FOR_LEAVING_ON, "Done button");
        }

        internal void ClickSearchButton()
        {
            UA.ClickOnElement(SEARCH_BUTTON, "Search button");
        }

        internal bool VerifyModalIsNotDisplayed()
        {
            try
            {
                return Driver.FindElement(MONTHLY_SAVER_MODAL) == null;
            }
            catch (WebDriverException e)
            {
                Console.WriteLine($"{e.Message}");
                return false;
            }
        }

        public void FilterDate()
        {
            DateTime dateInTwentyEightDays = DateTime.Now.AddDays(28);
            string getMonthOfDate = dateInTwentyEightDays.ToString("MMM").ToUpper();
            string currentMonth = Driver.FindElement(ACTIVE_MONTH_AND_YEAR).Text.Substring(0, 3).ToUpper();
            int counter = 0;
            while (currentMonth != getMonthOfDate && counter < 12)
            {
                UA.ClickOnElement(NEXT_MONTH_ICON, "Next month icon");
                Wait.WaitUntilElementIsPresent(ACTIVE_MONTH_AND_YEAR, "Active month and year element");
                currentMonth = Driver.FindElement(ACTIVE_MONTH_AND_YEAR).Text.Substring(0, 3).ToUpper();
                counter++;
            }
            ClickDateElementFromCalendar(dateInTwentyEightDays.Day.ToString());
            SelectHourAndMinute(dateInTwentyEightDays.Hour.ToString(), dateInTwentyEightDays.Minute.ToString());
        }

        public void ClickDateElementFromCalendar(string day)
        {
            Actions actions = new Actions(Driver);
            IWebElement date = Driver.FindElement(By.XPath($"//*[@role='presentation'][contains(@class, 'CalendarMonth_table')]/parent::div[@data-visible='true']/table//td[text()='{day}']"));
            actions.Click(date).Perform();
        }

        public void SelectHourAndMinute(string hour, string minute)
        {
            string desiredHour = hour;
            string desiredMinute = AdjustMinuteToBeDivisibleBy5(minute).ToString();
            int hourCounter = 0;
            int minuteCounter = 0;
            string currentHour = Driver.FindElements(By.XPath("//li[contains(@class, '--is-selected')]"))[0].Text;
            string currentMinute = Driver.FindElements(By.XPath("//li[(contains(@class, '--is-selected'))]"))[1].Text;
            if (desiredHour.Length == 1)
                desiredHour = $"0{desiredHour}";
            if (desiredMinute.Length == 1)
                desiredMinute = $"0{desiredMinute}";
            while (desiredHour != currentHour && hourCounter < 24)
            {
                UA.ClickOnElement(UPWARD_HOUR_SCROLLER, "Upward hour scroller.");
                currentHour = Driver.FindElements(By.XPath("//li[contains(@class, '--is-selected')]"))[0].Text;
                hourCounter++;
            }
            while (desiredMinute != currentMinute && minuteCounter < 20)
            {
                UA.ClickOnElement(UPWARD_MINUTE_SCROLLER, "Upward minute scroller.");
                currentMinute = Driver.FindElements(By.XPath("//li[contains(@class, '--is-selected')]"))[1].Text;
                minuteCounter++;
            }
        }

        public int AdjustMinuteToBeDivisibleBy5(string minute)
        {
            int value = Convert.ToInt32(minute);
            for (int i = 0; i < 10; i++)
            {
                if (i == 0 && value % 5 == 0)
                    continue;
                if (value % 5 == 0 && i != 0)
                    break;
                value--;
            }
            return value;
        }
    }
}
