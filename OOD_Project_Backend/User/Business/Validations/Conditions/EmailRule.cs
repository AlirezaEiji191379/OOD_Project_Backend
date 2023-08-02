using System.Text.RegularExpressions;
using OOD_Project_Backend.Core.Validation;
using OOD_Project_Backend.Core.Validation.Contracts;

namespace OOD_Project_Backend.User.Business.Validation.Rules;

public class EmailRule : IRule
{
    private string _email;
    
    public EmailRule(string email)
    {
        this._email = email;
    }
    public bool Apply()
    {
        if (string.IsNullOrEmpty(_email))
        {
            return false;
        }

        if (_email.Length > 100)
        {
            return false;
        }
        var pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(_email, pattern);
    }
}