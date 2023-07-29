using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using T1.Abstractions;

namespace T1.Command;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync<TCommand>(TCommand command)
    {
        var commandHandler = _serviceProvider.GetService<ICommandHandler<TCommand>>();
        Debug.Assert(null != commandHandler);

        await commandHandler.HandleAsync(command);
    }
}
