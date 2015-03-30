using Nancy.Testing;

namespace FagdagCqrs.Tests.Drivers
{
    public class TestingDriver
    {
        private const string _baseUrl = "api/testing";

        public static BrowserResponse DropDatabaseWithResponse(Browser browser)
        {
            const string url = _baseUrl + "/dropDatabase";
            return browser.Post(url);
        }
    }
}