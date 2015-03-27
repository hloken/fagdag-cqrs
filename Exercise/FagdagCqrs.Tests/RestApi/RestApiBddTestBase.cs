using FagdagCqrs.Tests.Bdd;
using Nancy.Testing;
using RestApi.Bootstrapping;

namespace FagdagCqrs.Tests.RestApi
{
    public abstract class RestApiBddTestBase : BddTestBase
    {
        private readonly Browser _browser;

        protected RestApiBddTestBase()
        {
            //Container = CommonBootstrapping.CreateContainer();
            //Bus = Substitute.For<IBus, IManageMessageHeaders>();
            //Container.Register(Component.For<IBus>().Instance(Bus));
            //Container.Register(Component.For<DbConnection>().Named("TestConnection").IsDefault().UsingFactoryMethod(m => SqlConnectionHelper.GetOpenConnection(), true));
            //Container.Register(Classes.FromAssemblyContaining<HandleSalaryHasChanged>().BasedOn(typeof(IHandleMessages<>)).WithServiceSelf().LifestyleTransient());

            //var metalPayNancyBootstrapper = new TestNancyBootstrapper(Container, ConfigureContainer);
            _browser = new Browser(new Bootstrapper());
        }

        public Browser Browser
        {
            get { return _browser; }
        }

    }
}