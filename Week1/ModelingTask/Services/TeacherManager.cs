using Week1.ModelingTask.Interfaces;
using Week1.ModelingTask.Models;

namespace Week1.ModelingTask.Service
{
    public class TeacherManager : ITeacherManager
    {
        public void AddCourse(Teacher teacher, Course course)
        {
            teacher.Courses.Add(course);
        }

        public void AddLesson(Teacher teacher, Lesson lesson)
        {
            teacher.Lessons.Add(lesson);
        }

        public void CourseScoring(Student student, Course course, int score)
        {
            student.CourseScore.Add(course, score);
        }

        public void LessonScoring(Student student, Lesson lesson, int score)
        {
            student.LessonScore.Add(lesson, score);
        }
    }
}
