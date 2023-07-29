using AutoMapper;
using Microsoft.Extensions.Logging;
using T1.Abstractions;
using T1.Command;
using T1.Db;
using T1.Dto;
using T1.Model;
using T1.Query;

namespace T1Test;

[TestClass]
public class TestCommandAndQuery
{
    [TestMethod]
    public async Task TestAddTransactionCommandValidation()
    {
        var mockDb = new Mock<IDb>();
        var mockLogger = new Mock<ILogger<AddTransactionCommandHandler>>();

        Mapper mapper = CreateMapper();

        var cmdHandler = new AddTransactionCommandHandler(mockDb.Object, mockLogger.Object, mapper);

        var cmd1_1 = new AddTransactionCommand
        {
            // Id = 1,
            TransactionDate = DateTime.UtcNow,
            Amount = 10
        };

//        Assert.ThrowsExceptionAsync

        await AssertEx.ThrowsAsync(async () =>
        {
            await cmdHandler.HandleAsync(cmd1_1);
        });

        var cmd1_2 = new AddTransactionCommand
        {
            Id = -1,
            TransactionDate = DateTime.UtcNow,
            Amount = 10
        };
        await AssertEx.ThrowsAsync(async () =>
        {
            await cmdHandler.HandleAsync(cmd1_2);
        });

        var cmd2 = new AddTransactionCommand
        {
            Id = 1,
            // TransactionDate = DateTime.UtcNow,
            Amount = 10
        };
        await AssertEx.ThrowsAsync(async () =>
        {
            await cmdHandler.HandleAsync(cmd2);
        });

        var cmd3_1 = new AddTransactionCommand
        {
            Id = 1,
            TransactionDate = DateTime.UtcNow,
            // Amount = 10
        };
        await AssertEx.ThrowsAsync(async () =>
        {
            await cmdHandler.HandleAsync(cmd3_1);
        });

        var cmd3_2 = new AddTransactionCommand
        {
            Id = 1,
            TransactionDate = DateTime.UtcNow,
            Amount = -10
        };
        await AssertEx.ThrowsAsync(async () =>
        {
            await cmdHandler.HandleAsync(cmd3_2);
        });
    }

    [TestMethod]
    public async Task TestTransactionAddedAndQueried()
    {
        var db = new MemoryDb();
        var mockLoggerCmd = new Mock<ILogger<AddTransactionCommandHandler>>();
        var mockLoggerQuery = new Mock<ILogger<GetTransactionByIdQueryHandler>>();

        Mapper mapper = CreateMapper();

        // Добавить транзакцию
        var cmd = new AddTransactionCommand
        {
            Id = 1,
            TransactionDate = DateTime.UtcNow,
            Amount = 10
        };
        var cmdHandler = new AddTransactionCommandHandler(db, mockLoggerCmd.Object, mapper);
        await cmdHandler.HandleAsync(cmd);

        // Запросить транзакцию
        var query = new GetTransactionByIdQuery
        {
            Id = (int)cmd.Id
        };
        var queryHandler = new GetTransactionByIdQueryHandler(db, mockLoggerQuery.Object, mapper);
        var dto = await queryHandler.HandleAsync(query);

        Assert.AreEqual(dto.Id, cmd.Id);
        Assert.AreEqual(dto.TransactionDate, cmd.TransactionDate);
        Assert.AreEqual(dto.Amount, cmd.Amount);
    }

    private static Mapper CreateMapper()
    {
        Mapper mapper;
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new CommandMappingProfile());
            cfg.AddProfile(new DtoMappingProfile());
        });
        mapper = new Mapper(configuration);
        return mapper;
    }
}
