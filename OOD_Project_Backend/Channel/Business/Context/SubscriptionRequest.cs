using OOD_Project_Backend.Channel.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Channel.Business.Context;

public class SubscriptionRequest
{
    public int? Id { get; set; }
    public int ChannelId { get; set; }
    public SubscriptionPeriod Period { get; set; }
    public int Price { get; set; }
}