using ChineseAction.Api.Data;
using ChineseAction.Api.Model;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChineseAction.Api.Repository
{
    public class GiftRepository : IGiftRepository
    {
        private readonly ApplicationDbContext _context;

        public GiftRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //מחזיר את כל המתנות
        public async Task<IEnumerable<Gift>> GetAllAsync()
        {
            return await _context.Gifts.ToListAsync();
        }

        //מחיקת מתנה לתורם
        public async Task<bool> DeleteGift(int id)
        {
            var gift = await _context.Gifts.FindAsync(id);
            if (gift == null)
                return false;

            _context.Gifts.Remove(gift);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Gift> AddGift(Gift gift)
        {
            _context.Gifts.Add(gift);
            await _context.SaveChangesAsync();
            return gift;
        }

        public async Task<IEnumerable<Gift>> GetSortedGiftsAsync(string? sortBy)
        {
            // שאילתת בסיס לקבלת כל המתנות
            var query = _context.Gifts.AsQueryable();

            // מיון לפי מחיר
            if (sortBy?.ToLower() == "price")
            {
                query = query.OrderBy(g => g.TicketPrice);
            }
            // מיון לפי קטגוריה
            else if (sortBy?.ToLower() == "category")
            {
                query = query.OrderBy(g => g.Category.Name);
            }

            // החזרת הרשימה
            return await query.ToListAsync();
        }
    }
}
