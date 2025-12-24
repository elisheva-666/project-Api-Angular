using ChineseAction.Api.Data;
using ChineseAction.Api.Model;
using Microsoft.EntityFrameworkCore;

public class PurchaserRepository : IPurchaserRepository
{
    private readonly ApplicationDbContext _context;

    public PurchaserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Purchaser> AddPurchaserAsync(Purchaser purchaser)
    {
        _context.Purchasers.Add(purchaser);
        await _context.SaveChangesAsync();
        return purchaser;
    }

    public async Task<Purchaser?> GetPurchaserByEmailAndPasswordAsync(string email, string password)
    {
        return await _context.Purchasers
            .FirstOrDefaultAsync(p => p.Email == email && p.Password == password);
    }

    public async Task<IEnumerable<Purchaser>> GetAllPurchasersAsync()
    {
        return await _context.Purchasers.ToListAsync();
    }

    public async Task<Purchaser?> GetPurchaserByIdAsync(int id)
    {
        return await _context.Purchasers.FindAsync(id);
    }
}