using Microsoft.AspNetCore.Mvc;
using OnData.Services;

namespace OnData.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationController : ControllerBase
    {
        private readonly RecommendationService _recommendationService;

        public RecommendationController(RecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        // Endpoint para obter recomendações de produtos para um usuário específico
        [HttpGet("{userId}")]
        public IActionResult GetRecommendations(int userId)
        {
            var recommendations = _recommendationService.RecommendProducts(userId);
            return Ok(recommendations);
        }
    }
}