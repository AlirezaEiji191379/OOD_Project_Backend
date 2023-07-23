using OOD_Project_Backend.Content.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Content.DataAccess.Entities;

public class ContentMetaDataEntity
{
    
    public ContentType ContentType { get; set; }
    public int Price { get; set; }
    public bool Premium { get; set; }
}