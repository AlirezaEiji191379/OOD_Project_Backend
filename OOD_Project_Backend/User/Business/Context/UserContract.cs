namespace OOD_Project_Backend.User.Business.Context;

public class UserContract
{
    public int UserId { get; init; }
    public string Name { get; init; }
    public string Biography { get; set; }
}