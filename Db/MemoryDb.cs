using T1.Abstractions;
using T1.Model;
using T1.Db.Exceptions;
using System.Collections.Concurrent;

namespace T1.Db;

public class MemoryDb : IDb
{
    private readonly ConcurrentDictionary<int, Transaction> _transactions =
        new ConcurrentDictionary<int, Transaction>();

    public async Task<Transaction> GetTransactionAsync(int transactionId)
    {
        Transaction? transaction;
        if (!_transactions.TryGetValue(transactionId, out transaction))
            throw new ObjectNotFoundException();

        return await Task.FromResult(transaction);
    }

    public async Task InsertTransactionAsync(Transaction transaction)
    {
        if (!_transactions.TryAdd(transaction.Id, transaction))
            throw new DuplicateKeyException();

        await Task.CompletedTask;
    }
}
