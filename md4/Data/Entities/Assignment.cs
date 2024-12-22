using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace md4.Data.Entities
{
    public class Assignment
    {
        public int Id { get; set; }
        public DateTime? Deadline { get; set; }
        public int? CourseId { get; set; }
        public Course? Course { get; set; }
        public string? Description { get; set; }

        public override string ToString()
        {
            return base.ToString() + " - Description: " + Description + ", Deadline: " + Deadline.ToString() + ", Course: " + Course?.ToString();
        }
    }
}
