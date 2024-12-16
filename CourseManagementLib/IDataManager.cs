using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementLib
{
    public interface IDataManager
    {
        string Print();
        bool Save(string path = "./");
        bool Load(string path);
        bool CreateTestData();
        bool Reset();

        List<Student> GetAllStudents();
        List<Teacher> GetAllTeachers();
        List<Course> GetAllCourses();
        List<Assignment> GetAllAssignments();
        List<Submission> GetAllSubmissions();
        
        bool AddStudent(Student student);
        bool AddTeacher(Teacher teacher);
        bool AddCourse(Course course);
        bool AddAssignment(Assignment assignment);
        bool AddSubmission(Submission submission);

        bool RemoveStudent(Student student);
        bool RemoveTeacher(Teacher teacher);
        bool RemoveCourse(Course course);
        bool RemoveAssignment(Assignment assignment);
        bool RemoveSubmission(Submission submission);
    }
}
