using System.Text.RegularExpressions;
using OOD_Project_Backend.Core.Validation;
using OOD_Project_Backend.Core.Validation.Contracts;

namespace OOD_Project_Backend.User.Business.Validation.Rules;

public class PhoneNumberRule : IRule
{
    private string _phoneNumber;

    public PhoneNumberRule(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
    }

    public bool Apply()
    {
        if (string.IsNullOrEmpty(_phoneNumber))
        {
            return false;
        }

        if (_phoneNumber.Length != 11)
        {
            return false;
        }
        var pattern = @"^\(?([0-9]{4})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$";
        return Regex.IsMatch(_phoneNumber, pattern);
    }
}