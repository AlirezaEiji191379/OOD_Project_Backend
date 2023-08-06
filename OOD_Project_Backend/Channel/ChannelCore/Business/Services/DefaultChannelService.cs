using System.Text;
using OOD_Project_Backend.Channel.ChannelCore.Business.Contracts;
using OOD_Project_Backend.Channel.ChannelCore.DataAccess.Entities;
using OOD_Project_Backend.Channel.ChannelCore.DataAccess.Entities.Enums;
using OOD_Project_Backend.Channel.ChannelCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.Core.Validation.Common;
using OOD_Project_Backend.Core.Validation.Contracts;
using OOD_Project_Backend.User.Business.Contracts;

namespace OOD_Project_Backend.Channel.ChannelCore.Business.Services;

public class DefaultChannelService : IChannelService
{
    private readonly IChannelRepository _channelRepository;
    private readonly IChannelMemberRepository _channelMemberRepository;
    private readonly IConfiguration _configuration;
    private readonly IValidator _validator;
    private readonly IUserFacade _userFacade;

    public DefaultChannelService(IChannelRepository channelRepository,
        IChannelMemberRepository channelMemberRepository, 
        IValidator validator,
        IConfiguration configuration, 
        IUserFacade userFacade)
    {
        _channelRepository = channelRepository;
        _channelMemberRepository = channelMemberRepository;
        _validator = validator;
        _configuration = configuration;
        _userFacade = userFacade;
    }

    public async Task<Response> CreateChannel(string name)
    {
        try
        {
            var joinLink = "@" + Guid.NewGuid().ToString().Replace("-", "");
            var channel = new ChannelEntity()
            {
                Name = name,
                JoinLink = joinLink
            };
            await _channelRepository.Create(channel);
            var userId = _userFacade.GetCurrentUserId();
            var channelMember = new ChannelMemberEntity()
            {
                UserId = userId,
                Channel = channel,
                Role = Role.OWNER
            };
            await _channelMemberRepository.Create(channelMember);
            await _channelMemberRepository.SaveChangesAsync();
            return new Response(201, new { Message = new { ChannelId = channel.Id, JoinLink = joinLink } });
        }
        catch (Exception e)
        {
            return new Response(400, new { Message = "the channel with that name was created!" });
        }
    }

    public async Task<Response> AddChannelPicture(IFormFile picture,int channelId)
    {
        try
        {
            var userId = _userFacade.GetCurrentUserId();
            if (await _channelMemberRepository.CheckIfUserIsChannelOwner(userId, channelId) == false)
            {
                return new Response(403, new { Message = "only channel owners can upload channel picture!" });
            }
            if (!_validator.Validate(new PictureRule(picture)))
            {
                return new Response(400,
                    new { Message = "image file size must be at most 5 mb and .png and .jpg extension are supported" });
            }
            var resourcePath = _configuration.GetValue<string>("ChannelPicturePath");
            var picPath = new StringBuilder(resourcePath).Append($"/{channelId}.png").ToString();
            if (File.Exists(picPath))
            {
                File.Delete(picPath);
            }
            await using var fileStream = new FileStream(picPath, FileMode.Create);
            await picture.CopyToAsync(fileStream);
            return new Response(200, new { Message = "image uploaded successfully!" });
        }
        catch (Exception e)
        {
            return new Response(400, new { Message = "channel photo upload failed!" });
        }
    }

    public async Task<Response> ShowChannelsList()
    {
        try
        {
            var userId = _userFacade.GetCurrentUserId();
            var channelMemberEntities = await _channelMemberRepository
                .FindByMemberId(userId);
            var channels = channelMemberEntities.Select(x => new
            {
                Id = x.ChannelId,
                JoinLink = x.Channel.JoinLink,
                Name = x.Channel.Name
            });
            return new Response(200, new { Message = channels });
        }
        catch (Exception e)
        {
            return new Response(400, "we can not retrive list of channels due to problems please try later");
        }
    }
    
    
    
}