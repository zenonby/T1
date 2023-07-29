using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoMapper;
using T1.UI;
using T1.Command;
using T1.Abstractions;
using T1.Db;
using T1.Dto;
using T1.Query;

namespace T1;

class Program
{
    public static async Task Main()
    {
        var cancelTokenSrc = new CancellationTokenSource();

        using IHost host = Host.CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton(CreateMapper());
                services.AddSingleton<IAppTerminator>(_ => new AppTerminator(cancelTokenSrc));

                // Database
                services.AddSingleton<IDb, MemoryDb>();

                // Commands
                services.AddTransient<ICommandDispatcher, CommandDispatcher>();
                services.AddTransient<ICommandHandler<AddTransactionCommand>, AddTransactionCommandHandler>();

                // Queries
                services.AddTransient<IQueryDispatcher, QueryDispatcher>();
                services.AddTransient<IQueryHandler<GetTransactionByIdQuery, TransactionDto>, GetTransactionByIdQueryHandler>();

                // UI
                services.AddHostedService<ConsoleUI>();
            })
            .Build();

        var logger = host.Services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Host created.");

        await host.RunAsync(cancelTokenSrc.Token);
    }

    /// <summary>
    /// Создает Mapper
    /// </summary>
    private static IMapper CreateMapper()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(typeof(CommandMappingProfile));
            cfg.AddProfile(typeof(DtoMappingProfile));
        });

#if DEBUG
        configuration.AssertConfigurationIsValid();
#endif
        var mapper = configuration.CreateMapper();
        return mapper;
    }
}
