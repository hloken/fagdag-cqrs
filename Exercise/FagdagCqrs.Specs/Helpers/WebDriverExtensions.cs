using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using FagdagCqrs.Specs.AngularBindingAdapters;
using FagdagCqrs.Specs.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace FagdagCqrs.Specs.Helpers
{
    public static class WebDriverExtensions
    {
        public static T NavigateAndRefresh<T>(this IWebDriver webDriver, params object[] additionalPageParams) where T : Page
        {
            return Navigate(webDriver, Activator.CreateInstance(typeof(T), new object[] { webDriver }.Concat(additionalPageParams).ToArray()) as T, true);
        }

        public static T Navigate<T>(this IWebDriver webDriver, params object[] additionalPageParams) where T : Page
        {
            return Navigate(webDriver, Activator.CreateInstance(typeof(T), new object[] { webDriver }.Concat(additionalPageParams).ToArray()) as T);
        }

        public static T Navigate<T>(this IWebDriver webDriver, T page, bool forceRefreshIfAlreadyOnPage = false) where T : Page
        {
            var startNew = Stopwatch.StartNew();
            if (ScenarioContext.Current.ContainsKey("currentPage"))
                ScenarioContext.Current.Remove("currentPage");

            ScenarioContext.Current.Add("currentPage", page);

            if (forceRefreshIfAlreadyOnPage || !IsAtPage(webDriver, page))
            {
                var url = page.GetUrl();
                webDriver.Navigate().GoToUrl(url);

                webDriver.WaitUntil(x => x.IsReadyStateComplete());
                WaitForAngularStuffToFinish(webDriver);
                Console.WriteLine("Navigation to url {0} completed after {1} ms", page.GetUrl(), startNew.ElapsedMilliseconds);
            }
            else
            {
                Console.WriteLine("Browser was already on url {0} and refresh wasn't forced. Completed in {1} ms", page.GetUrl(), startNew.ElapsedMilliseconds);
            }

            return page;
        }

        public static void WaitForAngularStuffToFinish(IWebDriver driver, int count = 0)
        {
            try
            {
                ((IJavaScriptExecutor)driver).ExecuteAsyncScript("var callback = arguments[arguments.length - 1]; angular.element(document.body).injector().get('$browser').notifyWhenNoOutstandingRequests(callback);");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("WARNING: Angular stuff not finished initializing...");
                driver.Navigate().Refresh();
                if (count > 5)
                {
                    Console.WriteLine("ERROR: AngularJS did not initialize...");
                    throw;
                }
                Thread.Sleep(100);
                WaitForAngularStuffToFinish(driver, ++count);
            }
        }

        private static bool IsReadyStateComplete(this IWebDriver webDriver)
        {
            return ((IJavaScriptExecutor)webDriver).ExecuteScript("return document.readyState").Equals("complete");
        }

        public static bool IsAtPage(this IWebDriver webDriver, Page page)
        {
            var url = page.GetUrl();
            return webDriver.Url == url;
        }

        public static void WaitUntil(this IWebDriver webDriver, Func<IWebDriver, bool> fx)
        {
            new WebDriverWait(webDriver, new TimeSpan(0, 0, 30)).Until(x =>
            {
                try
                {
                    return fx(webDriver);
                }
                catch (StaleElementReferenceException) { return false; }
                catch (NgViewException) { return false; }
            });
        } 
    }
}