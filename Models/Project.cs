namespace TaskManagementApp.Models {
    public class Project {
        public int Id {get; set; }
        public string Name {get; set; } = string.Empty;
        public bool IsSelected { get; set; }

        public ICollection<Tasker>? Tasks {get; set; }
    }
}