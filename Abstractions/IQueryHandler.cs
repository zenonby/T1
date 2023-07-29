namespace T1.Abstractions;

/// <summary>
/// Обработчик запросов
/// </summary>
/// <typeparam name="TQuery">Тип запроса</typeparam>
/// <typeparam name="TResult">Тип ответа (DTO)</typeparam>
public interface IQueryHandler<TQuery, TResult>
{
    Task<TResult> HandleAsync(TQuery query);
}
