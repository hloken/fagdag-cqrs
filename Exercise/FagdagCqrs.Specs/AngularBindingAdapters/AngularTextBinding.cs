using OpenQA.Selenium;

namespace FagdagCqrs.Specs.AngularBindingAdapters
{
    public class AngularTextBinding : AngularBinding
    {
        public AngularTextBinding(IWebElement scope, string propertyName)
            : base(scope, propertyName, "ng-bind")
        {
        }

        public string Text { get { return Element.Text; } }
    }
}
