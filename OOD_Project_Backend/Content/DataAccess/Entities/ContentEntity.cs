namespace OOD_Project_Backend.Content.DataAccess.Entities;

public class ContentEntity
{
    public int Id { get; set; }
    public string Title { get;set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public int ContentMetaDataId { get; set; }
    public ContentMetaDataEntity ContentMetaData { get; set; }
}