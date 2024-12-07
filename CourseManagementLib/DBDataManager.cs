using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementLib
{
    public class DBDataManager : DbContext, IDataManager
    {
        private readonly string _connectionString;

        public DBDataManager() {
            // tā kā connection string ir MAUI projektā no kura nevar veikt migrācijas, šim projektam arī vajag config failu
            // lai kaut kādā veidā tiktu pie connection string
           _connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        }

        public DBDataManager(DbContextOptions<DBDataManager> options) : base(options) { }

        public DBDataManager(string connectionString) {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public string Print() 
        { 
            string result = "Teachers:\n";
            foreach (var teacher in Teachers)
            {
                result += teacher.ToString() + "\n";
            }

            result += "\nStudents:\n";
            foreach (var student in Students)
            {
                result += student.ToString() + "\n";
            }

            result += "\nCourses:\n";
            foreach (var course in Courses)
            {
                result += course.ToString() + "\n";
            }

            result += "\nAssignments:\n";
            foreach (var assignment in Assignments)
            {
                result += assignment.ToString() + "\n";
            }

            result += "\nSubmissions:\n";
            foreach (var submission in Submissions)
            {
                result += submission.ToString() + "\n";
            }

            return result;
        }
        public bool Load(string path)
        {
            return false;
        }
        public bool Save(string path) 
        { 
            return false;
        }
        public bool CreateTestData()
        {
            try
            {
                var teacher = new Teacher
                {
                    Name = "Elīna",
                    Surname = "Kalniņa",
                    Gender = Gender.Woman,
                    ContractDate = new DateOnly(2020, 9, 28)
                };
                var teacher2 = new Teacher
                {
                    Name = "Andris",
                    Surname = "Bērziņš",
                    Gender = Gender.Man,
                    ContractDate = new DateOnly(2023, 12, 7)
                };

                Teachers.Add(teacher);
                Teachers.Add(teacher2);

                var course = new Course
                {
                    Teacher = teacher,
                    Name = "Lietotņu izstrāde .NET vidē"
                };
                var course2 = new Course
                {
                    Teacher = teacher2,
                    Name = "Spēļu izstrāde ar Unity"
                };

                Courses.Add(course);
                Courses.Add(course2);

                var assignment = new Assignment
                {
                    Deadline = DateOnly.FromDateTime(DateTime.Today),
                    Course = course,
                    Description = "Trešā mājasdarba uzdevums"
                };
                var assignment2 = new Assignment
                {
                    Deadline = DateOnly.FromDateTime(DateTime.Today),
                    Course = course2,
                    Description = "Pirmās spēles nodevums"
                };

                Assignments.Add(assignment);
                Assignments.Add(assignment2);

                var student = new Student("Ingus", "Valheims", Gender.Man, "abc1234");
                var student2 = new Student("Elīza", "Avotiņa", Gender.Woman, "def4321");

                Students.Add(student);
                Students.Add(student2);

                var submission = new Submission
                {
                    Assignment = assignment,
                    Student = student,
                    SubmissionTime = DateTime.Now,
                    Score = 100
                };
                var submission2 = new Submission
                {
                    Assignment = assignment2,
                    Student = student2,
                    SubmissionTime = DateTime.Now,
                    Score = 100
                };

                Submissions.Add(submission);
                Submissions.Add(submission2);

                SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Reset() 
        {
            try
            {
                Submissions.ExecuteDelete();
                Assignments.ExecuteDelete();
                Courses.ExecuteDelete();
                Teachers.ExecuteDelete();
                Students.ExecuteDelete();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Submission> Submissions { get; set; }
    }
}
