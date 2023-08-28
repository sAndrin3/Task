// using System;
// using Microsoft.EntityFrameworkCore;
// using TaskManagementApp.Data;
// using TaskManagementApp.Models;
// using TaskManagementApp.Services;

// namespace TaskManagementApp.ConsoleApp
// {
//     public class ProjectController
//     {
//         private readonly ProjectService _projectService;

//         public ProjectController()
//         {
//             var dbContext = new ApplicationDbContext();
//             _projectService = new ProjectService(dbContext);
//         }

//         public void Run()
//         {
//             // Create a new project
//             var newProject = new Project
//             {
//                 Name = "Sample Project"
//             };
//             _projectService.CreateProject(newProject);
//             Console.WriteLine("New project created!");

//             // Get project by ID
//             var projectId = newProject.Id;
//             var retrievedProject = _projectService.GetProjectById(projectId);
//             if (retrievedProject != null)
//             {
//                 Console.WriteLine($"Retrieved Project: {retrievedProject.Name}");
//             }

//             // Update project
//             if (retrievedProject != null)
//             {
//                 retrievedProject.Name = "Updated Project";
//                 _projectService.UpdateProject(retrievedProject);
//                 Console.WriteLine("Project updated!");
//             }

//             // Delete project
//             // if (retrievedProject != null)
//             // {
//             //     _projectService.DeleteProject(retrievedProject);
//             //     Console.WriteLine("Project deleted!");
//             // }
//         }
//     }
// }
