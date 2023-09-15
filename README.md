# CQRS example.
## Overview
Simple console application which implements two use cases:
* adding a transaction (command)
* getting a transaction by its ID (query)

## Project structure
Solution - T1.sln

Main projects:
* Console application which demonstrates CQRS - T1ConsoleApp
* Test project - T1Test

Other projects:
* Abstractions - interfaces used across the project
* Model - entities (actually just one entity: Transaction)
* Db - in memory DB/storage
* Command - as the name suggests, commands
* Query - queries, as it stands

UI is implemented in T1ConsoleApp.UI.
Main UI class is ConsoleUI.cs, which is implemented as BackgroundService.

Other UI classes:
* Menu - "add", "get", "exit" commands
* AddTransactionForm - for adding a transaction
* GetTransactionForm - for getting a transaction by its ID
