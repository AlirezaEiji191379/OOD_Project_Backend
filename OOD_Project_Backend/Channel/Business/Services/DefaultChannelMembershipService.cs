using OOD_Project_Backend.Channel.Business.Context;
using OOD_Project_Backend.Channel.Business.Contracts;
using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Channel.DataAccess.Entities.Enums;
using OOD_Project_Backend.Channel.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.User.Business.Contracts;

namespace OOD_Project_Backend.Channel.Business.Services;

public class DefaultChannelMembershipService : IChannelMembershipService
{
    private readonly IChannelMemberRepository _memberRepository;
    private readonly IChannelRepository _channelRepository;
    private readonly IUserFacade _userFacade;

    public DefaultChannelMembershipService(IChannelMemberRepository memberRepository, IChannelRepository channelRepository, IUserFacade userFacade)
    {
        _memberRepository = memberRepository;
        _channelRepository = channelRepository;
        _userFacade = userFacade;
    }

    public async Task<Response> JoinChannel(string joinLink)
    {
        try
        {
            var channelEntity = await _channelRepository.FindChannelByJoinLink(joinLink);
            if (channelEntity == null)
            {
                return new Response(404, "Channel Not Found!");
            }

            var channelId = channelEntity.Id;
            var userId = _userFacade.GetCurrentUserId();
            if (await _memberRepository.IsChannelMember(userId, channelId))
            {
                return new Response(400, new { Message = "the member was joined later to channel!" });
            }

            var channelMember = new ChannelMemberEntity()
            {
                UserId = userId,
                ChannelId = channelId,
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

    public async Task<Response> ShowMembers(int channelId)
    {
        try
        {
            var userContracts = await _memberRepository.FindUsersByChannelId(channelId);
            return new Response(200, new { Message = userContracts });
        }
        catch (Exception e)
        {
            return new Response(404, new { Message = "channel not found!" });
        }
    }

    public async Task<Response> AddAdminToChannel(ChannelMembershipRequest channelMembershipRequest)
    {
        try
        {
            var userId = _userFacade.GetCurrentUserId();
            var channelMemberEntity = await _memberRepository.FindByUserIdAndChannelId(userId, channelMembershipRequest.ChannelId);
            if (channelMemberEntity.Role != Role.OWNER)
            {
                return new Response(403, new { Message = "you do not have permission to add admin to Channel" });
            }

            foreach (var memberId in channelMembershipRequest.MemberIds)
            {
                _memberRepository.UpdateRoleOfUserInChannel(memberId, channelMembershipRequest.ChannelId, Role.ADMIN);
            }

            await _memberRepository.SaveChangesAsync();
            return new Response(200, new { Message = "Admins are added!" });
        }
        catch (Exception e)
        {
            return new Response(400, "");
        }
    }
}