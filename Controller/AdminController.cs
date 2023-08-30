using System;
using TaskManagementApp.Models;
using TaskManagementApp.Services;

namespace TaskManagementApp.ConsoleApp
{
    public class AdminController
    {
        private readonly ProjectService _projectService;
        private readonly TaskService _taskService;
        private readonly UserService _userService;
        private readonly AuthenticationController _authenticationController;


        public AdminController(ProjectService projectService, TaskService taskService, UserService userService)
        {
            _projectService = projectService;
            _taskService = taskService;
            _userService = userService;
            _authenticationController = new AuthenticationController();
        }

        public void AdminMenu()
        {
            Console.WriteLine("Admin Dashboard");
            Console.WriteLine("**********************");
            Console.WriteLine("0. View Projects");
            Console.WriteLine("1. Create Project");
            Console.WriteLine("2. Update Project");
            Console.WriteLine("3. Delete Project");
            Console.WriteLine("4. Create Task");
            Console.WriteLine("5. Assign Task");
            Console.WriteLine("6. View Task Status");
            Console.WriteLine("7. Logout");
            Console.WriteLine("*************");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 0:
                    ViewAvailableProjects();
                    break;
                case 1:
                    CreateProject();
                    break;
                case 2:
                    UpdateProject();
                    break;
                case 3:
                    DeleteProject();
                    break;
                case 4:
                    Console.Write("Enter project ID to associate the task with: ");
                    int projectId = int.Parse(Console.ReadLine());
                    CreateTask(projectId); // Call the modified CreateTask method
                    break;

                case 5:
                    AssignTask();
                    break;
                case 6:
                    ViewTaskStatus();
                    break;
                case 7:
                    Console.WriteLine("Exiting user dashboard...");
                    _authenticationController.StartSession();
                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }

        public void CreateProject()
        {
            Console.Write("Enter project name: ");
            string projectName = Console.ReadLine();

            var newProject = new Project
            {
                Name = projectName
            };

            _projectService.CreateProject(newProject);
            Console.WriteLine("New project created!");
        }

        public void UpdateProject()
        {
            Console.WriteLine("Current Projects:");
            var projects = _projectService.GetAllProjects();
            foreach (var proj in projects)
            {
                Console.WriteLine($"Project ID: {proj.Id}, Name: {proj.Name}");
            }

            Console.Write("Enter project ID to update: ");
            int projectId = int.Parse(Console.ReadLine());

            var project = _projectService.GetProjectById(projectId);
            if (project != null)
            {
                Console.Write("Enter new project name: ");
                string newProjectName = Console.ReadLine();
                project.Name = newProjectName;

                _projectService.UpdateProject(project);
                Console.WriteLine("Project updated!");
            }
            else
            {
                Console.WriteLine("Project not found.");
            }
        }

        public void ViewAvailableProjects()
        {
            Console.WriteLine("Available Projects:");
            var availableProjects = _projectService.GetAvailableProjects();
            foreach (var project in availableProjects)
            {
                Console.WriteLine($"Project ID: {project.Id}, Name: {project.Name}");
            }
        }


        public void DeleteProject()
        {
            Console.WriteLine("Current Projects:");
            var projects = _projectService.GetAllProjects();
            foreach (var proj in projects)
            {
                Console.WriteLine($"Project ID: {proj.Id}, Name: {proj.Name}");
            }

            Console.Write("Enter project ID to delete: ");
            int projectId = int.Parse(Console.ReadLine());

            var project = _projectService.GetProjectById(projectId);
            if (project != null)
            {
                _projectService.DeleteProject(project);
                Console.WriteLine("Project deleted!");
            }
            else
            {
                Console.WriteLine("Project not found.");
            }
        }



        public void CreateTask(int projectId) // Pass the projectId to associate the task with a project
        {
            Console.Write("Enter task title: ");
            string taskTitle = Console.ReadLine();

            Console.WriteLine("Enter username of the assigned user: ");
            string username = Console.ReadLine();

            var user = _userService.GetUserByUsername(username);
            if (user != null)
            {
                var newTask = new Tasker
                {
                    title = taskTitle,
                    TaskStatus = TaskManagementApp.Models.TaskStatus.Pending,
                    Project = _projectService.GetProjectById(projectId), // Associate the task with the project
                    AssignedUser = user,
                };

                _taskService.CreateTask(newTask);
                Console.WriteLine("New task created!");
            }
            else
            {
                System.Console.WriteLine("User not found");
            }
        }




        public void AssignTask()
        {
            Console.WriteLine("Current Tasks:");
            var tasks = _taskService.GetAllTasks();
            foreach (var t in tasks)
            {
                Console.WriteLine($"Task ID: {t.Id}, Title: {t.title}, Status: {t.TaskStatus}");
            }

            Console.Write("Enter task ID to assign: ");
            int taskId = int.Parse(Console.ReadLine());

            var task = _taskService.GetTaskById(taskId);
            if (task != null)
            {
                Console.Write("Enter username to assign the task: ");
                string username = Console.ReadLine();

                var user = _userService.GetUserByUsername(username);
                if (user != null)
                {
                    task.AssignedUser = user;
                    _taskService.UpdateTask(task);
                    Console.WriteLine("Task assigned!");
                }
                else
                {
                    Console.WriteLine("User not found.");
                }
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
        }


        public void ViewTaskStatus()
        {
            Console.WriteLine("Task Status List:");
            foreach (var task in _taskService.GetAllTasks())
            {
                Console.WriteLine($"Task ID: {task.Id}, Title: {task.title}, Status: {task.TaskStatus}");
            }
        }

    }
}
