using Week1.ModelingTask.Models;

namespace Week1.ModelingTask.Interfaces
{
    public interface IStudentManager
    {
        int GetLessonScore(Student student, Lesson lesson);
        int GetCourseScore(Student student, Course course);
    }
}
