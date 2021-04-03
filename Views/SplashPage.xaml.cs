using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ToDoList.Views
{
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();

            Continue();
        }

        public async void Continue(){
            
            await Navigation.PushAsync(new MainDisplay());
        }
    }
}
