using BestStories.WebAPI.Constants;
using BestStories.WebAPI.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace BestStories.WebAPI.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly ILogger<HackerNewsService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public HackerNewsService(ILogger<HackerNewsService> logger, IHttpClientFactory httpClientFactory, IMemoryCache cache)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient();
            _cache = cache;

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<Story>> GetBestStories(int count)
        {
            var storyIds = await GetBestStoryIds(_httpClient);
            var stories = await Task.WhenAll(storyIds.Take(count).Select(_ => GetStoryDetails(_httpClient, _)));
            return stories.Where(_ => _ != null).OrderByDescending(_ => _.Score);
        }

        private async Task<int[]> GetBestStoryIds(HttpClient client)
        {
            return await _cache.GetOrCreateAsync("BestStoryIds", async _ =>
            {
                _.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                _logger.LogInformation("Fetching best story IDs from Hacker News API.");
                var response = await client.GetStringAsync(HackerNewsUri.BestStoriesUrl);
                return JsonSerializer.Deserialize<int[]>(response, _jsonSerializerOptions) ?? new int[0];
            });
        }

        private async Task<Story> GetStoryDetails(HttpClient client, int id)
        {
            return await _cache.GetOrCreateAsync($"Story_{id}", async _ =>
            {
                _.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                _logger.LogInformation($"Fetching story details for ID: {id} from Hacker News API.");
                var response = await client.GetStringAsync(string.Format(HackerNewsUri.StoryDetailsUrl, id));
                return JsonSerializer.Deserialize<Story>(response, _jsonSerializerOptions);
            });
        }
    }
}
