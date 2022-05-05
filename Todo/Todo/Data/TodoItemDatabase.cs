using Firebase.Database;
using Firebase.Database.Query;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Data
{
    public class TodoItemDatabase
    {
       
        FirebaseClient firebase;

        public static readonly AsyncLazy<TodoItemDatabase> Instance = new AsyncLazy<TodoItemDatabase>(async () =>
        {
            var instance = new TodoItemDatabase();
            return instance;
        });

        public TodoItemDatabase()
        {
            firebase = new FirebaseClient("https://xamarinfirebasedatabase-b6202-default-rtdb.firebaseio.com/");
           
        }

        public async Task<List<TodoItem>> GetItemsAsync()
        {
           return (await firebase.Child("TodoItem").OnceAsync<TodoItem>()).Select(item => new TodoItem
           {
                ID = item.Object.ID,
                Name = item.Object.Name,
                Notes = item.Object.Notes,
                Done = item.Object.Done
           }).ToList();
        }
        public async Task AddTodoItem(int id, string Name, string Notes, bool Done)
        {
            await firebase.Child("TodoItem").PostAsync(new TodoItem() { ID = id , Name = Name,Notes=Notes,Done=Done });
        }

        public async Task UpdateTodoItem(int id, string Name, string Notes, bool Done)
        {
            var toUpdatePerson = (await firebase
              .Child("TodoItem")
              .OnceAsync<TodoItem>()).Where(a => a.Object.ID == id).FirstOrDefault();

            await firebase
              .Child("TodoItem")
              .Child(toUpdatePerson.Key)
              .PutAsync(new TodoItem() { ID = id, Name = Name, Notes = Notes, Done = Done });
        }

        public async Task DeleteTodoItem(int id)
        {
            var toDeletePerson = (await firebase
              .Child("TodoItem")
              .OnceAsync<TodoItem>()).Where(a => a.Object.ID == id).FirstOrDefault();
            await firebase.Child("TodoItem").Child(toDeletePerson.Key).DeleteAsync();
        }
    }
}