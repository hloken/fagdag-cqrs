using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FagdagCqrs.Specs.AngularBindingAdapters
{
    public class AngularSelectBinding : AngularBinding
    {
        public AngularSelectBinding(IWebElement scope, string propertyName)
            : base(scope, propertyName, "ng-model")
        {
        }

        public SelectElement Select { get { return new SelectElement(Element); } }

        public string SelectedOption
        {
            get { return Select.SelectedOption.Text; }
            set { Select.SelectByText(value); }
        }

        public void SelectFirstOptionThatContainsText(string text)
        {
            Select.SelectByText(Select.Options.First(o => o.Text.Contains(text)).Text);
        }

        public string[] GetOptions()
        {
            return Select.Options.Select(o => o.Text).ToArray();
        }
    }
}
