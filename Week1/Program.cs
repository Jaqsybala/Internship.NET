using Week1.ModelingTask.Managers;
using Week1.ModelingTask.Models;

namespace Week1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("IT WORKS");

            StudentManager sm = new StudentManager();
            LessonManager lm = new LessonManager();
            CourseManager cm = new CourseManager();
            TeacherManager tm = new TeacherManager();


            Teacher teacher = new Teacher
            {
                Id = Guid.NewGuid(),
                Name = "Aziz teacher"
            };
            tm.Add(teacher);

            Course math = new Course
            {
                Id = Guid.NewGuid(),
                CourseName = "Math",
                Teacher = teacher,
            };
            cm.Add(math);

            Teacher teacher2 = new Teacher
            {
                Id = Guid.NewGuid(),
                Name = "Ruslan teacher"
            };
            tm.Add(teacher2);

            Course programming = new Course
            {
                Id = Guid.NewGuid(),
                CourseName = "Programming",
                Teacher = teacher2,
            };
            cm.Add(programming);

            Student arlanStudent = new Student
            {
                Id = Guid.NewGuid(),
                Name = "Arlan student",
                Courses = new List<Course>
                {
                    math
                }
            };
            sm.Add(arlanStudent);

            Student blerStudent = new Student
            {
                Id = Guid.NewGuid(),
                Name = "Bler student",
                Courses = new List<Course>
                {
                    math, programming
                }
            };
            sm.Add(blerStudent);


            Random random = new Random();

            for (int i = 1; i <= 10; i++)
            {
                Lesson randomLesson = new Lesson
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now.AddDays(i),
                    LessonName = "Lesson " + (i),
                    Course = math,
                    Student = arlanStudent,
                    Score = random.Next(1,5)
                };
                lm.Add(randomLesson);
            }

            Console.WriteLine(sm.GetFinalScoreByCourse(arlanStudent, math));



        }
    }
}
