using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Content.DataAccess.Dtos;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;
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
}