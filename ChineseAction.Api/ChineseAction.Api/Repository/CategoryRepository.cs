using ChineseAction.Api.Data;
using ChineseAction.Api.Model;
using Microsoft.EntityFrameworkCore;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    //ctor
    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    //קבלת כל הקטגוריןת
    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        return await _context.Categories.ToListAsync();
    }

    //יצירת קטגוריה
    public async Task<Category> AddCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    //קבלת קטגוריה ורשימת מתנות ע"פ id
    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }
}