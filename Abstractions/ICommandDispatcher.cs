namespace T1.Abstractions;

/// <summary>
/// Диспатч команд
/// </summary>
public interface ICommandDispatcher
{
    Task DispatchAsync<TCommand>(TCommand command);
}
