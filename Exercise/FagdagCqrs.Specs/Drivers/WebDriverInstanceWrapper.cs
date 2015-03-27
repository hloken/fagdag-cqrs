using OpenQA.Selenium;

namespace FagdagCqrs.Specs.Drivers
{
    public class WebDriverInstanceWrapper
    {
        private readonly IWebDriver _instance;

        public WebDriverInstanceWrapper(IWebDriver instance)
        {
            _instance = instance;
        }

        public IWebDriver Instance
        {
            get { return _instance; }
        }
    }
}