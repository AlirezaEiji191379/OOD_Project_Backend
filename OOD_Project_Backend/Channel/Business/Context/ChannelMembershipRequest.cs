namespace OOD_Project_Backend.Channel.Business.Context;

public class ChannelMembershipRequest
{
    public List<int>? MemberIds { get; set; }
    public List<int>? AdminIds { get; set; }
    public int ChannelId { get; set; }
}