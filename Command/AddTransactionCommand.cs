using System.ComponentModel.DataAnnotations;

namespace T1.Command;

/// <summary>
/// Команда добавления транзакции
/// </summary>
public class AddTransactionCommand
{
    [Required]
    [Range(1, int.MaxValue)]
    public int? Id { get; set; }

    [Required]
    public DateTime? TransactionDate { get; set; }

    [Required]
    [Range(double.Epsilon, double.MaxValue)]
    public decimal? Amount { get; set; }
}
