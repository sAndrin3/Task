using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Data;
using TaskManagementApp.Services;

namespace TaskManagementApp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create DbContext instance
            ApplicationDbContext context = new ApplicationDbContext();

            // Create instances of services
            ProjectService projectService = new ProjectService(context);
            TaskService taskService = new TaskService(context);
            UserService userService = new UserService(context);
            AuthenticationController authenticationController = new AuthenticationController(userService);

            authenticationController.StartSession();
        }
    }
}
