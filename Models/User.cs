namespace TaskManagementApp.Models {
      public enum UserRole
    {
        Admin,
        User
    }
    public class User {
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public UserRole Role { get; set; }

        public ICollection<Tasker> Tasks { get; set; }
    }
}