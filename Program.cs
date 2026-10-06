using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static List<TaskItem> tasks = new List<TaskItem>();
    static string filePath = "tasks.json";

    static void Main()
    {
        LoadTasks();

        bool running = true;

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("=== C# TASK MANAGER ===");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Complete Task");
            Console.WriteLine("4. Delete Task");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;

                case "2":
                    ViewTasks();
                    break;

                case "3":
                    CompleteTask();
                    break;

                case "4":
                    DeleteTask();
                    break;

                case "5":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void AddTask()
    {
        Console.Write("Enter task name: ");
        string? title = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(title))
        {
            tasks.Add(new TaskItem(title));
            SaveTasks();
            Console.WriteLine("Task added.");
        }
        else
        {
            Console.WriteLine("Task name cannot be empty.");
        }
    }

    static void ViewTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks found.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Your Tasks:");

        for (int i = 0; i < tasks.Count; i++)
        {
            string status = tasks[i].IsCompleted ? "[X]" : "[ ]";
            Console.WriteLine($"{i + 1}. {status} {tasks[i].Title}");
        }
    }

    static void CompleteTask()
    {
        ViewTasks();

        if (tasks.Count == 0)
        {
            return;
        }

        Console.Write("Enter task number to complete: ");

        if (int.TryParse(Console.ReadLine(), out int taskNumber)
            && taskNumber >= 1
            && taskNumber <= tasks.Count)
        {
            tasks[taskNumber - 1].IsCompleted = true;
            SaveTasks();
            Console.WriteLine("Task marked complete.");
        }
        else
        {
            Console.WriteLine("Invalid task number.");
        }
    }

    static void DeleteTask()
    {
        ViewTasks();

        if (tasks.Count == 0)
        {
            return;
        }

        Console.Write("Enter task number to delete: ");

        if (int.TryParse(Console.ReadLine(), out int taskNumber)
            && taskNumber >= 1
            && taskNumber <= tasks.Count)
        {
            tasks.RemoveAt(taskNumber - 1);
            SaveTasks();
            Console.WriteLine("Task deleted.");
        }
        else
        {
            Console.WriteLine("Invalid task number.");
        }
    }

    static void SaveTasks()
    {
        string json = JsonSerializer.Serialize(tasks);
        File.WriteAllText(filePath, json);
    }

    static void LoadTasks()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            List<TaskItem>? loadedTasks =
                JsonSerializer.Deserialize<List<TaskItem>>(json);

            if (loadedTasks != null)
            {
                tasks = loadedTasks;
            }
        }
    }
}