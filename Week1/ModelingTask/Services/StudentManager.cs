using Week1.ModelingTask.Interfaces;
using Week1.ModelingTask.Models;

namespace Week1.ModelingTask.Service
{
    public class StudentManager : IStudentManager
    {
        public int GetCourseScore(Student student, Course course)
        {
            return student.CourseScore[course];
        }

        public int GetLessonScore(Student student, Lesson lesson)
        {
            return student.LessonScore[lesson];
        }
    }
}
