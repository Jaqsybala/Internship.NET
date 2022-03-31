namespace Week1.ModelingTask.Models
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Course> Courses { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}
