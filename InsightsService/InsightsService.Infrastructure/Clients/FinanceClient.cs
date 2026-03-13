using InsightsService.Application.Dtos;
using InsightsService.Application.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace InsightsService.Infrastructure.Clients
{
    public class FinanceClient : IFinanceClient
    {
        private readonly HttpClient _httpClient;

        public FinanceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ExternalTransactionDto>> GetTransactionsAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("api/transactions");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ExternalTransactionDto>>() ?? new List<ExternalTransactionDto>();
        }

        public async Task<ExternalBudgetDto?> GetBudgetAsync(string token, string month)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"api/budgets/{month}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ExternalBudgetDto>();
        }
    }
}
