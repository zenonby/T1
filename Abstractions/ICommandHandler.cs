namespace T1.Abstractions;

/// <summary>
/// Обработчик команд
/// </summary>
/// <typeparam name="TCommand"></typeparam>
public interface ICommandHandler<TCommand>
{
    Task HandleAsync(TCommand cmd);
}
