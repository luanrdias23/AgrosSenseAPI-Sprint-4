using AgrosSenseAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AgroSenseController : ControllerBase
{
    private readonly FoodRecognitionModel _recognitionModel;
    private readonly RecommendationService _recommendationService;

    public AgroSenseController(FoodRecognitionModel recognitionModel, RecommendationService recommendationService)
    {
        _recognitionModel = recognitionModel;
        _recommendationService = recommendationService;
    }

    [HttpPost("analyze")]
    public IActionResult AnalyzeFood([FromBody] FoodData foodData)
    {
        // Lógica para analisar a imagem e retornar resultados
        return Ok("Análise concluída.");
    }

    [HttpPost("recommend")]
    public async Task<IActionResult> GetRecommendations([FromBody] string input)
    {
        var recommendations = await _recommendationService.GetRecommendationsAsync(input);
        return Ok(recommendations);
    }
}
