using Microsoft.AspNetCore.Mvc;
using ToDo.Models; // Змініть на ваш простір імен
using System.Collections.Generic;
using System.Linq;

namespace ToDo.Controllers // Змініть на ваш простір імен
{
    public class ToDoController : Controller
    {
        private static List<ToDoItem> todoItems = new List<ToDoItem>();
        private static int nextId = 1; // Для генерації унікальних ID

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
                IsCompleted = false // За замовчуванням задача не виконана
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
