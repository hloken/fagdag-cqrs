using Nancy;

namespace RestApi
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters => new {Name = "Hans"};
        }
    }
}