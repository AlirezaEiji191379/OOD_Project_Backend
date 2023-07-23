namespace OOD_Project_Backend.User.DataAccess.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string ProfilePicPath { get; set; }
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
}