using Week1.ModelingTask.Context;
using Week1.ModelingTask.Models;
using Week1.ModelingTask.Repository;

namespace Week1.ModelingTask.Managers
{
    public class CourseManager : IRepository<Course>
    {
        private readonly TrainingCenterContext _context;

        public CourseManager()
        { 
            _context = TrainingCenterContext.GetInstance();
        }

        public void Add(Course entity)
        {
            _context.Courses.Add(entity);
        }

        public void Delete(Course entity)
        {
            _context.Courses.Remove(entity);
        }

        public IEnumerable<Course> GetAll()
        {
            return _context.Courses;
        }

        public Course GetByID(Guid id)
        {
            return _context.Courses.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Course entity)
        {
            foreach (var course in _context.Courses)
            {
                if (course.Id == entity.Id)
                {
                    course.Teacher = entity.Teacher;
                    course.Lessons = entity.Lessons;
                    course.CourseName = entity.CourseName;
                }
            }
        }
    }
}
