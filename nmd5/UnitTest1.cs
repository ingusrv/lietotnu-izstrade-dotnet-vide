using CourseManagementLib;
using Microsoft.EntityFrameworkCore;
using Moq;

// �ie avoti pal�dz�ja
// https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
// https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices

namespace nmd5
{
    public class UnitTest1
    {
        [Fact]
        public void AddStudent_SingleStudent()
        {
            var options = new DbContextOptionsBuilder<DataManagerContext>().UseInMemoryDatabase("TestDatabase").Options;
            var context = new DataManagerContext(options);
            var manager = new DBDataManager(context);
            var student = new Student { Id = 1, Name = "StudentName", Surname = "StudentSurname" };

            var result = manager.AddStudent(student);

            Assert.True(result);
            Assert.Single(context.Students.ToList());
            Assert.Contains(context.Students.ToList(), student => student.FullName == "StudentName StudentSurname");
        }

        [Fact]
        public void AddTeacher_SingleTeacher()
        {
            var options = new DbContextOptionsBuilder<DataManagerContext>().UseInMemoryDatabase("TestDatabase").Options;
            var context = new DataManagerContext(options);
            var manager = new DBDataManager(context);
            var teacher = new Teacher { Id = 1, Name = "TeacherName", Surname = "TeacherSurname" };

            var result = manager.AddTeacher(teacher);

            Assert.True(result);
            Assert.Single(context.Teachers.ToList());
            Assert.Contains(context.Teachers.ToList(), teacher => teacher.FullName == "TeacherName TeacherSurname");
        }

        [Fact]
        public void AddCourse_SingleCourse()
        {
            var options = new DbContextOptionsBuilder<DataManagerContext>().UseInMemoryDatabase("TestDatabase").Options;
            var context = new DataManagerContext(options);
            var manager = new DBDataManager(context);
            var teacher = new Teacher { Id = 1, Name = "TeacherName", Surname = "TeacherSurname" };
            var course = new Course { Id = 1, Name = "Test Course", Teacher = teacher }; 

            var result = manager.AddCourse(course);

            Assert.True(result);
            Assert.Single(context.Courses.ToList());
            Assert.Contains(context.Courses.ToList(), course => course.Name == "Test Course");
        }

        [Fact]
        public void AddAssignment_SingleAssignment()
        {
            var options = new DbContextOptionsBuilder<DataManagerContext>().UseInMemoryDatabase("TestDatabase").Options;
            var context = new DataManagerContext(options);
            var manager = new DBDataManager(context);
            var assignment = new Assignment { Id = 1, Deadline = DateOnly.FromDateTime(DateTime.Now), Description = "Test Assignment" };

            var result = manager.AddAssignment(assignment);

            Assert.True(result);
            Assert.Single(context.Assignments.ToList());
            Assert.Contains(context.Assignments.ToList(), assignment => assignment.Description == "Test Assignment");
        }

        [Fact]
        public void AddSubmission_SingleSubmission()
        {
            var options = new DbContextOptionsBuilder<DataManagerContext>().UseInMemoryDatabase("TestDatabase").Options;
            var context = new DataManagerContext(options);
            var manager = new DBDataManager(context);
            var date = DateTime.Now;
            var submission = new Submission { Id = 1, SubmissionTime = date, Score = 100 };

            var result = manager.AddSubmission(submission);

            Assert.True(result);
            Assert.Single(context.Submissions.ToList());
            Assert.Contains(context.Submissions.ToList(), submission => submission.Score == 100 && submission.SubmissionTime == date);
        }

        [Fact]
        public void Print_NoData_ReturnsOnlyTemplateString()
        {
            var options = new DbContextOptionsBuilder<DataManagerContext>().UseInMemoryDatabase("TestDatabase").Options;
            var context = new DataManagerContext(options);
            var manager = new DBDataManager(context);

            var result = manager.Print();

            Assert.Equal(String.Concat(
                "Teachers:\n",
                "\nStudents:\n",
                "\nCourses:\n",
                "\nAssignments:\n",
                "\nSubmissions:\n"
            ), result);
        }

