using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Content.DataAccess.Dtos;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Repository.Abstractions;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Content.DataAccess.Repository;

public class ContentRepository : BaseRepository<ContentEntity>,IContentRepository
{
    private readonly AppDbContext _appDbContext;
    public ContentRepository(AppDbContext dbContext) : base(dbContext)
    {
        _appDbContext = dbContext;
    }

    public async Task<List<ContentDto>> GetChannelContents(int channelId)
    {
        var query = await _appDbContext.Contents
            .Where(x => x.ChannelId == channelId)
            .Include(x => x.Channel)
            .ToListAsync();
        return query.Select(x => new ContentDto()
        {
            Type = x.ContentMetaData.ContentType,
            CreatedAt = x.CreatedAt,
            ContentId = x.Id,
            FileName = x.ContentMetaData.FileName
        }).ToList();
    }
}