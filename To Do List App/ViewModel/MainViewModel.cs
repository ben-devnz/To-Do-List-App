using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input; // for ICommand
using To_Do_List_App.Commands; // for RelayCommand
using To_Do_List_App.Models;

namespace To_Do_List_App.ViewModel
{
    // MainViewModel: Acts as a bridge between the Model (ToDoManager) and the view (UI)
    // Manages the collection of tasks and will handle user commands (add, delete, toggle)
    public class MainViewModel : INotifyPropertyChanged
    {
        // ==================== BOILERPLATE (INotifyPropertyChanged) ====================

        // Event required by INotifyPropertyChanged - notifies the UI of property changes
        public event PropertyChangedEventHandler? PropertyChanged;

        // Raises PropertyChanged event - Call this in property setters
        // [CallerMemberName] auto-fills the property name
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // ==================== PRIVATE FIELDS ====================

        // Private field - holds the business logic layer (Model)
        // The view never directly accesses this - only the ViewModel uses it
        private ToDoManager _manager;

        // Private backing field - Holds selectedTask
        private ToDoItem _selectedTask;

        // ==================== PUBLIC PROPERTIES ====================

        // Public Property - Tracks which task is currently selected in the UI
        // When a user clicks a task in ListView, this gets set automatically via binding
        // Used by delete and toggle commands to know which task to operate on
        public ToDoItem SelectedTask
        {
            get
            {
                return _selectedTask;
            }
            set
            {
                _selectedTask = value;
                OnPropertyChanged(); // Notify the UI that selection changed

                // Tell commands to re-check their CanExecute
                (DeleteTaskCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (ToggleCompleteCommand as RelayCommand)?.RaiseCanExecuteChanged();

            }
        }

        // Public property - the UI binds to this collection
        // ObservableCollection automatically notifies the UI when items are added/removed
        // This is what makes the ListBox/ListView update automatically in the view
        public ObservableCollection<ToDoItem> Tasks { get; set; }

        // ==================== COMMANDS ====================
        public ICommand AddTaskCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }
        public ICommand ToggleCompleteCommand { get; set; }

        // ==================== CONSTRUCTOR ====================
        // Constructor - runs when a MainViewModel is created
        // Initializes the manager and loads the initial data
        public MainViewModel()
        {
            // Creates the instance of ToDoManager (Our business logic/Model layer)
            _manager = new ToDoManager();

            // Load all tasks from ToDoManager into our observable collection
            // GetAllTasks() returns an IEnumarable<ToDoItem> from the model
            // We wrap it in ObservableCollection so the UI can listen for changes
            Tasks = new ObservableCollection<ToDoItem>(_manager.GetAllTasks());

            // Initialize Commands
            AddTaskCommand = new RelayCommand(
                executeMethod: (param) => AddTask(),
                CanExecuteMethod: (param) => true
            );
            DeleteTaskCommand = new RelayCommand(
                executeMethod: (param) => DeleteTask(),
                // If no task is selected CanExecuteMethod returns false
                CanExecuteMethod: (param) => SelectedTask != null
            );
            ToggleCompleteCommand = new RelayCommand(
                executeMethod: (param) => ToggleComplete(),
                // If no task is selected CanExecuteMethod returns false
                CanExecuteMethod: (param) => SelectedTask != null
            );
        }

        // ==================== PRIVATE METHODS ====================
        private void AddTask()
        {
            // Create a new Task
            // Basic implementation until the new AddToDoView is working
            var newTask = new ToDoItem
            {
                Name = "New Task",
                Details = "Add Details Here"
            };

            // Add to Model Layer
            _manager.AddTask(newTask);

            // Add to ViewModel collection (UI will auto-update)
            Tasks.Add(newTask);
        }

        // Deletes the currently selected task
        // Called by DeleteTaskCommand when the delete button is clicked
        private void DeleteTask()
        {
            // Only delete if a task is selected (defensive check)
            if(SelectedTask != null)
            {
                // Remove from the model layer (business logic/data storage)
                _manager.RemoveTask(SelectedTask.Id);

                // Remove from the ViewModel collection (Triggers the UI update via ObservableCollection)
                Tasks.Remove(SelectedTask);
            }
        }
        // Marks the selected task as complete or incomplete depending on current state
        // Called by ToggleCompleteCommand when the mark as complete button is clicked
        private void ToggleComplete()
        {
            // Only mark as complete or incomplete if there is a task selected
            if (SelectedTask != null)
            {
                // Save a reference of the selected task before removing
                var taskToToggle = SelectedTask;

                // Toggle IsComplete in the Model Layer
                _manager.ToggleComplete(taskToToggle.Id);

                // Remove and re-add to force ObservableCollection to notify the UI of changes
                // This refreshes the item in the ListView
                Tasks.Remove(taskToToggle);
                Tasks.Add(taskToToggle);
            }
        }
    }
}
