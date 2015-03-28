using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;

namespace FagdagCqrs.Specs.AngularBindingAdapters
{
    public abstract class AngularBinding
    {
        protected readonly string PropertyName;
        protected readonly string BindingType;

        private readonly IWebElement _scope;
        private List<IWebElement> _result;

        protected AngularBinding(IWebElement scope, string propertyName, string bindingType)
        {
            _scope = scope;
            PropertyName = propertyName;
            BindingType = bindingType;
        }

        public IWebElement Element
        {
            get
            {
                if (Elements.Count() > 1)
                {
                    throw new ArgumentException(string.Format("Ambigious {0} binding on property {1}", BindingType, PropertyName));
                }

                if (!Elements.Any())
                {
                    throw new ArgumentException(string.Format("{0} binding on property {1} does not have a match", BindingType, PropertyName));
                }

                return Elements.Single();
            }
        }

        public bool IsVisible
        {
            get { return Element.Displayed; }
        }

        public bool IsEnabled
        {
            get { return Element.Enabled; }
        }

        public IEnumerable<IWebElement> Elements
        {
            get
            {
                if (_result == null)
                {
                    var webElements = ExecuteQuery();

                    _result = webElements;
                }

                return _result;
            }
        }

        public bool Found { get { return Elements.Any(); } }

        private List<IWebElement> ExecuteQuery()
        {
            var pattern = GetPattern();

            return _scope.FindElements(By.XPath(string.Format(".//descendant-or-self::*[{0}]", pattern))).ToList();
        }

        protected virtual string GetPattern()
        {
            return string.Format(@"@{0}='{1}'", BindingType, PropertyName);
        }

        public AngularBinding WaitUntil(Func<AngularBinding, bool> condition, int timeoutInSeconds = 30, int pollIntervallInMilliseconds = 250)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var timeout = TimeSpan.FromSeconds(timeoutInSeconds);

            while (!condition(this) && stopwatch.Elapsed <= timeout)
            {
                Thread.Sleep(250);
            }

            if (!Elements.Any())
            {
                throw new TimeoutException(string.Format("No elements found for {0} binding on property {1} after waiting {2} seconds", BindingType, PropertyName, timeoutInSeconds));
            }
            return this;
        }

        public AngularBinding WaitUntilFound(int timeoutInSeconds = 30, int pollIntervallInMilliseconds = 250)
        {
            return WaitUntil(binding => binding.Found, timeoutInSeconds, pollIntervallInMilliseconds);
        }
    }
}
