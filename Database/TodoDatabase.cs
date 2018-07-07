using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace ToDoList.Database
{
    public class TodoDatabase
    {
        SQLite.SQLiteAsyncConnection Connection { get; }
        public static string Root { get; set; }

        public TodoDatabase()
        {
            var location = "tododb.db3";
            location = System.IO.Path.Combine(Root, location);
            Connection = new SQLite.SQLiteAsyncConnection(location);

            Connection.CreateTableAsync<ToDoItem>();
        }

        public async Task<int> InsertTaskAsync(ToDoItem item){
            return await Connection.InsertAsync(item);
        }

        public async Task<int> RemoveTaskAsync(ToDoItem item){
            return await Connection.DeleteAsync(item);
        }

        public async Task<IEnumerable<ToDoItem>> GetTodayTasksAsync(){
            return await (from item in Connection.Table<ToDoItem>()
                          where item.Created >= DateTime.Today
                          select item).ToListAsync();
        }
    }
}
