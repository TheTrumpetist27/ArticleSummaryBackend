using Core.Models;
using System.Net.Http.Json;

namespace Core.Services
{
    public class TextSummaryService : ITextSummaryService
    {
        private readonly HttpClient _httpClient;
        public TextSummaryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SummarizeTextAsync(string content)
        {
            var request = new { text = content };
            // http://127.0.0.1:8000/summarize
            var response = await _httpClient.PostAsJsonAsync("http://host.docker.internal:8000/summarize", request);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadFromJsonAsync<SummaryResponse>();
            return json?.Summary ?? throw new Exception("Geen summary ontvangen");
        }
    }
}
