namespace TaskManagementApp.Models {
     public enum TaskStatus {
        Pending,
        InProgress,
        Completed
    }

    public class Tasker{
        public int Id { get; set; }
        public string title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public Project Project { get; set; } 

        public int AssignedUserId { get; set; }
        public User AssignedUser { get; set; } 
    }
}