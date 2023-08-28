using System.Linq;
using TaskManagementApp.Data;
using TaskManagementApp.Models;

namespace TaskManagementApp.Services {
    public class ProjectService {
        private readonly ApplicationDbContext _context;
        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Project GetProjectById(int projectId){
            return _context.Projects.FirstOrDefault(p => p.Id == projectId);
        }
        public void CreateProject(Project project){
            _context.Projects.Add(project);
            _context.SaveChanges();
        }
        public void UpdateProject(Project project){
            _context.Projects.Update(project);
            _context.SaveChanges();
        }
        public void DeleteProject(Project project){
            _context.Projects.Remove(project);
            _context.SaveChanges();
        }
    }
}