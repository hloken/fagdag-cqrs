using System;
using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FagdagCqrs.Specs.AngularBindingAdapters
{
    public static class WebElementExtensions
    {
        public static string GetValue(this IWebElement element)
        {
            return element.GetAttribute("value") ?? string.Empty;
        }
        
        public static void SetValue(this IWebElement element, string value)
        {
            if (element.TagName == "select")
            {
                var lSelectElement = new SelectElement(element);

                lSelectElement.SelectByText(value);
            }
            else
            {
                if (element.Enabled)
                {
                    element.Clear();
                    if (!string.IsNullOrEmpty(value))
                    {
                        element.SendKeys(value);
                    }
                    element.SendKeys("\t");

                }
            }
        }

        //public static void FillInDate(this IWebElement element, DateTime? value, By by)
        //{
        //    FillInDate(element, value);
        //}

        public static void FillInDate(IWebElement findElement, DateTime? value)
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

        private static string FormatDate(DateTime value)
        {
            //if (WebDriver is FirefoxDriver)
            //{
            //    return value.ToString("yyyy-MM-dd");
            //}
            return value.ToString("MMddyyyy");
        }

        public static void TypeText(this IWebElement element, decimal? number)
        {
            if (number.HasValue)
            {
                TypeText(element, number.Value);
            }
        }

        public static void TypeText(this IWebElement element, decimal number)
        {
            TypeText(element, number.ToString(CultureInfo.InvariantCulture));
        }

        public static void TypeText(this IWebElement element, int number)
        {
            TypeText(element, number.ToString(CultureInfo.InvariantCulture));
        }

        public static void TypeText(this IWebElement element, string text)
        {
            element.Clear();
            element.SendKeys(text ?? "");
        }
    }
}