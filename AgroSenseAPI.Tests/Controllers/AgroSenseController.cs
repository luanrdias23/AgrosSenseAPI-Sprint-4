using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class AgroSenseControllerTests
{
    private readonly AgroSenseController _controller;
    private readonly Mock<FoodRecognitionModel> _recognitionModelMock;
    private readonly Mock<RecommendationService> _recommendationServiceMock;

    public AgroSenseControllerTests()
    {
        _recognitionModelMock = new Mock<FoodRecognitionModel>();
        _recommendationServiceMock = new Mock<RecommendationService>();
        _controller = new AgroSenseController(_recognitionModelMock.Object, _recommendationServiceMock.Object);
    }

    [Fact]
    public async Task GetRecommendations_ReturnsOkResult_WithExpectedResponse()
    {
        // Arrange
        var input = "Sugira melhorias para colheita";
        _recommendationServiceMock.Setup(service => service.GetRecommendationsAsync(input))
                                  .ReturnsAsync("Recomendação: use técnicas de irrigação controlada.");

        // Act
        var result = await _controller.GetRecommendations(input) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal("Recomendação: use técnicas de irrigação controlada.", result.Value);
    }
}
