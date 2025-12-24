using ChineseAction.Api.Model;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategories();
    Task<Category> AddCategoryAsync(Category category);
    Task<Category?> GetCategoryByIdAsync(int id);
}