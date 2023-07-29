using System.ComponentModel.DataAnnotations;

namespace T1.UI;

/// <summary>
/// Меню выбора команды
/// </summary>
internal class Menu : IView
{
    private readonly IServiceProvider _services;

    internal Menu(IServiceProvider services)
    {
        _services = services;
    }

    public async Task<IView?> HandleTextInputAsync(string text)
    {
        switch (text)
        {
            case "add":
                return new AddTransactionForm(_services);
            case "get":
                return new GetTransactionForm(_services);
            case "exit":
                return null;
            default:
                await Task.CompletedTask;
                throw new ValidationException("Неизвестная команда");
        };
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("Введите команду (add, get, exit):");
        await Task.CompletedTask;
    }
}
