using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace HCAUI.Services
{
  
        public class HttpClientService
        {

            private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

            public HttpClientService(HttpClient httpClient, ILogger logger)
            {
            _logger = logger;
                _httpClient = httpClient;
            }


            private T OperateData<T>(string baseUrl, string endPoint, HttpMethod method) where T : class, new()
            {
                try
                {

                    _logger.LogInformation($"Base URL: {baseUrl}, End point: {endPoint}, HttpMethod: {method.Method}");

                    var httpRequestMessage = new HttpRequestMessage(method, baseUrl + endPoint)
                    {
                        Headers ={
                        { HeaderNames.Accept, "application/json" },

                    }
                    };




                    using (var httpClient = _httpClient.SendAsync(httpRequestMessage))
                    {
                        var response = httpClient.Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var responseBody = response.Content.ReadAsStringAsync().Result;
                            _logger.LogInformation($"Api service call completed:{responseBody} ");

                            var resultData = JsonConvert.DeserializeObject<T>(responseBody);

                            return resultData == null ? new T() : resultData;
                        }
                        else
                        {
                            _logger.LogError($"C4C Call Error: {response.StatusCode} - {response.ReasonPhrase}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
                return new T();
            }

            public T GetData<T>(string baseUrl, string endPoint) where T : class, new() => OperateData<T>(baseUrl, endPoint, HttpMethod.Get);

            public T PostData<T>(string baseUrl, string endPoint) where T : class, new() => OperateData<T>(baseUrl, endPoint, HttpMethod.Post);

        }


    
}
