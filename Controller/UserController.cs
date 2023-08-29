using System;
using System.Collections.Generic;
using TaskManagementApp.Models;
using TaskManagementApp.Services;

namespace TaskManagementApp.ConsoleApp
{
    public class UserController
    {
        private readonly ProjectService _projectService;
        private readonly TaskService _taskService;
        private readonly AuthenticationController _authenticationController;

        public UserController(ProjectService projectService, TaskService taskService, AuthenticationController authenticationController)
        {
            _projectService = projectService;
            _taskService = taskService;
            _authenticationController = authenticationController;
        }

        public void UserMenu()
        {
            Console.WriteLine("User Dashboard");
            Console.WriteLine("1. View Available Projects");
            Console.WriteLine("2. Select Project");
            Console.WriteLine("3. View Tasks in Project");
            Console.WriteLine("4. Accept Task");
            Console.WriteLine("5. Update Task Status");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ViewAvailableProjects();
                    break;
                case 2:
                    SelectProject();
                    break;
                case 3:
                    ViewTasksInProject();
                    break;
                case 4:
                    AcceptTask();
                    break;
                case 5:
                    UpdateTaskStatus();
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }

        public void ViewAvailableProjects()
        {
            List<Project> availableProjects = _projectService.GetAvailableProjects();
            if (availableProjects.Count > 0)
            {
                Console.WriteLine("Available Projects:");
                foreach (var project in availableProjects)
                {
                    Console.WriteLine($"{project.Id}. {project.Name}");
                }
            }
            else
            {
                Console.WriteLine("No available projects.");
            }
        }

        public void SelectProject()
        {
            Console.Write("Enter project ID to select: ");
            int projectId = int.Parse(Console.ReadLine());

            var project = _projectService.GetProjectById(projectId);
            if (project != null)
            {
                // Mark the project as selected (update its IsSelected property, assuming you have one)
                project.IsSelected = true;
                _projectService.UpdateProject(project);

                Console.WriteLine($"Project '{project.Name}' selected.");
            }
            else
            {
                Console.WriteLine("Project not found.");
            }
        }

        public void ViewTasksInProject()
        {
            Console.Write("Enter project ID: ");
            int projectId = int.Parse(Console.ReadLine());

            var project = _projectService.GetProjectById(projectId);
            if (project != null)
            {
                Console.WriteLine($"Tasks in Project '{project.Name}':");
                foreach (var task in project.Tasks)
                {
                    Console.WriteLine($"{task.Id}. {task.title} (Status: {task.TaskStatus})");
                }
            }
            else
            {
                Console.WriteLine("Project not found.");
            }
        }

        public void AcceptTask()
{
    Console.Write("Enter task ID to accept: ");
    int taskId = int.Parse(Console.ReadLine());

    var task = _taskService.GetTaskById(taskId);
    if (task != null)
    {
        var loggedInUser = _authenticationController.GetLoggedInUser();
        if (loggedInUser != null)
        {
            task.AssignedUser = loggedInUser;
            task.TaskStatus = TaskManagementApp.Models.TaskStatus.InProgress;


            _taskService.UpdateTask(task);

            Console.WriteLine($"Task '{task.title}' accepted.");
        }
        else
        {
            Console.WriteLine("You are not logged in.");
        }
    }
    else
    {
        Console.WriteLine("Task not found.");
    }
}


        public void UpdateTaskStatus()
        {
            Console.Write("Enter task ID: ");
            int taskId = int.Parse(Console.ReadLine());

            var task = _taskService.GetTaskById(taskId);
            if (task != null)
            {
                Console.WriteLine("Select task status:");
                Console.WriteLine("1. Completed");
                Console.WriteLine("2. In Progress");
                Console.WriteLine("3. Pending");
                Console.Write("Enter your choice: ");
                int statusChoice = int.Parse(Console.ReadLine());

                switch (statusChoice)
                {
                    case 1:
                        task.TaskStatus = TaskManagementApp.Models.TaskStatus.Completed; // Fully qualify the enum value
                        break;
                    case 2:
                        task.TaskStatus = TaskManagementApp.Models.TaskStatus.InProgress; // Fully qualify the enum value
                        break;
                    case 3:
                        task.TaskStatus = TaskManagementApp.Models.TaskStatus.Pending; // Fully qualify the enum value
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        return;
                }

                _taskService.UpdateTask(task);
                Console.WriteLine($"Task status updated to {task.TaskStatus}");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
        }
    }
}