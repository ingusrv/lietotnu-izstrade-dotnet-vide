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

        public DBDataManager(string connectionString) {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public string Print() 
        {
            try
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

            } catch
            {
                return "";
            }
        }
        public bool Load(string path)
        {
            return false;
        }
        public bool Save(string path = "./") 
        { 
            try
            {
                SaveChanges();
                return true;
            } catch
            { 
                return false;
            }
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
            } catch
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
            } catch
            {
                return false;
            }
        }

        public List<Student> GetAllStudents()
        {
            return Students.ToList();
        }
        public List<Teacher> GetAllTeachers()
        {
            return Teachers.ToList();
        }
        public List<Course> GetAllCourses()
        {
            return Courses.ToList();
        }
        public List<Assignment> GetAllAssignments()
        {
            return Assignments.ToList();
        }
        public List<Submission> GetAllSubmissions()
        {
            return Submissions.ToList();
        }

        public bool AddStudent(Student student)
        {
            try
            {
                Students.Add(student);
                SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }
        public bool AddTeacher(Teacher teacher)
        {
            try
            {
                Teachers.Add(teacher);
                SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }
        public bool AddCourse(Course course)
        {
            try
            {
                Courses.Add(course);
                SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }
        public bool AddAssignment(Assignment assignment)
        {
            try
            {
                Assignments.Add(assignment);
                SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }
        public bool AddSubmission(Submission submission)
        {
            try
            {
                Submissions.Add(submission);
                SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }

        public bool RemoveStudent(Student student)
        {
            try
            {
                Students.Remove(student);
                SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }
        public bool RemoveTeacher(Teacher teacher)
        {
            try
            {
                Teachers.Remove(teacher);
                SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }
        public bool RemoveCourse(Course course)
        {
            try
            {
                Courses.Remove(course);
                SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }
        public bool RemoveAssignment(Assignment assignment)
        {
            try
            {
                Assignments.Remove(assignment);
                SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }
        public bool RemoveSubmission(Submission submission)
        {
            try
            {
                Submissions.Remove(submission);
                SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }

        private DbSet<Student> Students { get; set; }
        private DbSet<Teacher> Teachers { get; set; }
        private DbSet<Course> Courses { get; set; }
        private DbSet<Assignment> Assignments { get; set; }
        private DbSet<Submission> Submissions { get; set; }
    }
}
