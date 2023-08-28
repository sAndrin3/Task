using System.Linq;
using TaskManagementApp.Models;
using TaskManagementApp.Data;

namespace TaskManagementApp.Services {
    public class TaskService {
        private readonly ApplicationDbContext _context;
        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Tasker? GetTaskById(int taskId){
            return _context.Tasks.FirstOrDefault(t => t.Id == taskId);
        }
        public void MarkTaskAsCompleted(Tasker task){
            task.IsCompleted = true;
            _context.SaveChanges();
        }
        public void MarkTaskAsInProgress(Tasker task){
            task.TaskStatus = Models.TaskStatus.InProgress; // Fully qualify the enum value
            _context.SaveChanges();
        }
        public void MarkTaskAsPending(Tasker task){
            task.TaskStatus = Models.TaskStatus.Pending; // Fully qualify the enum value
            _context.SaveChanges();
        }
    }
}
