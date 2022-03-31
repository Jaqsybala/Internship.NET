namespace Week1.ModelingTask.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Course> Courses { get; set; }
        public List<Lesson> Lessons { get; set; }
        public Dictionary<Course, int> CourseScore { get; set; }
        public Dictionary<Lesson, int> LessonScore { get; set; }
    }
}
