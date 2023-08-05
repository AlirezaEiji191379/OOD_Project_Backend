namespace OOD_Project_Backend.Finance.Business.Contracts;

public class TransactionContract
{
    public DateTime DateTime { get; set; }
    public string Destination { get; set; }
    public bool Success { get; set; }
}