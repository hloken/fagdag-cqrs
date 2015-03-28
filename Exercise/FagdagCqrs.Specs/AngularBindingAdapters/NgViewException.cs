using System;

namespace FagdagCqrs.Specs.AngularBindingAdapters
{
    public class NgViewException : Exception
    {
        public NgViewException(string msg)
            : base(msg)
        { }
    }
}
