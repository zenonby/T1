using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using T1.Abstractions;
using T1.Dto;
using T1.Query;

namespace T1.UI;

/// <summary>
/// Форма запроса транзакции по идентификатору
/// </summary>
internal class GetTransactionForm : IView
{
    private readonly IServiceProvider _services;
    private int _transactionId = 0;

    internal GetTransactionForm(IServiceProvider services)
    {
        _services = services;
    }

    public async Task<IView?> HandleTextInputAsync(string text)
    {
        if (0 >= _transactionId)
        {
            int id;
            if (!int.TryParse(text, out id) || 0 >= id)
                throw new ValidationException("Неверное значение идентификатора транзакции");

            _transactionId = id;

            var dto = await GetTransactionAsync();
            var json = JsonSerializer.Serialize(dto);

            Console.WriteLine(json);
            Console.WriteLine("[OK]");

            return new Menu(_services);
        }
        else
        {
            Debug.Assert(false);
            throw new ApplicationException("Unexpected");
        }
    }

    public async Task ShowAsync()
    {
        if (0 >= _transactionId)
        {
            Console.WriteLine("Введите индентификатор транзакции:");
        }
        else
        {
            await Task.CompletedTask;
            Debug.Assert(false);
            throw new ApplicationException("Unexpected");
        }
    }

    private async Task<TransactionDto> GetTransactionAsync()
    {
        Debug.Assert(0 < _transactionId);

        try
        {
            var queryDispatcher = _services.GetRequiredService<IQueryDispatcher>();
            var dto = await queryDispatcher.DispatchAsync<GetTransactionByIdQuery, TransactionDto>(
                new GetTransactionByIdQuery { Id = _transactionId });
            return dto;
        }
        catch (Exception)
        {
            // Сбросить Id транзакции
            _transactionId = 0;
            throw;
        }
    }
}
