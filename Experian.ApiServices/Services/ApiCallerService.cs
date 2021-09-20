using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Experian.ApiServices.Services
{
    public abstract class ApiCallerService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings _jsonSerializerSettings = null;
        public ApiCallerService(HttpClient httpClient)
        {
            _httpClient = httpClient;     
        }

        private ApiCallerService(HttpClient httpClient,
            JsonSerializerSettings jsonSerializerSettings)
        {
            _jsonSerializerSettings = jsonSerializerSettings;
            _httpClient = httpClient;
        }
        public async Task<T> GetAsync<T>(string uri)
        {
            var response = await _httpClient.GetAsync(uri);

            return await GetDataAsync<T>(response);
        }

        private async Task<T> GetDataAsync<T> (HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                await HandleFailureResponseAsync(responseMessage);
            }

            var data = await responseMessage.Content.ReadAsStringAsync();

            if (typeof(T) == typeof(string))
                return DeserializeString<T>(data);

            return JsonConvert.DeserializeObject<T>(data, _jsonSerializerSettings);
        }

        private Task HandleFailureResponseAsync(HttpResponseMessage responseMessage)
        {
            var errorText = responseMessage.Content.ReadAsStringAsync();
            throw new Exception($"Server error HTTP ({responseMessage.StatusCode}) .Body:{errorText}");
        }

        private T DeserializeString<T> (string data)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(data, _jsonSerializerSettings);
            }
            catch
            {
                return (T)Convert.ChangeType(data, typeof(T));
            }
        }
    }
}
