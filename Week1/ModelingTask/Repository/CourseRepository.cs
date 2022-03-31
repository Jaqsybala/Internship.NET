using Week1.ModelingTask.DBContext;
using Week1.ModelingTask.Models;

namespace Week1.ModelingTask.Repository
{
    public class CourseRepository : IRepository<Course>
    {

        private readonly CourseContext _courseContext;

        public CourseRepository()
        { 
            _courseContext = new CourseContext();
        }

        public void Add(Course entity)
        {
            _courseContext.Courses.Add(entity);
        }

        public void Delete(Course entity)
        {
            _courseContext.Courses.Remove(entity);
        }

        public IEnumerable<Course> GetAll()
        {
            return _courseContext.Courses;
        }

        public Course GetByID(int id)
        {
            return _courseContext.Courses[id];
        }

        public void Update(Course entity)
        {
            throw new NotImplementedException();
        }
    }
}
