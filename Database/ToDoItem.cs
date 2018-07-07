using System;
using SQLite;

namespace ToDoList.Database
{
    public class ToDoItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string TaskName { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }

        public ToDoItem(){
            TaskName = string.Empty;
            Created = DateTime.UtcNow;
            IsActive = false;
        }
    }
}
