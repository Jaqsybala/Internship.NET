using System;
using System.Collections.Generic;
using Week1.ModelingTask.Managers;
using Week1.ModelingTask.Models;
using Xunit;

namespace UnitTests.Week1.ModelingTask.Tests
{
    public class StudentTests
    {
        [Theory]
        [InlineData(75, 77)]
        [InlineData(55, 57)]
        [InlineData(60, 62)]
        [InlineData(90, 92)]
        public void GetFinalScoreByCourse_Returns_Final_Score(int score, int expected)
        {
            StudentManager sm = new StudentManager();
            LessonManager lm = new LessonManager();
            CourseManager cm = new CourseManager();
            TeacherManager tm = new TeacherManager();

            var teacher = new Teacher
            {
                Id = Guid.NewGuid(),
                Name = "Aziz"
            };
            tm.Add(teacher);

            Course math = new Course
            {
                Id = Guid.NewGuid(),
                CourseName = "Math",
                Teacher = teacher,
            };
            cm.Add(math);

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
            for (int i = 0; i < 5; i++)
            {
                Lesson lesson = new Lesson
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now.AddDays(i),
                    LessonName = "Lesson" + i,
                    Course = math,
                    Student = arlanStudent,
                    Score = score + i
                };
                lm.Add(lesson);
            }

            int final = sm.GetFinalScoreByCourse(arlanStudent, math);

            Assert.Equal(expected, final);
        }
    }
}
