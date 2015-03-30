using System;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using BoDi;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace FagdagCqrs.Specs.Drivers
{
    [Binding]
    public class SetupDrivers
    {
        private readonly IObjectContainer _objectContainer;
        private static WebDriverInstanceWrapper _webDriverWrapper;
        private static HttpClientWrapper _httpClientWrapper;

        public SetupDrivers(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            CreateWebDriver();
            CreateHttpClient();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _webDriverWrapper.Instance.Dispose();
            _webDriverWrapper = null;

            _httpClientWrapper.Instance.Dispose();
            _httpClientWrapper = null;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _objectContainer.RegisterInstanceAs(_webDriverWrapper);
            _objectContainer.RegisterInstanceAs(_httpClientWrapper);

            var testingDriver = _objectContainer.Resolve<TestingApiDriver>();
            testingDriver.DropDatabase();
        }

        [AfterScenario]
        public void Teardown()
        {
            var webDriver = _objectContainer.Resolve<WebDriverInstanceWrapper>().Instance;
            var takesScreenshot = webDriver as ITakesScreenshot;
            if (ScenarioContext.Current.TestError != null && takesScreenshot != null)
            {
                var screenshot = takesScreenshot.GetScreenshot();
#if DEBUG
                const string screenshots = @"..\..\..\screenshots\";
#else
                const string screenshots = @"..\screenshots\";
#endif
                if (!Directory.Exists(screenshots))
                {
                    Directory.CreateDirectory(screenshots);
                }
                screenshot.SaveAsFile(string.Format(@"{0}{1}.png", screenshots, ScreenshotFilename), ImageFormat.Png);
            }
            _webDriverWrapper.Instance.Navigate().GoToUrl("about:blank");
        }

        private static string ScreenshotFilename
        {
            get
            {
                var title = ScenarioContext.Current.ScenarioInfo.Title;
                foreach (var c in Path.GetInvalidFileNameChars())
                {
                    title = title.Replace(c, '_');
                }
                return title + DateTime.Now.ToFileTime();
            }
        }

        private static void CreateHttpClient()
        {
            var httpClient = new HttpClient();
            _httpClientWrapper = new HttpClientWrapper(httpClient, Settings.BaseUrlForWebApi);
        }

        private static void CreateWebDriver()
        {
            var webDriver = GetMyPreferredWebDriver() ?? WebDrivers.GetChromeDriver();

            _webDriverWrapper = new WebDriverInstanceWrapper(webDriver);
        }

        private static IWebDriver GetMyPreferredWebDriver()
        {
            var d = Directory.GetCurrentDirectory();

            if (File.Exists(@"c:\temp\MyPreferredWebDriver.txt"))
            {
                var driverName = File.ReadAllText(@"c:\temp\MyPreferredWebDriver.txt");

                var driverType = Type.GetType(driverName, true);

                return (IWebDriver)Activator.CreateInstance(driverType);
            }
            return null;
        }
    }
}
