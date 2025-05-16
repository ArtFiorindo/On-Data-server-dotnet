using Moq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Moq.Protected;
using OnData.Services;
using Xunit;

namespace OnData.Tests
{
    public class ExternalAuthServiceTests
    {
        [Fact]
        public async Task GetGoogleUserInfoAsync_ReturnsEmail_WhenSuccessful()
        {
            // Configuração do mock para simular uma resposta bem-sucedida da API do Google
            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"email\":\"test@example.com\"}")
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var service = new ExternalAuthService(httpClient);

            // Executa o método que será testado
            var email = await service.GetGoogleUserInfoAsync("fake-token");

            // Verifica se o resultado corresponde ao esperado
            Assert.Equal("test@example.com", email);
        }
    }
}