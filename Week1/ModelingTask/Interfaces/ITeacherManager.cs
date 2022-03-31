using Week1.ModelingTask.Models;

namespace Week1.ModelingTask.Interfaces
{
    public interface ITeacherManager
    {
        void AddCourse(Teacher teacher, Course course);
        void AddLesson(Teacher teacher, Lesson lesson);
        void LessonScoring(Student student, Lesson lesson, int score);
        void CourseScoring(Student student, Course course, int score);
    }
}
