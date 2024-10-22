using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace md2
{
    public class Course
    {
        public string Name { get; set; }
        public Teacher Teacher { get; set; }

        public override string ToString()
        {
            // pieņemu, ka teacher informācijai jāizmanto iepriekš pārdefinēto ToString
            return base.ToString() + " - Name: " + Name + ", Teacher: " + Teacher.ToString();
        }
    }
}
