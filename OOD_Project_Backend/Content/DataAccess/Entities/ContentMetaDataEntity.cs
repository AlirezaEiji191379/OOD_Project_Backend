using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Content.DataAccess.Entities;

public class ContentMetaDataEntity
{
    public int ContentId { get; set; }
    public ContentEntity Content { get; set; }
    public int ChannelId { get; set; }
    public ChannelEntity Channel { get; set; }
    //public CategoryEntity Category { get; set; }*/
    public ContentType ContentType { get; set; }
    public int Price { get; set; }
    public bool Premium { get; set; }
    public string FileName { get; set; }
}