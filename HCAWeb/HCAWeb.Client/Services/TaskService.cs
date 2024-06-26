﻿using HCA.Model;
using HCA.Model.HCAUI;
using HCAWeb.Client.Services.Interface;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace HCAWeb.Client.Services
{
    public class TaskService : ITaskService
    {
        private readonly ILogger<HCAMaster> _logger;

        private readonly HttpClient _httpClient;

        private readonly HttpClientService _httpClientService;


        public TaskService(HttpClient httpClient, ILogger<HCAMaster> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _httpClientService = new HttpClientService(httpClient, logger);
        }
        public UiResponse AddTask(TaskRequest taskRequest)
        {
            try
            {
                string json = JsonConvert.SerializeObject(taskRequest);
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7213/api/Task/InsertUpdateTask")
                {
                    Headers ={
                        { HeaderNames.Accept, "application/json" },

                    },
                    Content = new StringContent(json, Encoding.UTF8, "application/json")

                };

                using (var httpClient = _httpClient.SendAsync(httpRequestMessage))
                {
                    var response = httpClient.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = response.Content.ReadAsStringAsync().Result;
                        _logger.LogInformation($"Api service call completed:{responseBody} ");

                        var resultData = JsonConvert.DeserializeObject<UiResponse>(responseBody);

                        return resultData;
                    }
                    else
                    {
                        _logger.LogError($"C4C Call Error: {response.StatusCode} - {response.ReasonPhrase}");
                        return new UiResponse { status = -1, message = "Something Went Wrong!" };

                    }
                }
            }
            catch(Exception ex)
            {
                return new UiResponse{ status=-1,message="Something Went Wrong!"};   
            }
           
           
            
        }

        public IEnumerable<TaskDetailsResponse> GetTasks()
        {
            var data = _httpClientService.GetData<ResponseModel<TaskDetailsResponse>>("https://localhost:7213/", "api/Task/GetAllTask");

            return data.data;
           
        }
    }
}
