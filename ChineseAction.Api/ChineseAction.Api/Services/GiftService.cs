using ChineseAction.Api.Model;
using ChineseAction.Api.Repository;

namespace ChineseAction.Api.Servies
{
    public class GiftService: IGiftService
    {

        private readonly IGiftRepository _repository;

        public GiftService(IGiftRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Gift>> GetAllGift()
        {
            return (IEnumerable<Gift>) await _repository.GetAllAsync();
        }

        public async Task<bool> DeleteGift(int id)
        {
            return await _repository.DeleteGift(id);
        }

        public async Task<Gift> AddGift(Gift gift)
        {
            return await _repository.AddGift(gift);
        }

        public async Task<IEnumerable<Gift>> GetSortedGiftsAsync(string? sortBy)
        {
            // קריאה לריפוזיטורי לקבלת רשימת מתנות ממוין
            return await _repository.GetSortedGiftsAsync(sortBy);
        }
    }
}
