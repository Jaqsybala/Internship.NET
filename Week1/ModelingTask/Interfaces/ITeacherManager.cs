using Week1.ModelingTask.Models;

namespace Week1.ModelingTask.Interfaces
{
    public interface ITeacherManager
    {
        void LessonScoring(Student student, Course course, int score, Lesson lesson);
    }
}
