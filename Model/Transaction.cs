namespace T1.Model;

/// <summary>
/// Transaction DB entity
/// </summary>
public class Transaction
{
    public int Id { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
}
