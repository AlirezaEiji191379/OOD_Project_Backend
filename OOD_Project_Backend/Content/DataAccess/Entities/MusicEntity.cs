namespace OOD_Project_Backend.Content.DataAccess.Entities;

public class MusicEntity
{
    public int ContentId { get; set; }
    public ContentEntity Content { get; set; }
    public string ArtistName{ get; set; }
    public int FileId { get; set; }
    public FileEntity File{ get; set; }
    public string MusicText{ get; set; }
}