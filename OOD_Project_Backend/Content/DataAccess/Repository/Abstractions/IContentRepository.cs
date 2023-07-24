﻿using OOD_Project_Backend.Content.DataAccess.Dtos;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Core.DataAccess.Abstractions;

namespace OOD_Project_Backend.Content.DataAccess.Repository.Abstractions;

public interface IContentRepository : IBaseRepository<ContentEntity>
{
    Task<List<ContentDto>> GetChannelContents(int channelId);
}