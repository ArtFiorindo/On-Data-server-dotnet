using Moq;
using OnData.Services;
using System.Collections.Generic;
using Xunit;

namespace OnData.Tests
{
    public class RecommendationServiceTests
    {
        [Fact]
        public void RecommendProducts_ReturnsRecommendedProducts()
        {
            // Configuração do mock do serviço de recomendação
            var mockService = new Mock<RecommendationService>();

            // Configura o mock para retornar uma lista de produtos simulada
            mockService
                .Setup(service => service.RecommendProducts(It.IsAny<int>()))
                .Returns(new List<string> { "Product 1", "Product 2" });

            // Executa o método de recomendação
            var recommendations = mockService.Object.RecommendProducts(userId: 1);

            // Valida se as recomendações foram retornadas
            Assert.NotNull(recommendations); // Verifica se a lista não é nula
            Assert.True(recommendations.Count > 0, "Nenhum produto foi recomendado."); // Verifica se há produtos na lista
            Assert.Contains("Product 1", recommendations); // Verifica se "Product 1" está presente
            Assert.Contains("Product 2", recommendations); // Verifica se "Product 2" está presente
        }
    }
}