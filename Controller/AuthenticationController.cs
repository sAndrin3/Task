using System;
using System.Collections.Generic;
using TaskManagementApp.Models;
using TaskManagementApp.Services;

namespace TaskManagementApp.ConsoleApp
{
    public class AuthenticationController
    {
        private readonly UserService _userService;
        private User _loggedInUser;

         public User GetLoggedInUser()
    {
        return _loggedInUser;
    }

        public AuthenticationController(UserService userService)
        {
            _userService = userService;
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

        public void StartSession()
        {
            Console.WriteLine("Welcome to the Task Management App!");

            if (_loggedInUser == null)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.Write("Enter your choice: ");
                int optionChoice = int.Parse(Console.ReadLine());

                switch (optionChoice)
                {
                    case 1:
                        Console.Write("Enter username: ");
                        string username = Console.ReadLine();
                        Console.Write("Enter password: ");
                        string password = Console.ReadLine();
                        Login(username, password);
                        break;
                    case 2:
                        Console.Write("Enter username: ");
                        string newUsername = Console.ReadLine();
                        Console.Write("Enter password: ");
                        string newPassword = Console.ReadLine();
                        Console.WriteLine("Select role:");
                        Console.WriteLine("1. Admin");
                        Console.WriteLine("2. User");
                        Console.Write("Enter your choice: ");
                        int roleChoice = int.Parse(Console.ReadLine());
                        UserRole role = roleChoice == 1 ? UserRole.Admin : UserRole.User;
                        Register(newUsername, newPassword, role);
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Logged in user: " + _loggedInUser.username + ", Role: " + _loggedInUser.Role);
                if (_loggedInUser.Role == UserRole.Admin)
                {
                    // Redirect to admin dashboard
                    AdminDashboard();
                }
                else
                {
                    // Redirect to user dashboard
                    UserDashboard();
                }
            }
        }

        private void AdminDashboard()
        {
            // Implement admin dashboard actions here
            Console.WriteLine("Welcome to the Admin Dashboard.");
        }

        private void UserDashboard()
        {
            // Implement user dashboard actions here
            Console.WriteLine("Welcome to the User Dashboard.");
        }
    }
}
