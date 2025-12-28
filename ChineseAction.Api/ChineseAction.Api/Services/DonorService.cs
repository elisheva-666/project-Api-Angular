using ChineseAction.Api.Model;
using ChineseAction.Api.Repository;

public class DonorService : IDonorService
{
    private readonly IDonorRepository _donorRepository;

    public DonorService(IDonorRepository donorRepository)
    {
        _donorRepository = donorRepository;
    }

    public async Task<IEnumerable<Donor>> GetAllDonorsAsync()
    {
        return await _donorRepository.GetAllDonorsAsync();
    }

    public async Task<bool> DeleteDonorAsync(int id)
    {
        _log.Information("Deleting donor with ID: {DonorId}", id);
        return await _donorRepository.DeleteDonorAsync(id);
    }

    public async Task<Donor?> UpdateDonorAsync(Donor donor)
    {
        return await _donorRepository.UpdateDonorAsync(donor);
    }

    public async Task<Donor> AddDonorAsync(Donor donor)
    {
        return await _donorRepository.AddDonorAsync(donor);
    }

    public async Task<IEnumerable<Donor>> GetFilteredDonorsAsync(string? name, string? email)
    {
        // קריאה לריפוזיטורי לקבלת רשימת תורמים מסוננת
        return await _donorRepository.GetFilteredDonorsAsync(name, email);
    }
}