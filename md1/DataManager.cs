using System.Text.Json;
using System.Text.Json.Serialization;

interface IDataManager
{
    string Print();
    bool Save(string path);
    bool Load(string path);
    void CreateTestData();
    void Reset();
}

public class DataManager : IDataManager
{
    private DataRepository dataRepo;

    public DataManager()
    {
        dataRepo = new DataRepository();
    }

    public string Print()
    {
        string result = "Teachers:\n";
        foreach (var teacher in dataRepo.Teachers)
        {
            result += teacher.ToString() + "\n";
        }

        result += "\nStudents:\n";
        foreach (var student in dataRepo.Students)
        {
            result += student.ToString() + "\n";
        }

        result += "\nCourses:\n";
        foreach (var course in dataRepo.Courses)
        {
            result += course.ToString() + "\n";
        }

        result += "\nAssignments:\n";
        foreach (var assignment in dataRepo.Assignments)
        {
            result += assignment.ToString() + "\n";
        }

        result += "\nSubmissions:\n";
        foreach (var submission in dataRepo.Submissions)
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

        string json = JsonSerializer.Serialize<DataRepository>(dataRepo, options);
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

            dataRepo = deserializedData;
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

        dataRepo.Teachers.Add(teacher);

        var course = new Course();
        course.Teacher = teacher;
        course.Name = "Lietotņu izstrāde .NET vidē";

        dataRepo.Courses.Add(course);

        var assignment = new Assignment();
        assignment.Deadline = DateOnly.FromDateTime(DateTime.Today);
        assignment.Course = course;
        assignment.Description = "Pirmā mājasdarba uzdevums";

        dataRepo.Assignments.Add(assignment);

        var student = new Student("Ingus", "Valheims", Gender.Man, "abc1234");

        dataRepo.Students.Add(student);

        var submission = new Submission();
        submission.Assignment = assignment;
        submission.Student = student;
        submission.SubmissionTime = DateTime.Now;
        submission.Score = 100;

        dataRepo.Submissions.Add(submission);
    }

    public void Reset()
    {
        dataRepo.Teachers.Clear();
        dataRepo.Students.Clear();
        dataRepo.Courses.Clear();
        dataRepo.Assignments.Clear();
        dataRepo.Submissions.Clear();
    }
}
