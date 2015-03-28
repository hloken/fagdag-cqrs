using System;
using OpenQA.Selenium;

namespace FagdagCqrs.Specs.AngularBindingAdapters
{
    public class AngularInputBinding : AngularBinding
    {
        public AngularInputBinding(IWebElement scope, string propertyName)
            : base(scope, propertyName, "ng-model")
        {
        }

        public string Value
        {
            get
            {
                return Element.GetValue();
            }
            set
            {
                Element.SetValue(value);
            }
        }

        public void TypeText(decimal number)
        {
            Element.TypeText(number);
        }

        public void TypeText(string text)
        {
            Element.TypeText(text);
        }

        public void TypeDate(DateTime? value)
        {
            WebElementExtensions.FillInDate(Element, value);
        }
    }
}
