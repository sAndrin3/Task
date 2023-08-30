namespace TaskManagementApp.Models {
      public enum UserRole
    {
        Admin,
        User
    }
    public class User {
        public int Id { get; set; }
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public UserRole Role { get; set; }

        public ICollection<Tasker>? Tasks { get; set; }
    }
}