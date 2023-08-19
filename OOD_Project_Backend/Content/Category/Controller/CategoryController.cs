using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Content.Category.Business.Contexts;
using OOD_Project_Backend.Content.Category.Business.Contracts;
using OOD_Project_Backend.Core.Common.Authentication;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Content.Category.Controller;

[ApiController]
[Route("Category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    [Route("Add")]
    [Authorize]
    public async Task<Response> Add([FromBody] CategoryRequest categoryRequest)
    {
        return await _categoryService.CreateCategory(categoryRequest);
    }
    
}