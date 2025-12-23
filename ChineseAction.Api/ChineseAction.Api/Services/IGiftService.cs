using ChineseAction.Api.NewFolder;

namespace ChineseAction.Api.Servies
{
    public interface IGiftService
    {
        Task<IEnumerable<Gift>> GetAllGift();
    }
}