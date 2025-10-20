# To Do List App - MVVM WPF

A simple todo list application built with WPF using the MVVM (Model-View-ViewModel) pattern.

## Prerequisites

- Visual Studio 2022 (or later)
- .NET 8.0 SDK
- Windows operating system

## How to Run

1. Clone this repository
2. Open `To Do List App.sln` in Visual Studio
3. Press F5 to build and run

## Project Structure
```
/Models          - Data classes (ToDoItem, ToDoManager)
/ViewModels      - UI logic (MainViewModel)
/Views           - XAML UI files (MainWindow.xaml)
/Commands        - Command infrastructure (RelayCommand)
```

## Features

- Add new tasks
- Delete selected tasks
- Mark tasks as complete/incomplete
- View task details in a list

## Learning Resources

This project demonstrates:
- MVVM architecture pattern
- Data binding in WPF
- INotifyPropertyChanged implementation
- ICommand pattern with RelayCommand
- ObservableCollection for UI updates
```