using System;
using System.Drawing;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.PhantomJS;

namespace FagdagCqrs.Specs.Drivers
{
    public static class WebDrivers
    {
        public static PhantomJSDriver GetPhantomJsDriver()
        {
            var driverService = PhantomJSDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            var phantomJsDriver = new PhantomJSDriver(driverService);
            phantomJsDriver.Manage().Window.Size = new Size(1120, 550);
            phantomJsDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            phantomJsDriver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(3));
            return phantomJsDriver;
        }

        public static FirefoxDriver GetFirefoxDriver()
        {
            var firefoxProfile = new FirefoxProfile();
            firefoxProfile.SetPreference("webdriver.log.browser.ignore", true);
            const int logLevel = 1000;
            firefoxProfile.SetPreference("webdriver.log.browser", logLevel);
            firefoxProfile.SetPreference("webdriver.log.browser.level", logLevel);
            firefoxProfile.SetPreference("webdriver.log.driver.ignore", true);
            firefoxProfile.SetPreference("webdriver.log.driver.level", logLevel);
            firefoxProfile.SetPreference("webdriver.log.driver", logLevel);
            firefoxProfile.SetPreference("webdriver.log.profiler.ignore", true);
            firefoxProfile.SetPreference("webdriver.log.profiler.level", logLevel);
            firefoxProfile.SetPreference("webdriver.log.profiler", logLevel);
            firefoxProfile.SetPreference("webdriver.log.client.ignore", true);
            firefoxProfile.SetPreference("webdriver.log.client.level", logLevel);
            firefoxProfile.SetPreference("webdriver.log.client", logLevel);
            firefoxProfile.SetPreference("webdriver.log.server.ignore", true);
            firefoxProfile.SetPreference("webdriver.log.server.level", logLevel);
            firefoxProfile.SetPreference("webdriver.log.server", logLevel);
            firefoxProfile.SetPreference("webdriver.log.performance.ignore", true);
            firefoxProfile.SetPreference("webdriver.log.performance.level", logLevel);
            firefoxProfile.SetPreference("webdriver.log.performance", logLevel);
            var firefoxDriver = new FirefoxDriver(firefoxProfile);
            firefoxDriver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10));
            firefoxDriver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(3));
            return firefoxDriver;
        }

        public static ChromeDriver GetChromeDriver()
        {
            var chromeDriver = new ChromeDriver(new ChromeOptions {});
            chromeDriver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(3));
            return chromeDriver;
        }
    }
}
