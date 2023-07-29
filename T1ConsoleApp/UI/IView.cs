namespace T1.UI;

/// <summary>
/// Абстрактная реализация UI представлений (форм, меню и пр.)
/// </summary>
internal interface IView
{
    /// <summary>
    /// Отображает реализацию IView
    /// </summary>
    Task ShowAsync();

    /// <summary>
    /// Контроллер
    /// </summary>
    /// <param name="text">Текст, введенный пользователем</param>
    /// <returns>Дальнейший IView (для сохранения состояния реализации IView вернуть this).
    /// Если возвращается null, программа завершается.</returns>
    Task<IView?> HandleTextInputAsync(string text);
}
