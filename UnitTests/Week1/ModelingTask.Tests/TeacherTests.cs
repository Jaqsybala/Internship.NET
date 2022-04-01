using Week1.ModelingTask.Models;
using Xunit;
using System;
using System.Collections.Generic;
using Week1.ModelingTask.Managers;

namespace UnitTests.Week1.ModelingTask.Tests
{
    public class TeacherTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(50, 50)]
        [InlineData(75, 75)]
        [InlineData(100, 100)]
        public void LessonScoring_Adds_Score_For_Student(int score, int expected)
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

            Lesson lesson = new Lesson
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                LessonName = "Matrix multiplication",
                Course = math,
                Student = arlanStudent,
            };
            lm.Add(lesson);

            tm.LessonScoring(arlanStudent, math, score, lesson);

            Assert.Equal(expected, lesson.Score);
            
        }
    }
}
