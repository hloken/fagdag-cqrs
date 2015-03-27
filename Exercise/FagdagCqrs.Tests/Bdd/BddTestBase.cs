using NUnit.Framework;

namespace FagdagCqrs.Tests.Bdd
{
    public abstract class BddTestBase
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            Given();
            When();
        }
        
        protected abstract void Given();
        protected abstract void When();
    }
}
