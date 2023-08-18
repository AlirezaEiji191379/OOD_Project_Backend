using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Content.ContentCore.Business.Contracts;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories;

public class ContentMetaDataRepository : BaseRepository<ContentMetaDataEntity>, IContentMetaDataRepository
{
    private readonly AppDbContext _appDbContext;

    public ContentMetaDataRepository(AppDbContext dbContext) : base(dbContext)
    {
        _appDbContext = dbContext;
    }


    public Task<ContentMetaDataEntity> FindByContentId(int contentId)
    {
        return _appDbContext.ContentMetaDatas.Where(x => x.ContentId == contentId).SingleAsync();
    }

    public Task<List<ContentDto>> FindByChannelIdIncludeContent(int channelId)
    {
        return _appDbContext
            .ContentMetaDatas
            .Where(x => x.ChannelId == channelId)
            .Include(x => x.Content)
            .Select(x => new ContentDto()
            {
                ContentId = x.ContentId,
                Description = x.Content.Description,
                CreatedAt = x.Content.CreatedAt,
                Price = x.Price,
                IsPremium = x.Premium,
                Title = x.Content.Title,
                Type = x.ContentType.ToString(),
                FileName = x.FileName
            })
            .ToListAsync();
    }

    public Task<List<ContentDto>> FindByChannelIdIncludeContentAndLikeTitle(int channelId, string title)
    {
        return _appDbContext
            .ContentMetaDatas
            .Where(x => x.ChannelId == channelId)
            .Include(x => x.Content)
            .Where(x => EF.Functions.ILike(x.Content.Title,$"%{title}%"))
            .Select(x => new ContentDto()
            {
                ContentId = x.ContentId,
                Description = x.Content.Description,
                CreatedAt = x.Content.CreatedAt,
                Price = x.Price,
                IsPremium = x.Premium,
                Title = x.Content.Title,
                Type = x.ContentType.ToString(),
                FileName = x.FileName
            })
            .ToListAsync();
    }

    public Task<List<ContentDto>> FindByLikeTitle(string title)
    {
        return _appDbContext
            .ContentMetaDatas
            .Include(x => x.Content)
            .Where(x => EF.Functions.ILike(x.Content.Title,$"%{title}%"))
            .Select(x => new ContentDto()
            {
                ContentId = x.ContentId,
                Description = x.Content.Description,
                CreatedAt = x.Content.CreatedAt,
                Price = x.Price,
                IsPremium = x.Premium,
                Title = x.Content.Title,
                Type = x.ContentType.ToString(),
                FileName = x.FileName
            })
            .ToListAsync();
    }

    public Task<ContentMetaDataEntity> FindByChannelId(int contentId)
    {
        return _appDbContext.ContentMetaDatas
            .Where(x => x.ContentId == contentId)
            .SingleAsync();
    }
}