using System.Linq;
using OpenQA.Selenium;

namespace FagdagCqrs.Specs.AngularBindingAdapters
{
    public static class AngularBindingAdapter
    {
        public static IWebElement AngularView(this IWebDriver driver, string containerId = null)
        {
            if (containerId == null)
            {
                var elements = driver.FindElements(By.TagName("ng-view"));

                if (elements.Count != 1)
                    throw new NgViewException(string.Format("no container id was provided, found: {0} containers, expecting: 1", elements.Count));

                return elements.Single();
            }

            var element = driver.FindElement(By.Id(containerId));

            if (element.TagName != "ng-view")
                throw new NgViewException(string.Format("the provided container id ({0}) must be an angular view", containerId));

            return element;
        }

        public static AngularInputBinding InputBinding(this IWebElement scope, string propertyName)
        {
            return new AngularInputBinding(scope, propertyName);
        }

        public static AngularSelectBinding SelectBinding(this IWebElement scope, string propertyName)
        {
            return new AngularSelectBinding(scope, propertyName);
        }

        public static AngularClickBinding ClickBinding(this IWebElement scope, string propertyName)
        {
            return new AngularClickBinding(scope, propertyName);
        }
    }
}
