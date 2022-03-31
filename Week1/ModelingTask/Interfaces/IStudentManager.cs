using Week1.ModelingTask.Models;

namespace Week1.ModelingTask.Interfaces
{
    public interface IStudentManager
    {
        public int GetFinalScoreByCourse(Student student, Course course);
    }
}
