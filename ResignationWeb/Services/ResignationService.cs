using Blazored.LocalStorage;
using ResignationWeb.Models;
using ResignationWeb.Services.Contracts;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using ResignationWeb.Models.DTOs;
using BlazorBootstrap;
using System.Globalization;

namespace ResignationWeb.Services
{
    public class ResignationService : IResignationService
    {
        private readonly HttpClient _httpClient;
        string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJfaWQiOiI2M2NlNzIyMDkzZTkxMzA2N2QwMmFhZGYiLCJlbWFpbCI6ImdwYXN3YW4xOTc2QGdtYWlsLmNvbSIsInRpbWUiOjE2OTA4NjQ0OTQyMjksImlhdCI6MTY5MDg2NDQ5NH0.Oif21Q2hX3k9XjbY3kVashGCg14C5jS2JAJiomF1Uqw";
        public ResignationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }
        public async Task<APIResponse> CreateAsync(ResignationRequestDTO resignRequest)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://192.180.0.127:4148/api/resignation");
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
                var response = await _httpClient.DeleteFromJsonAsync<APIResponse>($"http://192.180.0.127:4148/api/resignation/{resignId}");
                return response!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<APIResponse> GetAsync(int? limit = null, int? index = null, string? sortKey = null,
                                        int? sortDirection = null, string? id = null, int? status = null,
                                        string? userId = null)
        {
            try
               {
                //if (resignId == "")
                //{
                //    var responseAll = await _httpClient.GetFromJsonAsync<APIResponse>($"http://192.180.0.127:4148/api/resignation?limit={limit}&index={index}");
                //    return responseAll!;
                //}


                //var response = await _httpClient.GetFromJsonAsync<APIResponse>($"http://192.180.0.127:4148/api/resignation?limit={limit}&index={index}&id={resignId}");
                //    return response!;
                //}
                
                    string apiUrl = $"http://192.180.0.127:4148/api/resignation?";

                    // Append filters to the URL if provided
                    if (limit!=null)
                    {
                        apiUrl += $"limit={limit}&";
                    }

                    if (index!=null)
                    {
                        apiUrl += $"index={index}&";
                    }

                    if (!string.IsNullOrEmpty(sortKey))
                    {
                        apiUrl += $"sortKey={sortKey}&";
                    }

                    if (sortDirection.HasValue)
                    {
                        apiUrl += $"sortDirection={sortDirection}&";
                    }

                    if (!string.IsNullOrEmpty(id))
                    {
                        apiUrl += $"id={id}&";
                    }

                    if (status.HasValue)
                    {
                        apiUrl += $"status={status}&";
                    }

                    if (!string.IsNullOrEmpty(userId))
                    {
                        apiUrl += $"userId={userId}&";
                    }

                    var response = await _httpClient.GetFromJsonAsync<APIResponse>(apiUrl.TrimEnd('&'));
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
                var request = new HttpRequestMessage(HttpMethod.Put, $"http://192.180.0.127:4148/api/resignationStatus/{resignId}");
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
