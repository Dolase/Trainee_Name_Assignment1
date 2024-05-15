using System;
using System.Collections.Generic;

class Program
{
    static List<TaskItem> tasks = new List<TaskItem>();

    static void Main(string[] args)
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Task List Application");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Update Task");
            Console.WriteLine("4. Delete Task");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");

            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    AddTask();
                    break;
                case 2:
                    ViewTasks();
                    break;
                case 3:
                    UpdateTask();
                    break;
                case 4:
                    DeleteTask();
                    break;
                case 5:
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void AddTask()
    {
        Console.Write("Enter task title: ");
        string title = Console.ReadLine();
        Console.Write("Enter task description: ");
        string description = Console.ReadLine();
        tasks.Add(new TaskItem { Title = title, Description = description });
        Console.WriteLine("Task added successfully.");
    }

    static void ViewTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available.");
        }
        else
        {
            Console.WriteLine("Tasks:");
            foreach (var task in tasks)
            {
                Console.WriteLine($"Title: {task.Title}, Description: {task.Description}");
            }
        }
    }

    static void UpdateTask()
    {
        ViewTasks();
        Console.Write("Enter the index of the task to update: ");
        int index = int.Parse(Console.ReadLine());
        if (index >= 0 && index < tasks.Count)
        {
            Console.Write("Enter new task title: ");
            string newTitle = Console.ReadLine();
            Console.Write("Enter new task description: ");
            string newDescription = Console.ReadLine();
            tasks[index].Title = newTitle;
            tasks[index].Description = newDescription;
            Console.WriteLine("Task updated successfully.");
        }
        else
        {
            Console.WriteLine("Invalid index.");
        }
    }

    static void DeleteTask()
    {
        ViewTasks();
        Console.Write("Enter the index of the task to delete: ");
        int index = int.Parse(Console.ReadLine());
        if (index >= 0 && index < tasks.Count)
        {
            tasks.RemoveAt(index);
            Console.WriteLine("Task deleted successfully.");
        }
        else
        {
            Console.WriteLine("Invalid index.");
        }
    }
}