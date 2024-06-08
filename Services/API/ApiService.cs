using NLog;
using RestSharp.Authenticators;
using RestSharp;

namespace TestRail.Services.API
{
    public class ApiService
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public RestClient SetUpClientWithOptions(string baseUrl, string userName, string password)
        {
            var options = CreateOptions(baseUrl, userName, password);
            return SetUpClient(options);
        }

        private RestClient SetUpClient(RestClientOptions options)
        {
            return new RestClient(options);
        }

        private RestClientOptions CreateOptions(string baseUrl, string userName, string password)
        {
            return new RestClientOptions(baseUrl)
            {
                Authenticator = new HttpBasicAuthenticator(userName, password)
            };
        }

        public RestResponse CreateGetRequest(RestClient client, string endPoint)
        {
            return client.ExecuteGet(new RestRequest(endPoint));
        }

        public RestResponse CreatePostRequest(RestClient client, string endPoint,  string body)
        {
            return client.ExecutePost(new RestRequest(endPoint).AddJsonBody(body));
        }

        public RestResponse CreatePostRequest(RestClient client, string endPoint,  string name, string value)
        {
            return client.ExecutePost(new RestRequest(endPoint).AddHeader(name, value));
        }

        public string GetContent(RestResponse response)
        {
            if (response.Content != null)
            {
                return response.Content;
            }
            else
            {
                var exception = new Exception("Response content is null.");
                _logger.Error("Failed to get response content.");
                throw exception;
            }
        }
    }
}
