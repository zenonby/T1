using Microsoft.Extensions.DependencyInjection;
using T1.Abstractions;
using T1.Command;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;

namespace T1.UI;

/// <summary>
/// UI добавления транзакции
/// </summary>
internal class AddTransactionForm : IView
{
    private readonly IServiceProvider _services;
    private AddTransactionCommand _cmd = new AddTransactionCommand();

    internal AddTransactionForm(IServiceProvider services)
    {
        _services = services;
    }

    public async Task<IView?> HandleTextInputAsync(string text)
    {
        // Установить соотв. значение
        if (_cmd.Id <= 0)
        {
            int id;
            if (!int.TryParse(text, out id) || id <= 0)
                throw new ValidationException("Значение идентификатора транзакции должно быть целым числом, больше нуля");

            _cmd.Id = id;
        }
        else if (_cmd.TransactionDate == default(DateTime))
        {
            DateTime dt;
            if (!DateTime.TryParseExact(text, "dd.MM.yyyy", null, DateTimeStyles.None, out dt))
                throw new ValidationException("Неверное значение даты. Используйте формат ДД.ММ.ГГГГ");

            _cmd.TransactionDate = dt;
        }
        else if (_cmd.Amount == 0M)
        {
            decimal amount;
            if (!decimal.TryParse(text, out amount) || amount <= 0)
                throw new ValidationException("Неверное значение суммы транзакции. Сумма транзации должна быть больше нуля.");

            _cmd.Amount = amount;

            await SubmitTransactionAsync();

            return new Menu(_services);
        }
        else
        {
            Debug.Assert(false);
            throw new ApplicationException("Unexpected");
        }

        return this;
    }

    public async Task ShowAsync()
    {
        if (_cmd.Id <= 0)
        {
            Console.WriteLine("Введите Id:");
        }
        else if (_cmd.TransactionDate == default(DateTime))
        {
            Console.WriteLine("Введите дату в формате ДД.ММ.ГГГГ:");
        }
        else if (_cmd.Amount == 0M)
        {
            Console.WriteLine("Введите сумму:");
        }
        else
        {
            await Task.CompletedTask;
            Debug.Assert(false);
            throw new ApplicationException("Unexpected");
        }
    }

    /// <summary>
    /// Выполняет команду добавления транзакции
    /// </summary>
    /// <returns></returns>
    private async Task SubmitTransactionAsync()
    {
        try
        {
            var commandDispatcher = _services.GetRequiredService<ICommandDispatcher>();
            await commandDispatcher.DispatchAsync(_cmd);

            Console.WriteLine("[OK]");
        }
        catch (Exception)
        {
            // Сбросить заполненные значения
            _cmd = new AddTransactionCommand();
            throw;
        }
    }
}
