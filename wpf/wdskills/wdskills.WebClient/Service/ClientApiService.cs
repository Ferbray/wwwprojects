using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using wdskills.WebClient.Model;
using wdskills.WebServer.Model;
using System.Net.Http.Json;

namespace wdskills.WebClient.Service
{
    public class ClientApiService
    {
        public string? ErrorMessage { get; private set; }
        private readonly HttpClient _httpClient = new();

        public async Task<T> GetAsync<T>(string urlGet) where T : new()
        {
            T entities = new();
            HttpResponseMessage response = await CreateResponseGetAsync(urlGet);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    entities = await response.Content.ReadFromJsonAsync<T>(
                        new JsonSerializerOptions(JsonSerializerDefaults.Web)) ?? new();
                    ErrorMessage = string.Empty;
                }
                catch (JsonException)
                {
                    ErrorMessage = "Ошибка: на стороне клиента";
                    entities = new();
                }
            }
            else ErrorMessage = response.ReasonPhrase;

            return entities;
        }


        public async Task<string> PostAsync<T>(PostEntityModel<T> postModel)
        {
            HttpResponseMessage response = await CreateResponsePostAsync(postModel);

            if (response.IsSuccessStatusCode) ErrorMessage = string.Empty;
            else ErrorMessage = response.ReasonPhrase;

            return ErrorMessage ?? "Ошибка: неизвестная";
        }

        public async Task<string> DeleteProductAsync(string urlDelete)
        {
            HttpResponseMessage response = await CreateResponseDeleteProductAsync(urlDelete);

            if (response.IsSuccessStatusCode) ErrorMessage = string.Empty;
            else ErrorMessage = response.ReasonPhrase;

            return ErrorMessage ?? "Ошибка: неизвестная";
        }

        private async Task<HttpResponseMessage> CreateResponsePostAsync<T>(PostEntityModel<T> postModel)
        {
            string url = "https://localhost:7073/api" + postModel.PostUrl;
            string serializedProduct = JsonSerializer.Serialize(postModel.Content);
            StringContent content = new(serializedProduct, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url, content);

            return response;
        }

        private async Task<HttpResponseMessage> CreateResponseGetAsync(string urlGet)
        {
            string url = "https://localhost:7073/api" + urlGet;
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            return response;
        }

        private async Task<HttpResponseMessage> CreateResponseDeleteProductAsync(string urlDelete)
        {
            string url = "https://localhost:7073/api" + urlDelete;
            HttpResponseMessage response = await _httpClient.DeleteAsync(url);

            return response;
        }
    }
}
