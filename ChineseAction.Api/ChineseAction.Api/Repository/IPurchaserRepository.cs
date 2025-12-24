using ChineseAction.Api.Model;

public interface IPurchaserRepository
{
    Task<Purchaser> AddPurchaserAsync(Purchaser purchaser);
    Task<Purchaser?> GetPurchaserByEmailAndPasswordAsync(string email, string password);
    Task<IEnumerable<Purchaser>> GetAllPurchasersAsync();
    Task<Purchaser?> GetPurchaserByIdAsync(int id);
}