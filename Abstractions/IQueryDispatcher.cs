namespace T1.Abstractions;

/// <summary>
/// Диспатч запросов
/// </summary>
public interface IQueryDispatcher
{
    Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query);
}
