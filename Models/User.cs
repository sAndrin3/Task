namespace TaskManagementApp.Models {
    public class User {
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public ICollection<Tasker> Tasks { get; set; }
    }
}