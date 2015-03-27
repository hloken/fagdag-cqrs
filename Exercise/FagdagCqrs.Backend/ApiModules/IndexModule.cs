using Nancy;

namespace RestApi.ApiModules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters => new {Name = "Hans"};
        }
    }
}