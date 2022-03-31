namespace Week1.ModelingTask.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string CourseName { get; set; }
        public List<Lesson> Lessons { get; set; }
        public Teacher Teacher { get; set; }
    }
}
