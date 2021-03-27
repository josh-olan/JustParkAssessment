using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustParkAssessment.GetDriver
{
    class Driver
    {
        public IWebDriver InitializeDriver(DriverType driverType)
        {
            switch (driverType)
            {
                case DriverType.Chrome:
                    return new ChromeDriver();
                case DriverType.Edge:
                    return new EdgeDriver();
                case DriverType.Firefox:
                    return new FirefoxDriver();
                default:
                    return new ChromeDriver();
            }
        }

        public enum DriverType
        {
            Chrome,
            Edge,
            Firefox
        }
    }
}
