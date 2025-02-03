using BestStories.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class StoriesController : ControllerBase
{
    private readonly ILogger<StoriesController> _logger;
    private readonly IHackerNewsService _hackerNewsService;

    public StoriesController(ILogger<StoriesController> logger, IHackerNewsService hackerNewsService)
    {
        _logger = logger;
        _hackerNewsService = hackerNewsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBestStories([FromQuery] int count = 10)
    {
        _logger.LogInformation($"{nameof(GetBestStories)} endpoint has received a request.");

        if (count <= 0)
        {
            _logger.LogError("An error has occured while sending an invalid story count.");
            return BadRequest("An invalid story count. Please try with a valid count (e.g. 10)");
        }

        _logger.LogInformation($"Preparing a response from Hacker News API...");
        var stories = await _hackerNewsService.GetBestStories(count);
        _logger.LogInformation($"{nameof(GetBestStories)} endpoint has returned a response from Hacker News API.");

        return Ok(stories);
    }
}