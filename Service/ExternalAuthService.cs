using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OnData.Services
{
    public class ExternalAuthService
    {
        private readonly HttpClient _httpClient;

        public ExternalAuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Obtém as informações do usuário do Google usando o token de acesso
        public async Task<string> GetGoogleUserInfoAsync(string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync("https://www.googleapis.com/oauth2/v2/userinfo");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Falha ao recuperar informações do usuário do Google.");
            }

            var userInfo = await response.Content.ReadFromJsonAsync<GoogleUserInfo>();
            return userInfo?.Email ?? string.Empty;
        }

        private class GoogleUserInfo
        {
            public string Email { get; set; }
        }
    }
}