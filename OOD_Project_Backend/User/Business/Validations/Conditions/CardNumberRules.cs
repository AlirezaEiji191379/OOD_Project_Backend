using OOD_Project_Backend.Core.Validation.Contracts;

namespace OOD_Project_Backend.User.Business.Validations.Conditions;

public class CardNumberRules : IRule
{
    private string _cardNumber;

    public CardNumberRules(string cardNumber)
    {
        _cardNumber = cardNumber;
    }

    public bool Apply()
    {
        if (_cardNumber.Length != 16)
        {
            return false;
        }
        return _cardNumber.All(Char.IsDigit);
    }
}