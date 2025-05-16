using System.IO;
using Xunit;

public class RecommendationServiceModelTests
{
    [Fact]
    public void RecommendProducts_ModelReturnsValidPredictions()
    {
        // Caminho absoluto para o arquivo do modelo
        string modelPath = Path.Combine(Directory.GetCurrentDirectory(), "recommendation-model.zip");

        // Verifica se o modelo existe antes de executar o teste
        Assert.True(File.Exists(modelPath), $"O arquivo do modelo não foi encontrado: {modelPath}");

        var recommendationService = new RecommendationService(modelPath);

        int userId = 1;
        var recommendations = recommendationService.RecommendProducts(userId);

        Assert.NotNull(recommendations);
        Assert.True(recommendations.Count() > 0, "O modelo não retornou nenhuma recomendação.");
    }
}