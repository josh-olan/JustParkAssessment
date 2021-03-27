using JustParkAssessment.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustParkAssessment.GetDriver
{
    class TestBase
    {
        public IWebDriver Driver { get; set; }
        public Synchronization Wait { get; set; }
        public UA UA { get; set; }

        public TestBase(IWebDriver driver)
        {
            Driver = driver;
            Wait = new Synchronization(Driver);
            UA = new UA(Driver);
        }
    }
}
