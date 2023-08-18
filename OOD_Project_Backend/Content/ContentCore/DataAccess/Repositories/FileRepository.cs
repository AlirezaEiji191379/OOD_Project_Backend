using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories;

public class FileRepository : BaseRepository<FileEntity>,IFileEntityRepository
{
    private readonly AppDbContext _dbContext;

    public FileRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<FileEntity> FindById(int fileId)
    {
        return _dbContext.Files.Where(x => x.Id == fileId).SingleAsync();
    }
}