using BestStories.WebAPI.Models;
using BestStories.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BestStories.WebAPI.UnitTests
{
    public class StoriesControllerTests
    {
        private readonly Mock<ILogger<StoriesController>> _mockLogger;
        private readonly Mock<IHackerNewsService> _mockService;
        private readonly StoriesController _controller;

        public StoriesControllerTests()
        {
            //Arrange
            _mockLogger = new Mock<ILogger<StoriesController>>();
            _mockService = new Mock<IHackerNewsService>();
            _controller = new StoriesController(_mockLogger.Object, _mockService.Object);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetBestStories_InvalidCount_ReturnsBadRequest(int count)
        {
            //Act
            var result = await _controller.GetBestStories(count);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData(10)]
        public async Task GetBestStories_ValidCount_ReturnsStories(int count)
        {
            //Arrange
            var fakeStories = new List<Story>
            {
                new Story { Id = 1, Title = "Story 1", Score = 100 },
                new Story { Id = 2, Title = "Story 2", Score = 200 },
                new Story { Id = 3, Title = "Story 3", Score = 300 },
                new Story { Id = 4, Title = "Story 4", Score = 400 },
                new Story { Id = 5, Title = "Story 5", Score = 500 },
                new Story { Id = 6, Title = "Story 6", Score = 600 },
                new Story { Id = 7, Title = "Story 7", Score = 700 },
                new Story { Id = 8, Title = "Story 8", Score = 800 },
                new Story { Id = 9, Title = "Story 9", Score = 900 },
                new Story { Id = 10, Title = "Story 10", Score = 1000 }
            };

            _mockService.Setup(_ => _.GetBestStories(count)).ReturnsAsync(fakeStories);

            //Act
            var result = await _controller.GetBestStories(count);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedStories = Assert.IsType<List<Story>>(okResult.Value);
            Assert.Equal(count, returnedStories.Count);
            Assert.Equal(fakeStories.Count, returnedStories.Count);
        }
    }
}