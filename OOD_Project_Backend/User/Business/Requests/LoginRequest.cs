﻿namespace OOD_Project_Backend.User.Business.Requests;

public class LoginRequest
{
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string Password { get; set; }
}