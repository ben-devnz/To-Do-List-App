using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Key Concepts here:

// ENCAPSULATION: _tasks is private, so all access must go through our public methods. This gives us control
// LINQ: FirstOrDefault is a powerful search method. The t => t.Id == id is a lambda expression that means "for each item t, check if t.Id equals the id we are looking for
// DEFENSIVE COPYING: ToList() prevents external code from breaking our internal data.
// AUTO INCREMENT IDS: _nextId++ ensures every task gets a unique identifier

namespace To_Do_List_App.Models
{
    // Manages all to do items - "Business Logic" Layer
    // Handles creating, reading, updating and deleting tasks (CRUD operations)
    public class ToDoManager
    {
        // Private field - stores all tasks in memory
        // List<T> is a resizable array that can grow/shrink as needed
        // Private means only this class can access it (encapsulation)
        private List<ToDoItem> _tasks;

        // Private field - tracks what ID number to assign to the next new task
        // This ensures each task generated gets a unique ID
        private int _nextId;

        // Constructor - runs when you create a new ToDoManager instance
        // Example: var manager = new ToDoManager();
        public ToDoManager()
        {
            // Initialize _tasks with some sample data
            // This is "Seed Data" - placeholder tasks so the app isn't empty
            _tasks = new List<ToDoItem>
            {
                // Object initializer syntax - creates a new ToDoItem and sets its properties
                new ToDoItem {Id=1,Name="Mow Lawns",Details="Mow the lawn at the front", IsCompleted=false},
                new ToDoItem { Id = 2, Name = "Empty Rubbish", Details = "Empty rubbish into wheely bin", IsCompleted = false },
                new ToDoItem { Id = 3, Name = "Wash Dishes", Details = "Wash the pots and pans", IsCompleted = false }
            };

            // Set _nextId to 4 since we already used 1, 2, and 3
            // The next task will get ID 4, then 5 etc
            _nextId = 4;
        }

        // Public method - returns all tasks
        // Return type is IEnumerable<ToDoItem> which is a read only sequence of items
        public IEnumerable<ToDoItem> GetAllTasks()
        {
            // ToList() creates a NEW list (a copy) from _tasks
            // This prevents external code from modifying our private _tasks collection
            // This is defensive programming - protects our data integrity
            return _tasks.ToList();
        }

        // Public method - adds a new task to the collection
        // Parameter: task - the ToDoItem object to add
        // Return type: void - doesn't return anything
        public void AddTask(ToDoItem task)
        {
            // Assign a unique ID to the new task
            // _nextId++ uses the current ID, then increments it by 1
            // So if _nextId is 4, task.Id becomes 4, and _nextId becomes 5
            task.Id = _nextId++;

            // Add the task to our private list
            _tasks.Add(task);
        }

        // Public method - removes a new task from the collection
        // Parameter: ID - the the unique ID of the task to remove
        public void RemoveTask(int id)
        {
            // FirstOrDefault is a LINQ method that searches the list
            // t => t.Id == id is a lambda expression (think "where t.Id equals id")
            // It finds the FIRST task where the Id matches, or returns NULL if not found
            var task = _tasks.FirstOrDefault(t => t.Id == id);

            // Check if we actually found a task (NULL check)
            // If task is NULL, this prevents trying to remove something that doesn't exist
            if(task != null)
            {
                _tasks.Remove(task);
            }
        }

        // Public method - updates an existing task with new information
        // Parameter: updatedTask - a ToDoItem with the same Id but potentially new values
        public void UpdateTask(ToDoItem updatedTask)
        {
            // Find the existing task in our list by matching the Id
            // FirstOrDefault returns the new task if found, or null if not found
            var task = _tasks.FirstOrDefault(t => t.Id == updatedTask.Id);

            // Only update if we found a task
            if(task != null)
            {
                task.Name = updatedTask.Name;
                task.Details = updatedTask.Details;
                task.IsCompleted = updatedTask.IsCompleted;
            }
        }

        // Public method - flips the IsCompleted status of a task
        // Parameter: id - the unique ID of the task to toggle
        public void ToggleComplete(int id)
        {
            // Find the task by its Id
            var task = _tasks.FirstOrDefault(t => t.Id == id);

            // Only toggle if we found the task
            if(task != null)
            {
                // ! is the NOT operator - it flips true to false, or false to true
                // So if IsCompleted is true, it becomes false (and vice versa)
                task.IsCompleted = !task.IsCompleted;
            }
        }

    }
}
