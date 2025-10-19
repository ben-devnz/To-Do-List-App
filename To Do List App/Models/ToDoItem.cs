using System;
namespace To_Do_List_App.Models
{
    public class ToDoItem
    {
        // Public Id property (type: Integer)
        public int Id { get; set; }
        // Public Name property (type: String)
        public string Name { get; set; } = string.Empty;
        // Public Name Details property (type: String)
        public string Details { get; set; } = string.Empty;
        // Public IsCompleted property (type: Boolean)
        public bool IsCompleted { get; set; }
        // Public CreatedDate property (type: DateTime)
        public DateTime CreatedDate { get; set; }
        // Public optional DueDate property (type: DateTime)
        public DateTime? DueDate { get; set; }
        // Constructor when a new ToDoItem is instanced
        // Example: new ToDoItem { }
        public ToDoItem() 
        {
            CreatedDate = DateTime.Now;
            IsCompleted = false;       
        }
    }
}
