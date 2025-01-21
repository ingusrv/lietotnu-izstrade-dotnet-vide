using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseManagementLib
{
    public class DataManager : IDataManager
    {
        public DataManager()
        {
            Teachers = new List<Teacher>();
            Students = new List<Student>();
            Courses = new List<Course>();
            Assignments = new List<Assignment>();
            Submissions = new List<Submission>();
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

        public bool Save(string path)
        {
            // https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/how-to
            // https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/preserve-references
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            // vieglāk salikt visu vienā klasē priekš serializācijas un deserializācijas
            var Data = new DataRepository();
            Data.Students = Students;
            Data.Teachers = Teachers;
            Data.Courses = Courses;
            Data.Assignments = Assignments;
            Data.Submissions = Submissions;

            string json = JsonSerializer.Serialize<DataRepository>(Data, options);
            File.WriteAllText(path, json);
            return true;
        }

        public bool Load(string path)
        {
            if (!File.Exists(path)) return false;

            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            string json = File.ReadAllText(path);
            try
            {
                DataRepository? deserializedData = JsonSerializer.Deserialize<DataRepository>(json, options);

                // citādāk sūdzās par possible null reference assignment ja nepārbauda
                if (deserializedData == null) return false;

                Students = deserializedData.Students;
                Teachers = deserializedData.Teachers;
                Courses = deserializedData.Courses;
                Assignments = deserializedData.Assignments;
                Submissions = deserializedData.Submissions;
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CreateTestData()
        {
            var teacher = new Teacher();
            teacher.Name = "Elīna";
            teacher.Surname = "Kalniņa";
            teacher.Gender = Gender.Woman;
            teacher.ContractDate = new DateOnly(2020, 9, 28);

            Teachers.Add(teacher);

            var course = new Course();
            course.Teacher = teacher;
            course.Name = "Lietotņu izstrāde .NET vidē";

            Courses.Add(course);

            var assignment = new Assignment();
            assignment.Deadline = DateOnly.FromDateTime(DateTime.Today);
            assignment.Course = course;
            assignment.Description = "Pirmā mājasdarba uzdevums";

            Assignments.Add(assignment);

            var student = new Student("Ingus", "Valheims", Gender.Man, "abc1234");

            Students.Add(student);

            var submission = new Submission();
            submission.Assignment = assignment;
            submission.Student = student;
            submission.SubmissionTime = DateTime.Now;
            submission.Score = 100;

            Submissions.Add(submission);

            return true;
        }

        public bool Reset()
        {
            Teachers.Clear();
            Students.Clear();
            Courses.Clear();
            Assignments.Clear();
            Submissions.Clear();

            return true;
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
            Students.Add(student);
            return true;
        }
        public bool AddTeacher(Teacher teacher)
        {
            Teachers.Add(teacher);
            return true;
        }
        public bool AddCourse(Course course)
        {
            Courses.Add(course);
            return true;
        }
        public bool AddAssignment(Assignment assignment)
        {
            Assignments.Add(assignment);
            return true;
        }
        public bool AddSubmission(Submission submission)
        {
            Submissions.Add(submission);
            return true;
        }

        public bool RemoveStudent(Student student)
        {
            return Students.Remove(student);
        }
        public bool RemoveTeacher(Teacher teacher)
        {
            return Teachers.Remove(teacher);
        }
        public bool RemoveCourse(Course course)
        {
            return Courses.Remove(course);
        }
        public bool RemoveAssignment(Assignment assignment)
        {
            return Assignments.Remove(assignment);
        }
        public bool RemoveSubmission(Submission submission)
        {
            return Submissions.Remove(submission);
        }

        public List<Teacher> Teachers { get; set; }
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<Submission> Submissions { get; set; }
    }

}
