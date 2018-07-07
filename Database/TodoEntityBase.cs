using System;
using SQLite;

namespace ToDoList.Database
{
    public class TodoEntityBase : ITodoEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public TodoEntityBase()
        {
        }
    }
}
