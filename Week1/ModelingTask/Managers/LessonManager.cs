using Week1.ModelingTask.Context;
using Week1.ModelingTask.Models;
using Week1.ModelingTask.Repository;

namespace Week1.ModelingTask.Managers
{
    public class LessonManager : IRepository<Lesson>
    {
        private readonly TrainingCenterContext _context;

        public LessonManager()
        {
            _context = TrainingCenterContext.GetInstance();
        }

        public void Add(Lesson entity)
        {
            _context.Lessons.Add(entity);
        }

        public void Delete(Lesson entity)
        {
            _context.Lessons.Add(entity);
        }

        public IEnumerable<Lesson> GetAll()
        {
            return _context.Lessons;
        }

        public Lesson GetByID(Guid id)
        {
            return _context.Lessons.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Lesson entity)
        {
            foreach (var lesson in _context.Lessons)
            {
                if (lesson.Id == entity.Id)
                { 
                    lesson.LessonName = entity.LessonName;
                    lesson.Student = entity.Student;
                    lesson.Score = entity.Score;
                    lesson.Date = DateTime.Now;
                }
            }
        }
    }
}
