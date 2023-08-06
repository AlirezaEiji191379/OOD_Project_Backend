using OOD_Project_Backend.Channel.ChannelCore.Business.Contracts;
using OOD_Project_Backend.Channel.ChannelCore.Business.Services;
using OOD_Project_Backend.Channel.ChannelCore.DataAccess.Repositories;
using OOD_Project_Backend.Channel.ChannelCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Channel.Subscription.Business.Contracts;
using OOD_Project_Backend.Channel.Subscription.Business.Jobs;
using OOD_Project_Backend.Channel.Subscription.Business.Services;
using OOD_Project_Backend.Channel.Subscription.DataAccess.Repositories;
using OOD_Project_Backend.Channel.Subscription.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.Common.DependencyInjection.Abstractions;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Channel;

public class DependencyInjector : IDependencyInstaller
{
    public void Install(IServiceCollection services)
    {
        services.AddScoped<IChannelService,DefaultChannelService>();
        services.AddScoped<IChannelMembershipService, DefaultChannelMembershipService>();
        services.AddScoped<IChannelMemberRepository, ChannelMemberRepository>();
        services.AddScoped<IChannelRepository,ChannelRepository>();
        services.AddScoped<ISubscriptionService, DefaultSubscriptionService>();
        services.AddScoped<ISubscriptionRepository,SubscriptionRepository>();
        services.AddScoped<IChannelPremiumUsersRepository,ChannelPremiumUsersRepository>();
        services.AddScoped<INonPremiumUsersPremiumContentsRepository,NonPremiumUsersPremiumContentsRepository>();
        services.AddScoped<ISubscriptionDeactivationJob, SubscriptionDeactivationJob>();
        services.AddScoped<IChannelFacade, ChannelFacade>();
    }
}