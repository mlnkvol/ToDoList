namespace ToDo.Models // Змініть на ваш простір імен
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public bool IsCompleted { get; set; } // Додане поле
    }
}
