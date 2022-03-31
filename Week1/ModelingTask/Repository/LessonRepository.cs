using Week1.ModelingTask.DBContext;
using Week1.ModelingTask.Models;

namespace Week1.ModelingTask.Repository
{
    public class LessonRepository : IRepository<Lesson>
    {
        private readonly LessonContext _lessonContext;

        public LessonRepository()
        { 
            _lessonContext = new LessonContext();
        }
        
        public void Add(Lesson entity)
        {
            _lessonContext.Lessons.Add(entity);
        }

        public void Delete(Lesson entity)
        {
            _lessonContext.Lessons.Remove(entity);
        }

        public IEnumerable<Lesson> GetAll()
        {
            return _lessonContext.Lessons;
        }

        public Lesson GetByID(int id)
        {
            return _lessonContext.Lessons[id];
        }

        public void Update(Lesson entity)
        {
            throw new NotImplementedException();
        }
    }
}
