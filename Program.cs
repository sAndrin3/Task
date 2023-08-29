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

            // Pass all the required services to the AuthenticationController
            AuthenticationController authenticationController = new AuthenticationController(userService, projectService, taskService);

            authenticationController.StartSession();
        }
    }
}
