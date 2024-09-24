using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;
using ToDo.Models;

namespace YourNamespace.Controllers
{
    public class ToDoController : Controller
    {
        [HttpGet]
        public IActionResult GetItems()
        {
            var todoList = GetFromLocalStorage();
            return Json(todoList);
        }

        [HttpPost]
        public IActionResult AddItem([FromBody] ToDoItem newItem)
        {
            var todoList = GetFromLocalStorage();
            newItem.Id = todoList.Count + 1;
            todoList.Add(newItem);
            SaveToLocalStorage(todoList);
            return Ok(newItem);
        }

        [HttpDelete]
        public IActionResult RemoveItem(int id)
        {
            var todoList = GetFromLocalStorage();
            var itemToRemove = todoList.Find(x => x.Id == id);
            if (itemToRemove != null)
            {
                todoList.Remove(itemToRemove);
                SaveToLocalStorage(todoList);
                return Ok();
            }
            return NotFound();
        }

        private List<ToDoItem> GetFromLocalStorage()
        {
            var todoJson = HttpContext.Session.GetString("todoList");
            return string.IsNullOrEmpty(todoJson) ? new List<ToDoItem>() : JsonConvert.DeserializeObject<List<ToDoItem>>(todoJson);
        }

        private void SaveToLocalStorage(List<ToDoItem> todoList)
        {
            var todoJson = JsonConvert.SerializeObject(todoList);
            HttpContext.Session.SetString("todoList", todoJson);
        }
    }
}
