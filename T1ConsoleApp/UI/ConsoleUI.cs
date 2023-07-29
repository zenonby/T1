using System.Diagnostics;
using System.Globalization;
using Microsoft.Extensions.Hosting;
using T1.Abstractions;

namespace T1.UI;

/// <summary>
/// Главная реализация консольного UI в виде фонового сервиса
/// </summary>
public class ConsoleUI : BackgroundService
{
    private readonly IAppTerminator _appTerminator;
    private readonly IServiceProvider _services;

    public ConsoleUI(
        IAppTerminator appTerminator,
        IServiceProvider services)
    {
        _appTerminator = appTerminator;
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var originalCultureInfo = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        // Подождать, пока остальные сервисы инициализируются и перестанут логгировать в консоль
        await Task.Delay(1000);

        try
        {
            IView startView = new Menu(_services);
            IView currentView = startView;
            while (!stoppingToken.IsCancellationRequested)
            {
                // Отобразить текущий IView
                await currentView.ShowAsync();

                string text = Console.ReadLine() ?? "";

                // Обработать ввод пользвателя
                IView? nextView = null;
                try
                {
                    nextView = await currentView.HandleTextInputAsync(text.Trim());
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Ошибка: {ex.Message}");

                    // Вернуться к начальному IView
                    currentView = startView;
                    continue;
                }

                // Необходимо ли завершить обработку (нет дальнейшего IView)
                if (null == nextView)
                {
                    // Инициировать завершение программы
                    _appTerminator.StopApp();
                    break;
                }

                currentView = nextView;
            }
        }
        finally
        {
            // Восстановить локаль
            CultureInfo.CurrentCulture = originalCultureInfo;
        }
    }
}
