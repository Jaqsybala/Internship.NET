using Week1.ModelingTask.Context;
using Week1.ModelingTask.Interfaces;
using Week1.ModelingTask.Models;
using Week1.ModelingTask.Repository;

namespace Week1.ModelingTask.Managers
{
    public class TeacherManager : IRepository<Teacher>, ITeacherManager
    {
        private readonly TrainingCenterContext _context;

        public TeacherManager()
        { 
            _context = TrainingCenterContext.GetInstance();
        }

        public void Add(Teacher entity)
        {
            _context.Teachers.Add(entity);
        }

        public void Delete(Teacher entity)
        {
            _context.Teachers.Remove(entity);
        }

        public IEnumerable<Teacher> GetAll()
        {
            return _context.Teachers;
        }

        public Teacher GetByID(Guid id)
        {
            return _context.Teachers.FirstOrDefault(x => x.Id == id);
        }

        public void LessonScoring(Student student, Course course, int score, Lesson lesson)
        {
            if (score < 0 || score > 100) throw new ArgumentException("Score must be in range 0 and 100");
            var lessons = _context.Lessons.Where(x => x.Course.Id == course.Id && x.Student.Id == student.Id);
            foreach (var less in lessons.ToList())
            {
                if (less.Id == lesson.Id)
                { 
                    lesson.Score = score;
                }
            }
        }

        public void Update(Teacher entity)
        {
            foreach (var teacher in _context.Teachers)
            {
                if (teacher.Id == entity.Id)
                { 
                    teacher.Name = entity.Name;
                }
            }
        }
    }
}
