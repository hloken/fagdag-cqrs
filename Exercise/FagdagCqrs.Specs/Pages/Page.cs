using System;
using System.Diagnostics;
using System.Linq;
using FagdagCqrs.Specs.AngularBindingAdapters;
using FagdagCqrs.Specs.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace FagdagCqrs.Specs.Pages
{
    public abstract class Page
    {
        protected readonly IWebDriver WebDriver;

        protected Page(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        protected abstract string Url { get; }

        public string GetUrl()
        {
            var url = string.Format("{0}{1}", Settings.BaseUrl, Url);
            return url;
        }

        public void WaitUntilLoaded(bool ignoreIfNotExists = false)
        {
            var startNew = Stopwatch.StartNew();

            bool hadToWait = false;

            WebDriver.WaitUntil(x =>
            {
                if (ignoreIfNotExists && !WebDriver.FindElements(By.CssSelector(".loaded")).Any())
                    return true;

                if (!WebDriver.FindElement(By.CssSelector(".loaded")).Displayed)
                {
                    hadToWait = true;
                    return false;
                }
                return true;
            });

            if (hadToWait)
            {
                Console.WriteLine("WaitUntilLoaded finished after {0} ms", startNew.ElapsedMilliseconds);
            }
        }

        protected void WaitUntilSaved()
        {
            var startNew = Stopwatch.StartNew();
            WebDriver.WaitUntil(x => !x.FindElement(By.CssSelector(".loading")).Displayed);
            Console.WriteLine("Load finished after {0} ms", startNew.ElapsedMilliseconds);
        }

        protected void FillInValue(object value, By by)
        {
            FillInValue(value.ToString(), by);
        }

        protected void FillInValue(string value, By by)
        {
            if (!string.IsNullOrEmpty(value))
            {
                WebDriver.FindElement(@by).Clear();
                WebDriver.FindElement(@by).SendKeys(value);
            }
        }

        protected void FillInDate(DateTime? value, By by)
        {
            var findElement = WebDriver.FindElement(@by);
            FillInDate(value, findElement);
        }

        protected void FillInDate(DateTime? value, IWebElement findElement)
        {
            if (value.HasValue)
            {

                try { findElement.Clear(); }
                // ReSharper disable EmptyGeneralCatchClause
                catch
                // ReSharper restore EmptyGeneralCatchClause
                { }

                string formattedDate = FormatDate(value.Value);
                findElement.SendKeys(formattedDate);
            }
        }

        private string FormatDate(DateTime value)
        {
            if (WebDriver is FirefoxDriver)
            {
                return value.ToString("yyyy-MM-dd");
            }
            return value.ToString("MMddyyyy");
        }

        protected void ClickDefaultSave()
        {
            WebDriver.FindElement(By.CssSelector("button.save")).Click();
            WaitUntilSaved();
        }

        public IWebElement AngularView
        {
            get { return GetAngularView(); }
        }

        public virtual IWebElement GetAngularView(string containerId = null)
        {
            return WebDriver.AngularView(containerId);
        }
    }
}