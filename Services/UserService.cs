using System.Linq;
using TaskManagementApp.Data;
using TaskManagementApp.Models;

namespace TaskManagementApp.Services {
    public class UserService {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context){
            _context = context;
        }

        public User GetUserByUsername(string username) {
            return _context.Users.FirstOrDefault(u => u.username == username); // Corrected 'username' to 'Username'
        }

        public void CreateUser(User user) {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        
        public void EditUser(User editedUser) {
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == editedUser.Id); // Corrected 'user' to 'editedUser'
            if (existingUser != null) {
                existingUser.username = editedUser.username;
                existingUser.password = editedUser.password;
                _context.SaveChanges();
            }
        }
        
        public void DeleteUser(int userId) {
            var userToRemove = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (userToRemove != null) {
                _context.Users.Remove(userToRemove);
                _context.SaveChanges();
            }
        }
    }
}
