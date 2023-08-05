using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Content.DataAccess.Dtos;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Content.DataAccess.Repository;

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

    public Task<ContentMetaDataEntity> FindByChannelId(int contentId)
    {
        return _appDbContext.ContentMetaDatas
            .Where(x => x.ContentId == contentId)
            .SingleAsync();
    }
}