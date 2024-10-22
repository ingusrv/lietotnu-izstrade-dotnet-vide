using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace md2
{
    public class DataManager : IDataManager
    {
        public DataRepository Data { get; set; }

        public DataManager()
        {
            Data = new DataRepository();
        }

        public string Print()
        {
            string result = "Teachers:\n";
            foreach (var teacher in Data.Teachers)
            {
                result += teacher.ToString() + "\n";
            }

            result += "\nStudents:\n";
            foreach (var student in Data.Students)
            {
                result += student.ToString() + "\n";
            }

            result += "\nCourses:\n";
            foreach (var course in Data.Courses)
            {
                result += course.ToString() + "\n";
            }

            result += "\nAssignments:\n";
            foreach (var assignment in Data.Assignments)
            {
                result += assignment.ToString() + "\n";
            }

            result += "\nSubmissions:\n";
            foreach (var submission in Data.Submissions)
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

                Data = deserializedData;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void CreateTestData()
        {
            var teacher = new Teacher();
            teacher.Name = "Elīna";
            teacher.Surname = "Kalniņa";
            teacher.Gender = Gender.Woman;
            teacher.ContractDate = new DateOnly(2020, 9, 28);

            Data.Teachers.Add(teacher);

            var course = new Course();
            course.Teacher = teacher;
            course.Name = "Lietotņu izstrāde .NET vidē";

            Data.Courses.Add(course);

            var assignment = new Assignment();
            assignment.Deadline = DateOnly.FromDateTime(DateTime.Today);
            assignment.Course = course;
            assignment.Description = "Pirmā mājasdarba uzdevums";

            Data.Assignments.Add(assignment);

            var student = new Student("Ingus", "Valheims", Gender.Man, "abc1234");

            Data.Students.Add(student);

            var submission = new Submission();
            submission.Assignment = assignment;
            submission.Student = student;
            submission.SubmissionTime = DateTime.Now;
            submission.Score = 100;

            Data.Submissions.Add(submission);
        }

        public void Reset()
        {
            Data.Teachers.Clear();
            Data.Students.Clear();
            Data.Courses.Clear();
            Data.Assignments.Clear();
            Data.Submissions.Clear();
        }
    }

}
