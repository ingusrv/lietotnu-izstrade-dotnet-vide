using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementLib
{
    public class DataRepository
    {
        public List<Teacher> Teachers { get; set; }
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<Submission> Submissions { get; set; }

        public DataRepository()
        {
            Teachers = new List<Teacher>();
            Students = new List<Student>();
            Courses = new List<Course>();
            Assignments = new List<Assignment>();
            Submissions = new List<Submission>();
        }
    }
}
