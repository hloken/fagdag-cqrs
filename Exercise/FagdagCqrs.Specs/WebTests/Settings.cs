namespace FagdagCqrs.Specs.WebTests
{
    public static class Settings
    {
        public static string BaseUrl
        {
            get
            {
                return "http://localhost/FagdagCqrs/";
            }
        }

        public static string BaseUrlForWebApi
        {
            get
            {
                return "http://localhost:8080/";
            }
        }
    }
}
