using ChineseAction.Api.NewFolder;

namespace ChineseAction.Api.Repository
{
    public interface IGiftRepository
    {
        Task<IEnumerable<Gift>> GetAllAsync();
    }
}