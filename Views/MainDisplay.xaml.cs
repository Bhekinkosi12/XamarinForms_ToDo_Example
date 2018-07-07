using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToDoList.Database;

using Xamarin.Forms;

namespace ToDoList.Views
{
    public partial class MainDisplay : ContentPage
    {

        TodoDatabase db { get; set; }
        ObservableCollection<ToDoItem> Items { get; set; }

        public MainDisplay()
        {
            InitializeComponent();

            db = new TodoDatabase();
			
            LoadTodoItems();
        }

        public async void LoadTodoItems(){
            Items = new ObservableCollection<ToDoItem>();
            ListViewToDo.ItemsSource = Items;
            var dbitems = await db.GetTodayTasksAsync();

			if (dbitems.Count() > 0)
			{
                foreach(var item in dbitems){
                    Items.Add(item);    
                }

				btnClearItems.IsVisible = true;
			}
			else
			{
				btnClearItems.IsVisible = false;
			}
        }

        public async void ItemCountCheck(){
            
            var dbitems = await db.GetTodayTasksAsync();

            if (dbitems.Count() > 0)
            {
                btnClearItems.IsVisible = true;
            }
            else{
				btnClearItems.IsVisible = false;
            }
        }

        public async void AddItem(object sender, EventArgs e){

            if (!string.IsNullOrEmpty(txtItemName.Text))
            {
                ToDoItem tdi = new ToDoItem() { TaskName = txtItemName.Text, Created = DateTime.UtcNow, IsActive = true };

                await db.InsertTaskAsync(tdi);
                txtItemName.Text = "";

                Items.Add(tdi);

            }

            ItemCountCheck();
        }

        public async void OnDelete(object sender, EventArgs e){
            var mi = (MenuItem)sender;

            if (mi.CommandParameter != null)
            {
                var dbitems = await db.GetTodayTasksAsync();
                var item = dbitems.Where(x => x.Id == Convert.ToInt32(mi.CommandParameter)).FirstOrDefault();
                var listItem = Items.Where(x => x.Id == Convert.ToInt32(mi.CommandParameter)).FirstOrDefault();

                if (listItem != null){
                    Items.Remove(listItem);
				}

                if(item != null){
                    await db.RemoveTaskAsync(item);
                }

                ItemCountCheck();
            }
        }

        public async void ClearAllItems(object sender, EventArgs e)
        {
            var dbitems = await db.GetTodayTasksAsync();

            foreach (var item in dbitems)
            {
                await db.RemoveTaskAsync(item);
            }

            Items.Clear();
            ItemCountCheck();
		}
    }
}
