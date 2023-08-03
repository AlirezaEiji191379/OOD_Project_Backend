﻿using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Channel.DataAccess.Repositories.Contracts;

public interface IChannelRepository : IBaseRepository<ChannelEntity>
{
    Task<ChannelEntity> FindChannelByJoinLink(string joinLink);
}