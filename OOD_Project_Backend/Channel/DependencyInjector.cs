﻿using OOD_Project_Backend.Channel.Business.Contracts;
using OOD_Project_Backend.Channel.Business.Services;
using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Channel.DataAccess.Repositories;
using OOD_Project_Backend.Core.Common.DependencyInjection.Abstractions;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Channel;

public class DependencyInjector : IDependencyInstaller
{
    public void Install(IServiceCollection services)
    {
        services.AddScoped<IChannelService,DefaultChannelService>();
        services.AddScoped<IBaseRepository<ChannelMemberEntity>, ChannelMemberRepository>();
        services.AddScoped<IBaseRepository<ChannelEntity>,ChannelRepository>();
    }
}