using Week1.ModelingTask.DBContext;
using Week1.ModelingTask.Models;

namespace Week1.ModelingTask.Repository
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly StudentContext _studentContext;

        public StudentRepository()
        { 
            _studentContext = new StudentContext();
        }

        public void Add(Student entity)
        {
            _studentContext.Students.Add(entity);
        }

        public void Delete(Student entity)
        {
            _studentContext.Students.Remove(entity);
        }

        public IEnumerable<Student> GetAll()
        {
            return _studentContext.Students;
        }

        public Student GetByID(int id)
        {
            return _studentContext.Students[id];
        }

        public void Update(Student entity)
        {
            throw new NotImplementedException();
        }
    }
}
