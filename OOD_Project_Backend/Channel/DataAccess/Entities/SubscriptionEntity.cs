using OOD_Project_Backend.Channel.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Channel.DataAccess.Entities;

public class SubscriptionEntity
{
    public int Id { get; set; }
    public int ChannelId { get; set; }
    public ChannelEntity ChannelEntity { get; set; }
    public SubscriptionPeriod Period { get; set; }
    public int Price { get; set; }
}