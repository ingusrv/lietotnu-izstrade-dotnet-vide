using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CourseManagementLib 
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Teacher Teacher { get; set; }

        public override string ToString()
        {
            // pieņemu, ka teacher informācijai jāizmanto iepriekš pārdefinēto ToString
            return base.ToString() + " - Name: " + Name + ", Teacher: " + Teacher.ToString();
        }
    }
}
