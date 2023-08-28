using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManagementApp.Models;

namespace TaskManagementApp.Data {

    public class ApplicationDbContext : DbContext {

          protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder){
            optionBuilder.UseSqlServer("Server=localhost; Database=TaskManagement; User id=SA; Password=S@ndrine1!; Encrypt=True; TrustServerCertificate=True");
        }
        public DbSet<User> Users {get; set; }
        public DbSet<Tasker> Tasks {get; set; }
        public DbSet<Project> Projects {get; set; }
    }
}