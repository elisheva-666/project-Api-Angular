using ChineseAction.Api.NewFolder;
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
    }
}
