using Microsoft.ML;
using OnData.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnData.Services
{
    public class RecommendationService
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;

        public RecommendationService()
        {
            _mlContext = new MLContext();
            _model = LoadModel();
        }

        // Gera recomendações de produtos com base no ID do usuário
        public IEnumerable<string> RecommendProducts(int userId)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<UserProductRating, ProductPrediction>(_model);

            var recommendedProducts = new List<string>();

            // Exemplo: prevê classificações para produtos com IDs de 1 a 10
            for (int i = 1; i <= 10; i++)
            {
                var prediction = predictionEngine.Predict(new UserProductRating { UserId = userId, ProductId = i });
                if (prediction.Score > 3.5) // Exemplo: recomenda produtos com pontuação > 3.5
                {
                    recommendedProducts.Add($"Produto {i}");
                }
            }

            return recommendedProducts;
        }

        // Carrega o modelo de recomendação treinado
        private ITransformer LoadModel()
        {
            return _mlContext.Model.Load("recommendation-model.zip", out _);
        }
    }

    // Classe que representa os dados de entrada para o modelo
    public class UserProductRating
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }

    // Classe que representa a previsão gerada pelo modelo
    public class ProductPrediction
    {
        public float Score { get; set; }
    }
}