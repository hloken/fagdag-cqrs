using OpenQA.Selenium;

namespace FagdagCqrs.Specs.AngularBindingAdapters
{
    public class AngularClickBinding : AngularBinding
    {
        public AngularClickBinding(IWebElement scope, string propertyName)
            : base(scope, propertyName, "ng-click")
        {
        }

        public void Click()
        {
            Element.Click();
        }
    }
}
