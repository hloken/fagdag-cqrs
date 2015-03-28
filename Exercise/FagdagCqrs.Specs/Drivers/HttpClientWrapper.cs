using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FagdagCqrs.Specs.Drivers
{
    public class HttpClientWrapper
    {
        private readonly HttpClient _instance;
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        private readonly string _baseUrl;

        public HttpClientWrapper(HttpClient instance, string baseUrl)
        {
            _instance = instance;
            _baseUrl = baseUrl;
        }

        public dynamic Put(string relativeUrl, dynamic content)
        {
            return PostOrPutInternal(relativeUrl, content, false);
        }

        public dynamic Post(string relativeUrl, dynamic content)
        {
            return PostOrPutInternal(relativeUrl, content, true);
        }

        public dynamic Head(string url, bool absoluteUrl)
        {
            //_instance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TenantId", Tenant.TenantId);
            var requestUri = absoluteUrl ? url : CreateUrl(url);
            Console.WriteLine("HEAD to {0}", requestUri);
            var sendAsync = _instance.SendAsync(new HttpRequestMessage(HttpMethod.Head, requestUri));
            return GetResult(sendAsync, false);
        }

        public dynamic Get(string relativeUrl, bool deserializeResultAsJson = true)
        {
            //_instance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TenantId", Tenant.TenantId);
            var @async = _instance.GetAsync(CreateUrl(relativeUrl));
            return GetResult(@async, deserializeResultAsJson);
        }

        private dynamic PostOrPutInternal(string relativeUrl, dynamic content, bool post)
        {
            //_instance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TenantId", Tenant.TenantId);
            var serializeObject = JsonConvert.SerializeObject(content, _jsonSerializerSettings);

            Task<HttpResponseMessage> request;
            if (post)
            {
                Console.WriteLine("POSTing {0} to {1}", serializeObject, relativeUrl);
                request = _instance.PostAsync(CreateUrl(relativeUrl), new StringContent(serializeObject, Encoding.UTF8, "application/json"));
            }
            else
            {
                Console.WriteLine("PUTing {0} to {1}", serializeObject, relativeUrl);
                request = _instance.PutAsync(CreateUrl(relativeUrl), new StringContent(serializeObject, Encoding.UTF8, "application/json"));
            }
            return GetResult(request, true);
        }

        private string CreateUrl(string relativeUrl)
        {
            return string.Format("{0}{1}", _baseUrl, relativeUrl);
        }

        private static dynamic GetResult(Task<HttpResponseMessage> request, bool deserializeAsJson)
        {
            request.Wait();
            var result = WaitThenReturnResult(request.Result.Content);

            if (!request.Result.IsSuccessStatusCode)
            {
                throw new Exception(string.Format("{3} failed. StatusCode: {0}, Message: {1} \n Result: {2}", request.Result.StatusCode, request.Result.ReasonPhrase, result, request.Result.RequestMessage.Method));
            }

            if (deserializeAsJson)
                return JsonConvert.DeserializeObject(result);

            return result;
        }

        private static string WaitThenReturnResult(HttpContent content)
        {
            var asStringAsync = content.ReadAsStringAsync();
            asStringAsync.Wait();
            return asStringAsync.Result;
        }


        public HttpClient Instance
        {
            get { return _instance; }
        }
    }
}