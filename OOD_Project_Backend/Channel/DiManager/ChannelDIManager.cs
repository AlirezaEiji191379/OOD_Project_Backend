﻿using OOD_Project_Backend.Channel.Business.Abstractions;
using OOD_Project_Backend.Channel.Business.Services;
using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Channel.DataAccess.Repositories;
using OOD_Project_Backend.Core.Common.DependencyInjection.Abstractions;
using OOD_Project_Backend.Core.DataAccess.Abstractions;

namespace OOD_Project_Backend.Channel.DiManager;

public class ChannelDIManager : IDependencyInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ChannelService,DefaultChannelService>();
        serviceCollection.AddScoped<IBaseRepository<ChannelMemberEntity>, ChannelMemberRepository>();
        serviceCollection.AddScoped<IBaseRepository<ChannelEntity>,ChannelRepository>();
    }
}