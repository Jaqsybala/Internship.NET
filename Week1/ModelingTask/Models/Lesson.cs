namespace Week1.ModelingTask.Models
{
    public class Lesson
    {
        public Guid Id { get; set; }
        public string LessonName { get; set; }
        public DateTime Date { get; set; } 
        public Course Course { get; set; }
        public Student Student { get; set; }
        public int Score { get; set; }
    }
}
