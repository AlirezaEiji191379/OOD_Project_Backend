using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Channel.ChannelCore.Business.Contracts;
using OOD_Project_Backend.Content.Category.Business.Contexts;
using OOD_Project_Backend.Content.Category.Business.Contracts;
using OOD_Project_Backend.Content.Category.DataAccess.Entities;
using OOD_Project_Backend.Content.Category.DataAccess.Repository.Contracts;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.User.Business.Contracts;

namespace OOD_Project_Backend.Content.Category.Business.Services;

public class DefaultCategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IChannelFacade _channelFacade;
    private readonly IContentMetaDataRepository _contentMetaDataRepository;
    private readonly IUserFacade _userFacade;

    public DefaultCategoryService(
        IChannelFacade channelFacade,
        IUserFacade userFacade,
        ICategoryRepository categoryRepository,
        IContentMetaDataRepository contentMetaDataRepository)
    {
        _channelFacade = channelFacade;
        _userFacade = userFacade;
        _categoryRepository = categoryRepository;
        _contentMetaDataRepository = contentMetaDataRepository;
    }

    public async Task<Response> CreateCategory(CategoryRequest request)
    {
        var currentUserId = _userFacade.GetCurrentUserId();
        try
        {
            var isChannelAdminOrOwner = await _channelFacade.IsChannelAdminOrOwner(currentUserId, request.ChannelId);

            if (!isChannelAdminOrOwner)
                return new Response((int)HttpStatusCode.Unauthorized,
                    new { Message = "You are not authorized to create categories!" });

            var category = new CategoryEntity
            {
                Title = request.Title,
                ChannelId = request.ChannelId
            };
            await _categoryRepository.Create(category);
            await _categoryRepository.SaveChangesAsync();

            return new Response((int)HttpStatusCode.Created,
                new { Message = category.Id });
        }
        catch (Exception)
        {
            return new Response((int)HttpStatusCode.BadRequest, new { Message = "Something Went Wrong!" });
        }
    }

    public async Task<Response> DeleteCategory(CategoryRequest request)
    {
        var currentUserId = _userFacade.GetCurrentUserId();
        try
        {
            var category = await _categoryRepository.GetById(request.Id);
            if (category == null)
            {
                return new Response((int)HttpStatusCode.NotFound, new { Message = "Category Not Found" });
            }
            
            var isChannelAdminOrOwner = await _channelFacade.IsChannelAdminOrOwner(currentUserId, category.ChannelId);
            
            if (!isChannelAdminOrOwner)
                return new Response((int)HttpStatusCode.Unauthorized,
                    new { Message = "You are not authorized to delete categories!" });
            

            var contents = await _contentMetaDataRepository.FindByCategoryId(category.Id);

            foreach (var content in contents)
            {
                content.CategoryId = null;
                _contentMetaDataRepository.Update(content);
            }

            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();
            return new Response((int)HttpStatusCode.OK, new { Message = "Category Deleted Successfully" });
        }
        catch (Exception)
        {
            return new Response((int)HttpStatusCode.BadRequest, new { Message = "Something Went Wrong!" });
        }
    }

    public async Task<Response> UpdateCategory(CategoryRequest request)
    {
        var currentUserId = _userFacade.GetCurrentUserId();
        try
        {
            var category = await _categoryRepository.GetById(request.Id);
            if (category == null)
            {
                return new Response((int)HttpStatusCode.NotFound, new { Message = "Category Not Found" });
            }

            var isChannelAdminOrOwner = await _channelFacade.IsChannelAdminOrOwner(currentUserId, category.ChannelId);

            if (!isChannelAdminOrOwner)
                return new Response((int)HttpStatusCode.Unauthorized,
                    new { Message = "You are not authorized to update categories!" });


            var duplicateCategory = await _categoryRepository.GetByName(category.ChannelId, request.Title);

            if (duplicateCategory != null)
            {
                return new Response((int)HttpStatusCode.BadRequest,
                    new { Message = "Category with this title already exists" });
            }

            category.Title = request.Title;
            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();

            return new Response((int)HttpStatusCode.OK, new { Message = "Category Updated Successfully" });
        }
        catch (Exception)
        {
            return new Response((int)HttpStatusCode.BadRequest, new { Message = "Something Went Wrong!" });
        }
    }

    public async Task<Response> GetCategory(CategoryRequest request)
    {
        try
        {
            var category = await _categoryRepository.GetById(request.Id);
            return category == null
                ? new Response((int)HttpStatusCode.NotFound, new { Message = "Category Not Found" })
                : new Response((int)HttpStatusCode.OK, new { Message = category });
        }
        catch (Exception)
        {
            return new Response((int)HttpStatusCode.BadRequest, new { Message = "Something Went Wrong!" });
        }
    }

    public async Task<Response> GetCategories(CategoryRequest request)
    {
        try
        {
            var categories = await _categoryRepository.FindByChannelId(request.ChannelId);
            return new Response((int)HttpStatusCode.OK, new { Message = categories });
        }
        catch (Exception)
        {
            return new Response((int)HttpStatusCode.BadRequest, new { Message = "Something Went Wrong!" });
        }
    }
}