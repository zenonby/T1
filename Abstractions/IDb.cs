using T1.Model;

namespace T1.Abstractions;

/// <summary>
/// Абстрактная база данных
/// </summary>
public interface IDb
{
    public Task InsertTransactionAsync(Transaction transaction);

    public Task<Transaction> GetTransactionAsync(int transactionId);
}
