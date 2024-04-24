using HCA.Model;
using HCAWeb.Client.Services.Interface;

namespace HCAWeb.Client.Services
{
    public class HCAMaster : IHCAMaster
    {
        private readonly HttpClient _httpClient;

        private readonly ILogger<HCAMaster> _logger;


        private readonly HttpClientService _httpClientService;
        public HCAMaster(HttpClient httpClient, ILogger<HCAMaster> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _httpClientService = new HttpClientService(httpClient, logger);


        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployee()
        {
            try
            {
                var res = _httpClientService.GetData<ResponseModel<EmployeeDto>>("https://localhost:7213/", "api/HCAMaster/GetAllEmployee");

                //var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7213/api/HCAMaster/GetAllEmployee")
                //{
                //    Headers ={
                //        { HeaderNames.Accept, "application/json" },

                //    }
                //};

                //using (var httpClient = _httpClient.SendAsync(httpRequestMessage))
                //{
                //    var response = httpClient.Result;
                //    if (response.IsSuccessStatusCode)
                //    {
                //        var responseBody = response.Content.ReadAsStringAsync().Result;
                //        //      _logger.LogInformation($"Api service call completed:{responseBody} ");

                //        var resultData = JsonConvert.DeserializeObject<ResponseModel<EmployeeDto>>(responseBody);


                //        return resultData.data;
                //    }
                //    else
                //    {
                //        return null;
                //        //        _logger.LogError($"C4C Call Error: {response.StatusCode} - {response.ReasonPhrase}");
                //    }
                //}



                return res.data;


            }
            catch (Exception ex)
            {
                throw;

            }

        }
   
    
    
    
    
    }

}
