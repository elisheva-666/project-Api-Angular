using ChineseAction.Api.Data;
using ChineseAction.Api.NewFolder;
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

        public async Task<IEnumerable<Gift>> GetAllAsync()
        {
            return await _context.Gifts.ToListAsync();
        }
    }
}
