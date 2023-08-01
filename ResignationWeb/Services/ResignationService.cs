using Blazored.LocalStorage;
using ResignationWeb.Models;
using ResignationWeb.Services.Contracts;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using ResignationWeb.Models.DTOs;

namespace ResignationWeb.Services
{
    public class ResignationService : IResignationService
    {
        private readonly HttpClient _httpClient;
        string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJfaWQiOiI2M2NlODg3MTkzZTkxMzA2N2QwMzEyN2IiLCJlbWFpbCI6ImFtYW50YXJhcjAxQGdtYWlsLmNvbSIsInRpbWUiOjE2OTAyNjkyOTY3MjcsImlhdCI6MTY5MDI2OTI5Nn0.bwkDH2ReV2RqTgiAo8vG1SKh8Ast7DUl5jHJI219wOY";
        public ResignationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }
        public async Task<APIResponse> CreateAsync(ResignationRequestDTO resignRequest)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://192.180.0.127:4146/api/resignation");
                request.Content = new StringContent(JsonSerializer.Serialize(resignRequest), Encoding.UTF8, "application/json");
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

                var result = await _httpClient.SendAsync(request);

                if (!result.IsSuccessStatusCode)
                {
                    var errorResponse = await result.Content.ReadFromJsonAsync<APIResponse>();
                    return errorResponse!;
                }
                var response = await result.Content.ReadFromJsonAsync<APIResponse>();
                return response!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<APIResponse> DeleteAsync(string resignId)
        {
            try
            {
                var response = await _httpClient.DeleteFromJsonAsync<APIResponse>($"http://192.180.0.127:4146/api/resignation/{resignId}");
                return response!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<APIResponse> GetAsync(string resignId="")
        {
                try
                {
                if (resignId == "")
                {
                    var res = await _httpClient.GetFromJsonAsync<APIResponse>($"http://192.180.0.127:4146/api/resignation");
                    return res!;
                }
                    var response = await _httpClient.GetFromJsonAsync<APIResponse>($"http://192.180.0.127:4146/api/resignation?id={resignId}");
                    return response!;
                }
                catch (Exception)
                {
                    throw;
                }
         
        }

        public Task<APIResponse> UpdateAsync(string resignId, ResignationRequestDTO resignUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse> UpdateStatusAsync(string resignId, ResignationStatusDTO resignStatus)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"http://192.180.0.127:4146/api/resignationStatus/{resignId}");
                request.Content = new StringContent(JsonSerializer.Serialize(resignStatus), Encoding.UTF8, "application/json");
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

                var result = await _httpClient.SendAsync(request);

                if (!result.IsSuccessStatusCode)
                {
                    var errorResponse = await result.Content.ReadFromJsonAsync<APIResponse>();
                    return errorResponse!;
                }
                var response = await result.Content.ReadFromJsonAsync<APIResponse>();
                return response!;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
