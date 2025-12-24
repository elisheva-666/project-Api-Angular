using ChineseAction.Api.Model;

public interface IDonorService
{
    Task<IEnumerable<Donor>> GetAllDonorsAsync();
    Task<bool> DeleteDonorAsync(int id);
    Task<Donor?> UpdateDonorAsync(Donor donor);
    Task<Donor> AddDonorAsync(Donor donor);
    Task<IEnumerable<Donor>> GetFilteredDonorsAsync(string? name, string? giftName);
}