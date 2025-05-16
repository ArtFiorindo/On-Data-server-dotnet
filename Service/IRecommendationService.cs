using System.Collections.Generic;

namespace OnData.Services
{
    public interface IRecommendationService
    {
        IEnumerable<string> RecommendProducts(int userId);
    }
}