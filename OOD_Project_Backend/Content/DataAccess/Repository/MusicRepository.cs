﻿using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Content.DataAccess.Repository;

public class MusicRepository : BaseRepository<MusicEntity>,IMusicRepository
{
    public MusicRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}