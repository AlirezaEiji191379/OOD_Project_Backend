using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;

public class SubtitleEntity
{
    public int Id { get; set; }
    public int FileId { get; set; }
    public FileEntity File{ get; set; }
    public SubtitlePosition Position { get; set; }
}