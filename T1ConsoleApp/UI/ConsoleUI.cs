using System.Diagnostics;
using System.Globalization;
using Microsoft.Extensions.Hosting;
using T1.Abstractions;

namespace T1.UI;

/// <summary>
/// ������� ���������� ����������� UI � ���� �������� �������
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

        // ���������, ���� ��������� ������� ���������������� � ���������� ����������� � �������
        await Task.Delay(1000);

        try
        {
            IView startView = new Menu(_services);
            IView currentView = startView;
            while (!stoppingToken.IsCancellationRequested)
            {
                // ���������� ������� IView
                await currentView.ShowAsync();

                string text = Console.ReadLine() ?? "";

                // ���������� ���� �����������
                IView? nextView = null;
                try
                {
                    nextView = await currentView.HandleTextInputAsync(text.Trim());
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"������: {ex.Message}");

                    // ��������� � ���������� IView
                    currentView = startView;
                    continue;
                }

                // ���������� �� ��������� ��������� (��� ����������� IView)
                if (null == nextView)
                {
                    // ������������ ���������� ���������
                    _appTerminator.StopApp();
                    break;
                }

                currentView = nextView;
            }
        }
        finally
        {
            // ������������ ������
            CultureInfo.CurrentCulture = originalCultureInfo;
        }
    }
}
