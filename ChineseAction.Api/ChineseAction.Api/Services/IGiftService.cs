using ChineseAction.Api.Model;

namespace ChineseAction.Api.Servies
{
    public interface IGiftService
    {
        Task<IEnumerable<Gift>> GetAllGift();
        Task<bool> DeleteGift(int id);

        Task<Gift> AddGift(Gift gift);
        Task<IEnumerable<Gift>> GetSortedGiftsAsync(string? sortBy);
    }
}