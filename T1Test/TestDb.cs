using T1.Db;
using T1.Db.Exceptions;
using T1.Model;

namespace T1Test;

[TestClass]
public class TestDb
{
    [TestMethod]
    public async Task TestDuplicatesNotAllowed()
    {
        var db = new MemoryDb();
        var transaction = new Transaction
        {
            Id = 1,
            TransactionDate = DateTime.UtcNow,
            Amount = 10
        };

        // 1
        await db.InsertTransactionAsync(transaction);

        await Assert.ThrowsExceptionAsync<DuplicateKeyException>(async () =>
        {
            // 2
            await db.InsertTransactionAsync(transaction);
        });
    }

    [TestMethod]
    public async Task TestDbObjectNotFound()
    {
        var db = new MemoryDb();

        await Assert.ThrowsExceptionAsync<ObjectNotFoundException>(async () =>
        {
            await db.GetTransactionAsync(1);
        });
    }
}