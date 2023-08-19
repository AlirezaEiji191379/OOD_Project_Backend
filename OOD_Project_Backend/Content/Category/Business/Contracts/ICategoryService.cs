using OOD_Project_Backend.Content.Category.Business.Contexts;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Content.Category.Business.Contracts;

public interface ICategoryService
{
    Task<Response> CreateCategory(CategoryRequest request);
    Task<Response> UpdateCategory(CategoryRequest request);
    Task<Response> DeleteCategory(CategoryRequest request);
    Task<Response> GetCategory(CategoryRequest request);
    Task<Response> GetCategories(CategoryRequest request);
}