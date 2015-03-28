using FagdagCqrs.Tests.Bdd;
using Nancy.Testing;
using RestApi.Bootstrapping;
using RestApi.Data;

namespace FagdagCqrs.Tests.RestApi
{
    public abstract class RestApiBddTestBase : BddTestBase
    {
        private readonly Browser _browser;

        protected RestApiBddTestBase()
        {
            _browser = new Browser(new Bootstrapper());
        }

        public Browser Browser
        {
            get { return _browser; }
        }

        protected override void Given()
        {
            Database.Drop();
        }
    }
}