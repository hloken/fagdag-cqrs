using FagdagCqrs.Database.Data;
using Nancy;

namespace FagdagCqrs.Backend.ApiModules
{
    public class TestingModule : NancyModule
    {
        public TestingModule() : base("api/testing")
        {
            Post["/dropDatabase"] = parameters =>
            {
                TheDatabase.Instance().Drop();

                return HttpStatusCode.OK;
            };
        }
    }
}