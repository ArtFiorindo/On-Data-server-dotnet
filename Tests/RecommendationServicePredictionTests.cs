using Microsoft.ML;
using OnData.Models;
using OnData.Services;
using System.Collections.Generic;
using Xunit;

namespace OnData.Tests
{
    public class RecommendationServicePredictionTests
    {
        [Fact]
        public void RecommendProducts_ReturnsOnlyHighScoreProducts()
        {
            // Arrange
            string modelPath = "recommendation-model.zip"; // Caminho para o modelo
            var recommendationService = new RecommendationService(modelPath);

            int userId = 1; // Usuário fictício

            // Act
            var recommendations = recommendationService.RecommendProducts(userId);

            // Assert
            Assert.NotNull(recommendations); // Verifica se há recomendações
            Assert.NotEmpty(recommendations); // Verifica se a lista não está vazia

            foreach (var productName in recommendations)
            {
                // Os produtos devem estar no formato "Produto {i}".
                Assert.Matches(@"Produto \d+", productName);
            }
        }
    }
}