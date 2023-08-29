using System;
using System.Collections.Generic;
using TaskManagementApp.Models;
using TaskManagementApp.Services;

namespace TaskManagementApp.ConsoleApp
{
    public class AuthenticationController
    {
          private AdminController _adminController;
            private UserController _userController;
          
        
        private readonly UserService _userService;
        private User _loggedInUser;
        

         public User GetLoggedInUser()
    {
        return _loggedInUser;
    }

        public AuthenticationController(UserService userService, ProjectService projectService, TaskService taskService)
        {
            _userService = userService;
             _adminController = new AdminController(projectService, taskService, userService); 
             _userController = new UserController(projectService, taskService);
        }

        public AuthenticationController(){

        }

        public void Register(string username, string password, UserRole role)
        {
            var newUser = new User { username = username, password = password, Role = role, Tasks = new List<Tasker>() };
            _userService.CreateUser(newUser);
            Console.WriteLine("Registration successful. Please log in.");
        }

        public bool Login(string username, string password)
        {
            var user = _userService.GetUserByUsername(username);

            if (user != null && user.password == password)
            {
                _loggedInUser = user;
                Console.WriteLine("Login successful.");
                return true;
            }
            else
            {
                Console.WriteLine("Login failed.");
                return false;
            }
        }

        // Inside the StartSession method
public void StartSession()
{
    Console.WriteLine("Welcome to the Task Management App!");

    while (true){
        if (_loggedInUser == null)
    {
        Console.WriteLine("Select an option:");
        Console.WriteLine("**********************");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.WriteLine("3. Exit");
        Console.WriteLine("*************");
        Console.Write("Enter your choice: ");
        int optionChoice = int.Parse(Console.ReadLine());

        switch (optionChoice)
        {
            case 1:
                Console.Write("Enter username: ");
                string username = Console.ReadLine();
                Console.Write("Enter password: ");
                string password = Console.ReadLine();
                if (Login(username, password)) // Check if login is successful
                {
                    NavigateToDashboard(_loggedInUser.Role); // Redirect to the appropriate dashboard
                }
                break;
            case 2:
                Console.Write("Enter username: ");
                string newUsername = Console.ReadLine();
                Console.Write("Enter password: ");
                string newPassword = Console.ReadLine();
                Console.WriteLine("Select role:");
                Console.WriteLine("*************");
                Console.WriteLine("1. Admin");
                Console.WriteLine("2. User");
                Console.WriteLine("*************");
                Console.Write("Enter your choice: ");
                int roleChoice = int.Parse(Console.ReadLine());
                UserRole role = roleChoice == 1 ? UserRole.Admin : UserRole.User;
                Register(newUsername, newPassword, role);
                break;
            case 3:
                Console.WriteLine("Goodbye");
                return;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
    }
    else
    {
        Console.WriteLine("Logged in user: " + _loggedInUser.username + ", Role: " + _loggedInUser.Role);
        NavigateToDashboard(_loggedInUser.Role); // Redirect to the appropriate dashboard
    }
}
    }

    

// Add this method to navigate to the dashboard based on the user's role
public void NavigateToDashboard(UserRole role)
{
    if (role == UserRole.Admin)
    {
        AdminDashboard();
    }
    else
    {
        UserDashboard();
    }
}


        public void AdminDashboard()
        {
            
            Console.WriteLine("Welcome to the Admin Dashboard.");
           _adminController.AdminMenu();
            
            
            
        }

        public void UserDashboard()
        {
            
            Console.WriteLine("Welcome to the User Dashboard.");
            _userController.UserMenu();
        }
    }
}
