using Microsoft.AspNetCore.Mvc;
using ToDo.Models; 
using System.Collections.Generic;
using System.Linq;

namespace ToDo.Controllers 
{
    public class ToDoController : Controller
    {
        private static List<ToDoItem> todoItems = new List<ToDoItem>();
        private static int nextId = 1; 

        public IActionResult Index()
        {
            return View(todoItems);
        }

        [HttpPost("todo/add")]
        public IActionResult Add(string task)
        {
            var todo = new ToDoItem
            {
                Id = nextId++,
                Task = task,
                IsCompleted = false 
            };
            todoItems.Add(todo);
            return RedirectToAction("Index");
        }

        [HttpDelete("todo/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var item = todoItems.FirstOrDefault(t => t.Id == id);
            if (item != null)
            {
                todoItems.Remove(item);
                return Ok();
            }
            return NotFound();
        }

        [HttpPost("todo/complete/{id}")]
        public IActionResult Complete(int id)
        {
            var item = todoItems.FirstOrDefault(t => t.Id == id);
            if (item != null)
            {
                item.IsCompleted = true;
                return Ok();
            }
            return NotFound();
        }
    }
}
