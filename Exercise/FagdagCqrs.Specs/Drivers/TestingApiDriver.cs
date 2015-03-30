namespace FagdagCqrs.Specs.Drivers
{
    public class TestingApiDriver
    {
        private readonly HttpClientWrapper _client;

        private const string _baseUrl = "api/testing";

        public TestingApiDriver(HttpClientWrapper client)
        {
            _client = client;
        }

        public void DropDatabase()
        {
            const string url = _baseUrl + "/dropDatabase";
            _client.Post(url, null);
        }
    }
}