using OOD_Project_Backend.Channel.Business.Contracts;
using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Channel.DataAccess.Entities.Enums;
using OOD_Project_Backend.Channel.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Channel.Business.Services;

public class DefaultChannelMembershipService : IChannelMembershipService
{
    private readonly IChannelMemberRepository _memberRepository;
    private readonly IChannelRepository _channelRepository;

    public DefaultChannelMembershipService(IChannelMemberRepository memberRepository, IChannelRepository channelRepository)
    {
        _memberRepository = memberRepository;
        _channelRepository = channelRepository;
    }


    public async Task<Response> JoinChannel(string joinLink, int userId)
    {
        try
        {
            var channelEntity = await _channelRepository.FindChannelByJoinLink(joinLink);
            if (await _memberRepository.IsChannelMember(userId, channelEntity.Id))
            {
                return new Response(400,new {Message = "the member was joined later to channel!"});
            }

            var channelMember = new ChannelMemberEntity()
            {
                UserId = userId,
                ChannelId = channelEntity.Id,
                Role = Role.MEMBER
            };
            await _memberRepository.Create(channelMember);
            await _memberRepository.SaveChangesAsync();
            return new Response(200, new { Message = "user joined channel!" });
        }
        catch (Exception e)
        {
            return new Response(404, new { Message = "Channel Not Found!" });
        }
    }
}