﻿using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Channel.DataAccess.Repositories.Contracts;

public interface IChannelPremiumUsersRepository : IBaseRepository<ChannelPremiumUsersEntity>
{
    ValueTask<ChannelMemberEntity?> Find(int userId, int channelId);
}