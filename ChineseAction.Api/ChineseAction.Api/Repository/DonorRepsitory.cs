using ChineseAction.Api.Data;
using ChineseAction.Api.Model;
using Microsoft.EntityFrameworkCore;

public class DonorRepository : IDonorRepository
{
    private readonly ApplicationDbContext _context;

    public DonorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Donor>> GetAllDonorsAsync()
    {
        return await _context.Donors.ToListAsync();
    }

    public async Task<Donor> AddDonorAsync(Donor donor)
    {
        _context.Donors.Add(donor);
        await _context.SaveChangesAsync();
        return donor;
    }

    public async Task<bool> DeleteDonorAsync(int id)
    {
        var donor = await _context.Donors.FindAsync(id);
        if (donor == null)
        {
            return false;
        }

        _context.Donors.Remove(donor);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Donor?> UpdateDonorAsync(Donor donor)
    {
        var existingDonor = await _context.Donors.FindAsync(donor.Id);
        if (existingDonor == null)
        {
            return null;
        }

        existingDonor.Name = donor.Name;
        existingDonor.Email = donor.Email;
        existingDonor.Phone = donor.Phone;

        await _context.SaveChangesAsync();
        return existingDonor;
    }

    public async Task<IEnumerable<Donor>> GetFilteredDonorsAsync(string? name, string? email)
    {
        // בדיקה אם שני הפרמטרים נשלחו יחד
        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email))
        {
            throw new ArgumentException("Cannot filter by both name and email simultaneously.");
        }

        // שאילתת בסיס לקבלת כל התורמים
        var query = _context.Donors.AsQueryable();

        // סינון לפי שם התורם
        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(d => d.Name.Contains(name));
        }

        // סינון לפי מייל התורם
        if (!string.IsNullOrEmpty(email))
        {
            query = query.Where(d => d.Email.Contains(email));
        }

        // החזרת הרשימה המסוננת
        return await query.ToListAsync();
    }
}
