namespace ToDoManagerAPI.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(1);
    }
}
