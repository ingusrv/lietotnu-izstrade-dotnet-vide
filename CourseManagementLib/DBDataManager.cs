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
    public class DBDataManager : IDataManager
    {

        private readonly DataManagerContext _context;

        public DBDataManager() {
            // tā kā connection string ir MAUI projektā no kura nevar veikt migrācijas, šim projektam arī vajag config failu
            // lai kaut kādā veidā tiktu pie connection string
            var connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            var options = new DbContextOptionsBuilder<DataManagerContext>().UseSqlServer(connectionString).Options;
            _context = new DataManagerContext(options);
        }
        public DBDataManager(string connectionString) {
            var options = new DbContextOptionsBuilder<DataManagerContext>().UseSqlServer(connectionString).Options;
            _context = new DataManagerContext(options);
        }
        public DBDataManager(DataManagerContext context)
        {
            this._context = context;
        }

        public string Print() 
        {
            try
            {
                string result = "Teachers:\n";
                foreach (var teacher in _context.Teachers)
                {
                    result += teacher.ToString() + "\n";
                }

                result += "\nStudents:\n";
                foreach (var student in _context.Students)
                {
                    result += student.ToString() + "\n";
                }

                result += "\nCourses:\n";
                foreach (var course in _context.Courses)
                {
                    result += course.ToString() + "\n";
                }

                result += "\nAssignments:\n";
                foreach (var assignment in _context.Assignments)
                {
                    result += assignment.ToString() + "\n";
                }

                result += "\nSubmissions:\n";
                foreach (var submission in _context.Submissions)
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
                _context.SaveChanges();
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

                _context.Teachers.Add(teacher);
                _context.Teachers.Add(teacher2);

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

                _context.Courses.Add(course);
                _context.Courses.Add(course2);

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

                _context.Assignments.Add(assignment);
                _context.Assignments.Add(assignment2);

                var student = new Student("Ingus", "Valheims", Gender.Man, "abc1234");
                var student2 = new Student("Elīza", "Avotiņa", Gender.Woman, "def4321");

                _context.Students.Add(student);
                _context.Students.Add(student2);

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

                _context.Submissions.Add(submission);
                _context.Submissions.Add(submission2);

                _context.SaveChanges();

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
                _context.Submissions.ExecuteDelete();
                _context.Assignments.ExecuteDelete();
                _context.Courses.ExecuteDelete();
                _context.Teachers.ExecuteDelete();
                _context.Students.ExecuteDelete();

                return true;
            } catch
            {
                return false;
            }
        }

        public List<Student> GetAllStudents()
        {
            return _context.Students.ToList();
        }
        public List<Teacher> GetAllTeachers()
        {
            return _context.Teachers.ToList();
        }
        public List<Course> GetAllCourses()
        {
            return _context.Courses.ToList();
        }
        public List<Assignment> GetAllAssignments()
        {
            return _context.Assignments.ToList();
        }
        public List<Submission> GetAllSubmissions()
        {
            return _context.Submissions.ToList();
        }

        public bool AddStudent(Student student)
        {
            try
            {
                _context.Students.Add(student);
                _context.SaveChanges();
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
                _context.Teachers.Add(teacher);
                _context.SaveChanges();
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
                _context.Courses.Add(course);
                _context.SaveChanges();
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
                _context.Assignments.Add(assignment);
                _context.SaveChanges();
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
                _context.Submissions.Add(submission);
                _context.SaveChanges();
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
                _context.Students.Remove(student);
                _context.SaveChanges();
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
                _context.Teachers.Remove(teacher);
                _context.SaveChanges();
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
                _context.Courses.Remove(course);
                _context.SaveChanges();
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
                _context.Assignments.Remove(assignment);
                _context.SaveChanges();
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
                _context.Submissions.Remove(submission);
                _context.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }
    }
}
