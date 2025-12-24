using ChineseAction.Api.Model;

public interface ICategoryService
{
    Task<Category> AddCategoryAsync(Category category);
    Task<Category?> GetCategoryByIdAsync(int id);
    Task<IEnumerable<Category>> GetAllCategories();
}