using Week1.ModelingTask.Context;
using Week1.ModelingTask.Interfaces;
using Week1.ModelingTask.Models;
using Week1.ModelingTask.Repository;

namespace Week1.ModelingTask.Managers
{
    public class StudentManager : IRepository<Student>, IStudentManager
    {
        private readonly TrainingCenterContext _context;

        public StudentManager()
        {
            _context = TrainingCenterContext.GetInstance();
        }

        public void Add(Student entity)
        {
            _context.Students.Add(entity);
        }

        public void Delete(Student entity)
        {
            _context.Students.Remove(entity);
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students;
        }

        public Student GetByID(Guid id)
        {
            return _context.Students.FirstOrDefault(x => x.Id == id);
        }

        public int GetFinalScoreByCourse(Student student, Course course)
        {
            var lessons = _context.Lessons.Where(x => x.Course.Id == course.Id && x.Student.Id == student.Id);

            int totalScore = lessons.Sum(x => x.Score);
            int countLessons = lessons.Count();

            return (int)totalScore / countLessons;
        }

        public void Update(Student entity)
        {
            foreach(var student in _context.Students)
            {
                if (student.Id == entity.Id)
                {
                    student.Name = entity.Name;
                }
            }
        }
    }
}
