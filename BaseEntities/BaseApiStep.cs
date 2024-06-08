using NLog;
using RestSharp;
using TestRail.Services.API;

namespace TestRail.BaseEntities
{
    public class BaseApiStep
    {
        protected RestClient client;
        protected ApiService apiService;
        protected readonly Logger logger = LogManager.GetCurrentClassLogger();

        public BaseApiStep(RestClient client, ApiService apiService)
        {
            this.client = client;
            this.apiService = apiService;
        }
    }
}
