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

    [HttpDelete]
    [Route("Delete/{categoryId}")]
    [Authorize]
    public async Task<Response> Delete(int categoryId)
    {
        return await _categoryService.DeleteCategory(new CategoryRequest{Id = categoryId});
    }

    [HttpPut]
    [Route("Update")]
    [Authorize]
    public async Task<Response> Update([FromBody] CategoryRequest categoryRequest)
    {
        return await _categoryService.UpdateCategory(categoryRequest);
    }

    [HttpGet]
    [Route("Get/{categoryId}")]
    [Authorize]
    public async Task<Response> Get(int categoryId)
    {
        return await _categoryService.GetCategory(new CategoryRequest { Id = categoryId});
    }

    [HttpGet]
    [Route("GetAll/{channelId}")]
    [Authorize]
    public async Task<Response> GetAll(int channelId)
    {
        return await _categoryService.GetCategories(new CategoryRequest { ChannelId = channelId});
    }
}