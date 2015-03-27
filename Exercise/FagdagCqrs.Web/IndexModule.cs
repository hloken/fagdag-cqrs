using Nancy;

namespace FagdagCqrsWeb
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters => Response.AsFile("index.html");
        }
    }
}