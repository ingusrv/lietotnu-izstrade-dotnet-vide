using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace md4.Data.Entities
{
    public class Submission
    {
        public int Id { get; set; }
        public int? AssignmentId { get; set; }
        public Assignment? Assignment { get; set; }
        public int? StudentId { get; set; }
        public Student? Student { get; set; }
        public DateTime? SubmissionTime { get; set; }
        public int? Score { get; set; }

        public override string ToString()
        {
            return base.ToString() + " - Student Name: " + Student?.FullName + ", Submission Time: " + SubmissionTime.ToString() + ", Score: " + Score;
        }
    }
}
