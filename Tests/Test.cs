using JustParkAssessment.GetDriver;
using JustParkAssessment.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace JustParkAssessment
{
    [TestClass]
    [TestCategory("Search Results Page")]
    public class Test
    {
        HomePage homePage;
        IWebDriver driver;

        [TestInitialize]
        public void RunBeforeEachTest()
        {
            driver = new Driver().InitializeDriver(Driver.DriverType.Chrome);
            homePage = new HomePage(driver);
        }

        [Description("Verify no modal is displayed when the search duration is 27days 23.55hrs.")]
        [TestMethod]
        public void Test1()
        {
            homePage.NavigateToHomePage();
            Assert.IsTrue(homePage.IsPageDisplayed, "Homepage was not displayed");
            homePage.EnterParkingDetails();
            SearchResultsPage searchResultspage = homePage.ClickShowParkingSpacesButton();
            searchResultspage.EnterDateLesserThanTwentyEight();
            searchResultspage.ClickSearchButton();
            Assert.IsTrue(searchResultspage.VerifyModalIsNotDisplayed(), "Modal is displayed. Modal should not be displayed.");
        }

        [TestCleanup]
        public void RunAfterEachTest()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
