using Week1.ModelingTask.Models;

namespace Week1.ModelingTask.Context
{
    public class TrainingCenterContext
    {

        private static TrainingCenterContext _instance;

        private TrainingCenterContext()
        {
            Teachers = new List<Teacher>();
            Students = new List<Student>(); 
            Lessons = new List<Lesson>();
            Courses = new List<Course>();
        }

        public static TrainingCenterContext GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TrainingCenterContext();
            }
            return _instance;
        }

        public List<Teacher> Teachers { get; set; }

        public List<Student> Students { get; set; }

        public List<Lesson> Lessons { get; set; }

        public List<Course> Courses { get; set; }

    }
}
