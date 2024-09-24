document.getElementById("add-task").addEventListener("click", addTask);

function addTask() {
    const taskTitle = document.getElementById("new-task").value;

    if (taskTitle.trim() === "") {
        alert("Назва завдання не може бути порожньою");
        return;
    }

    fetch("/api/todo", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ title: taskTitle, isCompleted: false })
    })
        .then(response => response.json())
        .then(task => {
            appendTaskToList(task);
            document.getElementById("new-task").value = ""; // Очищення поля
        });
}

function appendTaskToList(task) {
    const taskItem = document.createElement("li");
    taskItem.textContent = task.title;
    document.getElementById("tasks").appendChild(taskItem);
}
