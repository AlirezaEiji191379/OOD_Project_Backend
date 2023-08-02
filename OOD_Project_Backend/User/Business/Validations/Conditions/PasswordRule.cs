using OOD_Project_Backend.Core.Validation;
using OOD_Project_Backend.Core.Validation.Contracts;

namespace OOD_Project_Backend.User.Business.Validation.Rules;

public class PasswordRule : IRule
{
    private string _password;

    public PasswordRule(string password)
    {
        _password = password;
    }

    public bool Apply()
    {
        if (_password.Length < 6 || _password.Length > 20)
        {
            return false;
        }

        return true;
    }
}