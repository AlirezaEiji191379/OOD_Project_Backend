using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Channel.Business.Context;
using OOD_Project_Backend.Channel.Business.Contracts;
using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Channel.DataAccess.Entities.Enums;
using OOD_Project_Backend.Channel.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.User.Business.Context;
using OOD_Project_Backend.User.Business.Contracts;

namespace OOD_Project_Backend.Channel.Business.Services;

public class DefaultChannelMembershipService : IChannelMembershipService
{
    private readonly IChannelMemberRepository _memberRepository;
    private readonly IChannelRepository _channelRepository;
    private readonly IUserFacade _userFacade;

    public DefaultChannelMembershipService(IChannelMemberRepository memberRepository,
        IChannelRepository channelRepository, IUserFacade userFacade)
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
            var channelMemberEntity =
                await _memberRepository.FindByUserIdAndChannelId(userId, channelMembershipRequest.ChannelId);
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

    public async Task<Response> SetIncomeShare(ChannelMembershipRequest channelMembershipRequest)
    {
        try
        {
            var userId = _userFacade.GetCurrentUserId();
            if (await IsOwner(channelMembershipRequest.ChannelId, userId) == false)
            {
                return new Response(403, new { Message = "just owner can set income shares!" });
            }

            var admins =
                await _memberRepository.FindByChannelIdAndRole(channelMembershipRequest.ChannelId, Role.ADMIN,
                    Role.OWNER);
            var incomeShareSum = channelMembershipRequest.IncomeShares.Values.Sum();
            if (incomeShareSum != 100)
            {
                return new Response(400, new { Message = "income shares must adds up to 100" });
            }

            foreach (var memberId in channelMembershipRequest.IncomeShares.Keys)
            {
                if (!admins.Any(x => x.UserId == memberId))
                {
                    return new Response(400,
                        new { Message = "all the admins and owners must be defined for incomeshares!" });
                }
            }

            foreach (var memberId in channelMembershipRequest.IncomeShares.Keys)
            {
                var admin = admins.Single(x => x.UserId == memberId);
                admin.IncomeShare = channelMembershipRequest.IncomeShares[memberId];
                _memberRepository.Update(admin);
            }

            await _memberRepository.SaveChangesAsync();
            return new Response(200, new { Message = "income shares are now updated!" });
        }
        catch (Exception e)
        {
            return new Response(400, new { Message = "add income share failed!" });
        }
    }

    public async Task<bool> IsOwner(int channelId, int userId)
    {
        return await _memberRepository.IsOwner(userId, channelId);
    }

    public async Task<bool> IsAdmin(int channelId, int userId)
    {
        return await _memberRepository.IsAdmin(userId, channelId);
    }

    public async Task<Response> ShowAdmins(int channelId)
    {
        try
        {
            var admins = await _memberRepository.FindByChannelIdAndRole(channelId, Role.ADMIN, Role.OWNER);
            var adminProfiles = new List<UserContract>();
            admins.ForEach(async (x) =>
            {
                var result = await _userFacade.GetUser(x.UserId);
                adminProfiles.Add(result);
            });
            return new Response(200, new { Messsage = adminProfiles });
        }
        catch (Exception e)
        {
            return new Response(400, new { Message = "channel not found" });
        }
    }

    public async Task<Response> RemoveMember(ChannelMembershipRequest membershipRequest)
    {
        try
        {
            var userId = _userFacade.GetCurrentUserId();
            var channelId = membershipRequest.ChannelId;
            var isCurrentUserAdmin = await IsAdmin(channelId, userId);
            var isCurrentUserOwner = await IsOwner(channelId, userId);
            if (!isCurrentUserAdmin && !isCurrentUserOwner)
            {
                return new Response(403, new { Message = "only admins and owners can delete users!" });
            }

            foreach (var memberId in membershipRequest.MemberIds)
            {
                var isAdmin = await IsAdmin(channelId, memberId);
                var isOwner = await IsOwner(channelId, memberId);
                if (!isAdmin && !isOwner)
                {
                    _memberRepository.Delete(new ChannelMemberEntity()
                    {
                        UserId = memberId,
                        ChannelId = channelId
                    });
                }
                else
                {
                    return new Response(403,
                        new { Message = "can not delete owner or admin! you must degrade them first!" });
                }
            }

            await _memberRepository.SaveChangesAsync();
            return new Response(200, new { Message = "Users deleted!" });
        }
        catch (Exception e)
        {
            return new Response(400, new { Message = "user or channel not found" });
        }
    }

    public async Task<Response> RemoveAdmin(ChannelMembershipRequest membershipRequest)
    {
        try
        {
            var userId = _userFacade.GetCurrentUserId();
            var channelId = membershipRequest.ChannelId;
            var isCurrentUserOwner = await IsOwner(channelId, userId);
            if (!isCurrentUserOwner)
            {
                return new Response(403, new { Message = "only owners can delete admins!" });
            }

            foreach (var memberId in membershipRequest.AdminIds)
            {
                var isAdmin = await IsAdmin(channelId, memberId);
                if (isAdmin)
                {
                    _memberRepository.UpdateRoleOfUserInChannel(memberId, channelId, Role.MEMBER);
                }
                else
                {
                    return new Response(403, new { Message = "the user is not admin!" });
                }
            }

            await _memberRepository.SaveChangesAsync();
            return new Response(200, new { Message = "Users deleted!" });
        }
        catch (Exception e)
        {
            return new Response(400, new { Message = "user or channel not found" });
        }
    }

    public async Task<Response> GetChannelsList()
    {
        try
        {
            var channelsList = await _channelRepository.GetAll(false).Select(x => new
            {
                Id = x.Id,
                JoinLink = x.JoinLink,
                Name = x.Name
            }).ToListAsync();
            return new Response(200, new { Message = channelsList });
        }
        catch (Exception e)
        {
            return new Response(400, new { Message = "get channels list failed!" });
        }
    }
}