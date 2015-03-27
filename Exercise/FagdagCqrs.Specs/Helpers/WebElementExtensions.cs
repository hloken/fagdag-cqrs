using System.Globalization;
using OpenQA.Selenium;

namespace FagdagCqrs.Specs.Helpers
{
    public static class WebElementExtensions
    {
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