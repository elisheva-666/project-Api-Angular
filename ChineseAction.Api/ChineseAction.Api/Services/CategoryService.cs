using ChineseAction.Api.Model;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> AddCategoryAsync(Category category)
    {
        return await _categoryRepository.AddCategoryAsync(category);
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await _categoryRepository.GetCategoryByIdAsync(id);
    }

    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        return await _categoryRepository.GetAllCategories();
    }
}