using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class RecommendationService
{
    private readonly HttpClient _httpClient;

    public RecommendationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("Authorization", "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");
    }

    public async Task<string> GetRecommendationsAsync(string input)
    {
        var requestBody = new { prompt = input, max_tokens = 100 };
        var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://api.openai.com/v1/completions", content);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        var jsonResponse = JsonConvert.DeserializeObject<dynamic>(result);
        return jsonResponse.choices[0].text.ToString();
    }

    internal async Task GenerateTextAsync(string prompt)
    {
        throw new NotImplementedException();
    }
}
