using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Channel.ChannelCore.Business.Contracts;
using OOD_Project_Backend.Content.Category.DataAccess.Repository.Contracts;
using OOD_Project_Backend.Content.ContentCore.Business.Contexts;
using OOD_Project_Backend.Content.ContentCore.Business.Contracts;
using OOD_Project_Backend.Content.ContentCore.Business.Creation.Contracts;
using OOD_Project_Backend.Content.ContentCore.Business.Models.Contract;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.User.Business.Contracts;

namespace OOD_Project_Backend.Content.ContentCore.Business.Services;

public class DefaultContentService : IContentService
{
    private readonly IContentRepository _contentRepository;
    private readonly IContentMetaDataRepository _contentMetadataRepository;
    private readonly IContentCreation _contentCreation;
    private readonly IUserFacade _userFacade;
    private readonly IChannelFacade _channelFacade;
    private readonly IContentModelProvider _contentModelProvider;
    private readonly IInteractionRepository _interactionRepository;
    private readonly ICategoryRepository _categoryRepository;

    public DefaultContentService(IContentRepository contentRepository,
        IContentMetaDataRepository contentMetadataRepository,
        IContentCreation contentCreation,
        IUserFacade userFacade,
        IChannelFacade channelFacade,
        IContentModelProvider contentModelProvider,
        IInteractionRepository interactionRepository, 
        ICategoryRepository categoryRepository)
    {
        _contentRepository = contentRepository;
        _contentMetadataRepository = contentMetadataRepository;
        _contentCreation = contentCreation;
        _userFacade = userFacade;
        _channelFacade = channelFacade;
        _contentModelProvider = contentModelProvider;
        _interactionRepository = interactionRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Response> Add(ContentCreationRequest request)
    {
        try
        {
            var userId = _userFacade.GetCurrentUserId();
            if (await _channelFacade.IsChannelAdminOrOwner(userId, request.ChannelId) == false)
            {
                return new Response(403, new { Message = "only admin or owner can add content to this channel!" });
            }

            var contentId = await _contentCreation.Generate(request);
            return new Response(201, new { Message = contentId });
        }
        catch (Exception e)
        {
            return new Response(400, new { Message = "add content failed!" });
        }
    }

    public async Task<Response> GetChannelContentsMetadata(int channelId)
    {
        try
        {
            var userId = _userFacade.GetCurrentUserId();
            var contentDtos = await _contentMetadataRepository.FindByChannelIdIncludeContent(channelId);
            foreach (var contentDto in contentDtos)
            {
                contentDto.IsLiked = await _interactionRepository.IsUserLikedContent(userId, contentDto.ContentId);
            }

            return new Response(200, new { Message = contentDtos });
        }
        catch (Exception e)
        {
            return new Response(400, "could not returve");
        }
    }

    public async Task<Response> Show(int contentId)
    {
        try
        {
            var contentMetaDataEntity = await _contentMetadataRepository.FindByChannelId(contentId);
            var userId = _userFacade.GetCurrentUserId();
            var hasAccess = !contentMetaDataEntity.Premium ||
                            await _channelFacade.CheckAccessToContent(userId, contentMetaDataEntity.ChannelId,
                                contentId);
            var contentModel = _contentModelProvider.GetContentModel(contentMetaDataEntity.ContentType);
            var toR = hasAccess
                ? await contentModel.ShowNormal(contentId)
                : await contentModel.ShowPreview(contentId);
            toR.IsLiked = await _interactionRepository.IsUserLikedContent(userId, contentId);
            return new Response(200, new { Message = toR });
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<Response> Delete(int contentId)
    {
        try
        {
            var contentMetaDataEntity = await _contentMetadataRepository.FindByChannelId(contentId);
            var userId = _userFacade.GetCurrentUserId();
            var isAdminOrOwner = await _channelFacade.IsChannelAdminOrOwner(userId, contentMetaDataEntity.ChannelId);
            if (!isAdminOrOwner)
            {
                return new Response(403, new { Message = "only admins and owners can remove contents of a channel" });
            }

            var contentModel = _contentModelProvider.GetContentModel(contentMetaDataEntity.ContentType);
            await contentModel.Delete(contentId);
            return new Response(200, new { Message = "deleted successfully!" });
        }
        catch (Exception e)
        {
            return new Response(403,
                new { Message = "the content is not found or you are not admin or owner of channel!" });
        }
    }

    public async Task<Response> Update(ContentUpdateRequest updateRequest)
    {
        try
        {
            var contentMetaDataEntity = await _contentMetadataRepository.FindByContentId(updateRequest.ContentId);
            var userId = _userFacade.GetCurrentUserId();
            var isAdminOrOwner = await _channelFacade.IsChannelAdminOrOwner(userId, contentMetaDataEntity.ChannelId);
            if (!isAdminOrOwner)
            {
                return new Response(403, new { Message = "only admins and owners can update contents of a channel" });
            }

            if (updateRequest.CategoryId != null)
            {
                var category = await _categoryRepository.GetById(updateRequest.CategoryId.Value);
                if (category.ChannelId != contentMetaDataEntity.ChannelId)
                {
                    return new Response(400, new {Message = "the category is not in the channel!"});
                }
            }
            await _contentCreation.UpdateContent(updateRequest);
            return new Response(200, new { Message = "content has been updated successfully!" });
        }
        catch (Exception exception)
        {
            return new Response(403, new { Message = "update of content failed!" });
        }
    }

    public async Task<Response> AddInteraction(int contentId)
    {
        try
        {
            var userId = _userFacade.GetCurrentUserId();
            await _interactionRepository.Create(new InteractionEntity()
            {
                UserId = userId,
                ContentId = contentId
            });
            await _interactionRepository.SaveChangesAsync();
            return new Response(200, new { Message = "interaction was added!" });
        }
        catch (Exception e)
        {
            return new Response(400, new { Message = "interaction add failed!" });
        }
    }


    public async Task<Response> DeleteInteraction(int contentId)
    {
        try
        {
            var userId = _userFacade.GetCurrentUserId();
            _interactionRepository.Delete(new InteractionEntity()
            {
                UserId = userId,
                ContentId = contentId
            });
            await _interactionRepository.SaveChangesAsync();
            return new Response(200, new { Message = "interaction was deleted!" });
        }
        catch (Exception e)
        {
            return new Response(400, new { Message = "interaction remove failed!" });
        }
    }
}