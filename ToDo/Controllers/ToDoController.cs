using Microsoft.AspNetCore.Mvc;
using ToDo.SSE.Controllers;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class ToDoController : Controller
    {
        private static List<ToDoItem> _todoItems = new();

        public IActionResult Index()
        {
            return View(_todoItems);
        }

        [HttpPost]
        public IActionResult Add(string task)
        {
            var newItem = new ToDoItem
            {
                Id = _todoItems.Count + 1,
                Task = task,
                IsCompleted = false
            };
            _todoItems.Add(newItem);
            SseController.SendUpdate("Task list updated");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            var item = _todoItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _todoItems.Remove(item);
                SseController.SendUpdate("Task list updated");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ToggleComplete(int id)
        {
            var item = _todoItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.IsCompleted = !item.IsCompleted;
                SseController.SendUpdate("Task list updated");
            }
            return RedirectToAction("Index");
        }
    }
}
