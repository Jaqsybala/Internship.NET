using Week1.ModelingTask.DBContext;
using Week1.ModelingTask.Models;

namespace Week1.ModelingTask.Repository
{
    public class TeacherRepository : IRepository<Teacher>
    {
        private readonly TeacherContext _teacherContext;

        public TeacherRepository()
        { 
            _teacherContext = new TeacherContext();
        }

        public void Add(Teacher entity)
        {
            _teacherContext.Teachers.Add(entity);
        }

        public void Delete(Teacher entity)
        {
            _teacherContext.Teachers.Remove(entity);
        }

        public IEnumerable<Teacher> GetAll()
        {
            return _teacherContext.Teachers;
        }

        public Teacher GetByID(int id)
        {
            return _teacherContext.Teachers[id];
        }

        public void Update(Teacher entity)
        {
            throw new NotImplementedException();
        }
    }
}
