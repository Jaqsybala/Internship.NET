// See https://aka.ms/new-console-template for more information
using Week1.ModelingTask.Models;
using Week1.ModelingTask.Service;

Console.WriteLine("Hello, World!");
TeacherManager tm = new TeacherManager();
var teacher = new Teacher
{
    Id = Guid.NewGuid(),
    Name = "Aziz"
};

Course math = new Course
{
    Id = Guid.NewGuid(),
    CourseName = "Math",
    Teacher = teacher,
};

Student arlanStudent = new Student
{
    Id = Guid.NewGuid(),
    Name = "Arlan student",
    Courses = new List<Course>
                {
                    math
                }
};

Lesson lesson = new Lesson
{
    Id = Guid.NewGuid(),
    Date = DateTime.Now,
    LessonName = "Matrix multiplication",
    Course = math,
    Student = arlanStudent,
};

tm.LessonScoring(arlanStudent, math, 90, lesson);

Console.WriteLine(lesson.Score);