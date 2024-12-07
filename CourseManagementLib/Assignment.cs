using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CourseManagementLib
{
    public class Assignment
    {
        public int Id { get; set; }
        // ja pieminēts vienkārši tips "Date", tad gan jau sagaidīts ka izmanto DateOnly nevis DateTime?
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly Deadline { get; set; }
        public Course? Course { get; set; }
        public string? Description { get; set; }

        public override string ToString()
        {
            return base.ToString() + " - Description: " + Description + ", Deadline: " + Deadline.ToString() + ", Course: " + Course?.ToString();
        }
    }
}
