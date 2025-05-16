using OnData.Services;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace OnData.Tests
{
    public class RecommendationServiceModelTest
    {
        [Fact]
        public void RecommendProducts_ModelReturnsValidPredictions()
        {
            // Caminho absoluto ou relativo para o arquivo do modelo
            string modelPath = "recommendation-model.zip";

            // Verifica se o modelo existe antes de executar o teste
            Assert.True(File.Exists(modelPath), $"O arquivo do modelo não foi encontrado: {modelPath}");

            var recommendationService = new RecommendationService(modelPath);

            int userId = 1; // Usuário fictício
            var recommendations = recommendationService.RecommendProducts(userId);

            // Verifica se há recomendações válidas
            Assert.NotNull(recommendations);
            Assert.True(recommendations.Count() > 0, "O modelo não retornou nenhuma recomendação.");
        }
    }
}