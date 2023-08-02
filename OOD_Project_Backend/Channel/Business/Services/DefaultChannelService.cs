using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Channel.Business.Abstractions;
using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Channel.DataAccess.Entities.Enums;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Channel.Business.Services;

public class DefaultChannelService : ChannelService
{
    private readonly IBaseRepository<ChannelEntity> _channelRepository;
    private readonly IBaseRepository<ChannelMemberEntity> _channelMemberRepository;

    public DefaultChannelService(IBaseRepository<ChannelEntity> channelRepository,
        IBaseRepository<ChannelMemberEntity> channelMemberRepository)
    {
        _channelRepository = channelRepository;
        _channelMemberRepository = channelMemberRepository;
    }

    public async Task<Response> CreateChannel(string name, int userId)
    {
        try
        {
            var joinLink =  "@" +Guid.NewGuid().ToString().Replace("-", "");
            var channel = new ChannelEntity()
            {
                Name = name,
                JoinLink = joinLink
            };
            await _channelRepository.Create(channel);
            await _channelRepository.SaveChangesAsync();
            var channelMember = new ChannelMemberEntity()
            {
                UserId = userId,
                ChannelId = channel.Id,
                Role = Role.OWNER
            };
            await _channelMemberRepository.Create(channelMember);
            await _channelMemberRepository.SaveChangesAsync();
            return new Response(201, new { Message = new { ChannelId = channel.Id,JoinLink = joinLink } });
        }
        catch (Exception e)
        {
            return new Response(400, new { Message = "the channel can not be created due to system failures!" });
        }
    }

    public async Task<Response> GetAllUsersChannels(int userId)
    {
        try
        {
            var ownedChannelIds =await _channelMemberRepository
                .FindByCondition(x => x.Role == Role.OWNER && x.UserId == userId, false)
                .Select(x => x.ChannelId)
                .ToListAsync();
            var channels = await _channelRepository
                .FindByCondition(x => ownedChannelIds.Contains(x.Id), false)
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    JoinLink = x.JoinLink
                }).ToListAsync();
            return new Response(200, new {Message = channels});
        }
        catch (Exception e)
        {
            return new Response(400,"we can not retrive list of channels due to problems please try later");
        }
    }

}