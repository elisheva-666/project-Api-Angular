using ChineseAction.Api.Model;
using ChineseAction.Api.Repository;

public class PurchaserService : IPurchaserService
{
    private readonly IPurchaserRepository _purchaserRepository;

    public PurchaserService(IPurchaserRepository purchaserRepository)
    {
        _purchaserRepository = purchaserRepository;
    }

    public async Task<Purchaser> RegisterAsync(Purchaser purchaser)
    {
        return await _purchaserRepository.AddPurchaserAsync(purchaser);
    }

    public async Task<Purchaser?> LoginAsync(string email, string password)
    {
        return await _purchaserRepository.GetPurchaserByEmailAndPasswordAsync(email, password);
    }

    public async Task<IEnumerable<Purchaser>> GetAllPurchasersAsync()
    {
        return await _purchaserRepository.GetAllPurchasersAsync();
    }

    public async Task<Purchaser?> GetPurchaserByIdAsync(int id)
    {
        return await _purchaserRepository.GetPurchaserByIdAsync(id);
    }
}