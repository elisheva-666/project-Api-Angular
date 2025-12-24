using ChineseAction.Api.Model;

namespace ChineseAction.Api.Repository
{
    public interface IGiftRepository
    {
        Task<IEnumerable<Gift>> GetAllAsync();
        Task<bool> DeleteGift(int id);
        Task<Gift> AddGift(Gift gift);
        Task<IEnumerable<Gift>> GetSortedGiftsAsync(string? sortBy);
    }
}