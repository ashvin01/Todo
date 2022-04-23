using System;
using Todo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new TodoListPage())
            {
                BarTextColor = Color.White,
                BarBackgroundColor = (Color)App.Current.Resources["primaryGreen"]
            };
            //MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
