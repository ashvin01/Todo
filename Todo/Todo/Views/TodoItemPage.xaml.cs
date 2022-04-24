using System;
using Todo.Data;
using Todo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoItemPage : ContentPage
    {
        TodoItemDatabase firebasedatabase = new TodoItemDatabase();
        public static int rcount=0;
        public TodoItemPage()
        {
            InitializeComponent();
        }
        async void OnSaveClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            if (todoItem.ID <= 0)
            {
                await firebasedatabase.AddTodoItem(Convert.ToInt32(TodoItemPage.rcount + 1), todoItem.Name, todoItem.Notes, todoItem.Done);
                await DisplayAlert("Success", "ToDoItem Added Successfully", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await firebasedatabase.UpdateTodoItem(Convert.ToInt32(todoItem.ID), todoItem.Name, todoItem.Notes, todoItem.Done);
                await DisplayAlert("Success", "ToDoItem Updated Successfully", "OK");
                await Navigation.PopAsync();
            }
        }
        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            await firebasedatabase.DeleteTodoItem(Convert.ToInt32(todoItem.ID));
            await DisplayAlert("Success", "ToDoItem Deleted Successfully", "OK");
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
