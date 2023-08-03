using OOD_Project_Backend.Core.Validation.Contracts;

namespace OOD_Project_Backend.Channel.Business.Validations.Conditions;

public class PriceRule : IRule
{
    private int _price;

    public PriceRule(int price)
    {
        _price = price;
    }

    public bool Apply()
    {
        if (_price <= 0)
        {
            return false;
        }
        return true;
    }
}