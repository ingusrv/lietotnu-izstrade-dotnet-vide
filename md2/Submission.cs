using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace md2
{
    public class Submission
    {
        public Assignment Assignment { get; set; }
        public Student Student { get; set; }
        public DateTime SubmissionTime { get; set; }
        public int Score { get; set; }

        public override string ToString()
        {
            return base.ToString() + " - Student Name: " + Student.FullName + ", Submission Time: " + SubmissionTime.ToString() + ", Score: " + Score;
        }
    }
}
