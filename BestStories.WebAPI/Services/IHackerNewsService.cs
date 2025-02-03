using BestStories.WebAPI.Models;

namespace BestStories.WebAPI.Services
{
    public interface IHackerNewsService
    {
        Task<IEnumerable<Story>> GetBestStories(int count);
    }
}
