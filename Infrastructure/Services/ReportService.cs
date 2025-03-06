using System.Text;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class ReportService
    {
        private readonly HttpClient httpClient;
        public ReportService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<string> GetTokenAsync()
        {
            var requestData = new { username = "ucand0021", password = "yNDVARG80sr@dDPc2yCT!" };
            var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://api.toka.com.mx/candidato/api/login/authenticate", content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(responseBody);
            return jsonDoc.RootElement.GetProperty("token").GetString();
        }

        //public async Task<IEnumerable<Cliente>> GetClientesAsync()
        //{
        //    string token = await GetTokenAsync();
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //    var response = await httpClient.GetAsync("https://api.toka.com.mx/candidato/api/customers");
        //    response.EnsureSuccessStatusCode();

        //    var responseBody = await response.Content.ReadAsStringAsync();
        //    return JsonSerializer.Deserialize<IEnumerable<Cliente>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //}
    }
}
