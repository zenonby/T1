Солюшн - T1.sln

Основные проекты:
* Консольное приложение - T1ConsoleApp
* Проект тестов - T1Test

Другие проекты:
* Abstractions - интерфейсы
* Model - сущности (транзакция)
* Db - БД в памяти
* Command - команды
* Query - запросы

UI реализован в T1ConsoleApp.UI.
Основной класс - ConsoleUI.cs, реализован как BackgroundService.

UI классы:
* Menu - меню (add, get, exit)
* AddTransactionForm - добавление транзакции
* GetTransactionForm - получение транзакции по Id
