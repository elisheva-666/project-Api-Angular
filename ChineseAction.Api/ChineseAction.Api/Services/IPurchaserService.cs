using ChineseAction.Api.Model;

public interface IPurchaserService
{
    Task<Purchaser> RegisterAsync(Purchaser purchaser);
    Task<Purchaser?> LoginAsync(string email, string password); 
    Task<IEnumerable<Purchaser>> GetAllPurchasersAsync();
    Task<Purchaser?> GetPurchaserByIdAsync(int id);
}