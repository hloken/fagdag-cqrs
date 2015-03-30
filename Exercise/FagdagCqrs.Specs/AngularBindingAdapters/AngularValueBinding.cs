using OpenQA.Selenium;

namespace FagdagCqrs.Specs.AngularBindingAdapters
{
    public class AngularValueBinding : AngularBinding
    {
        public AngularValueBinding(IWebElement scope, string propertyName)
            : base(scope, propertyName, "ng-value")
        {
        }

        public string Value { get { return Element.GetAttribute("Value"); } }
    }
}