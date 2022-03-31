namespace Week1.ModelingTask.Models
{
    public class Lesson
    {
        public Guid Id { get; set; }
        public string LessonName { get; set; }
        public Course Course { get; set; }
    }
}
