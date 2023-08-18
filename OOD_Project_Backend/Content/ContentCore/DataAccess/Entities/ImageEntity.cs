namespace OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;

public class ImageEntity
{
    public int ContentId { get; set; }
    public ContentEntity Content { get; set; }
    public int FileId { get; set; }
    public FileEntity File{ get; set; }
}