        [Fact]
        public void Print_AllData_ReturnsStringWithData()
        {
            var options = new DbContextOptionsBuilder<DataManagerContext>().UseInMemoryDatabase("TestDatabase").Options;
            var context = new DataManagerContext(options);

            var date = DateTime.Now;
            var student = new Student { Name = "StudentName", Surname = "StudentSurname" };
            var teacher = new Teacher { Name = "TeacherName", Surname = "TeacherSurname" };
            var course = new Course { Name = "Test Course", Teacher = teacher };
            var assignment = new Assignment { Course = course, Deadline = DateOnly.FromDateTime(date), Description = "Test Assignment" };
            var submission = new Submission { Assignment = assignment, Student = student, SubmissionTime = date, Score = 100 };
            context.Students.Add(student);
            context.Teachers.Add(teacher);
            context.Courses.Add(course);
            context.Assignments.Add(assignment);
            context.Submissions.Add(submission);
            context.SaveChanges();

            var manager = new DBDataManager(context);

            var result = manager.Print();

            Assert.Single(manager.GetAllStudents());
            Assert.Equal(String.Concat(
                "Teachers:\n",
                "CourseManagementLib.Teacher - Full Name: TeacherName TeacherSurname, Gender: Man, Contract Date: 01.01.0001\n",
                "\nStudents:\n",
                "CourseManagementLib.Student - Full Name: StudentName StudentSurname, Gender: Man, Student Id Number: \n",
                "\nCourses:\n",
                "CourseManagementLib.Course - Name: Test Course, Teacher: CourseManagementLib.Teacher - Full Name: TeacherName TeacherSurname, Gender: Man, Contract Date: 01.01.0001\n",
                "\nAssignments:\n",
                $"CourseManagementLib.Assignment - Description: Test Assignment, Deadline: {DateOnly.FromDateTime(date)}, Course: CourseManagementLib.Course - Name: Test Course, Teacher: CourseManagementLib.Teacher - Full Name: TeacherName TeacherSurname, Gender: Man, Contract Date: 01.01.0001\n",
                "\nSubmissions:\n",
                $"CourseManagementLib.Submission - Student Name: StudentName StudentSurname, Submission Time: {date}, Score: 100\n"
            ), result);
        }

        // pietrūka laika pielikt vēl testus

        //[Fact]
        //public void Reset_WithData_ReturnsTrue()
        //{
        //    var options = new DbContextOptionsBuilder<DataManagerContext>().UseInMemoryDatabase("TestDatabase").Options;
        //    var context = new DataManagerContext(options);

        //    var date = DateTime.Now;
        //    var student = new Student { Name = "StudentName", Surname = "StudentSurname" };
        //    var teacher = new Teacher { Name = "TeacherName", Surname = "TeacherSurname" };
        //    var course = new Course { Name = "Test Course", Teacher = teacher };
        //    var assignment = new Assignment { Course = course, Deadline = DateOnly.FromDateTime(date), Description = "Test Assignment" };
        //    var submission = new Submission { Assignment = assignment, Student = student, SubmissionTime = date, Score = 100 };
        //    context.Students.Add(student);
        //    context.Teachers.Add(teacher);
        //    context.Courses.Add(course);
        //    context.Assignments.Add(assignment);
        //    context.Submissions.Add(submission);
        //    context.SaveChanges();

        //    var manager = new DBDataManager(context);

        //    var result = manager.Reset();

        //    Assert.True(result);
        //    Assert.Empty(context.Students);
        //    Assert.Empty(context.Teachers);
        //    Assert.Empty(context.Courses);
        //    Assert.Empty(context.Assignments);
        //    Assert.Empty(context.Submissions);
        //}
    }
}