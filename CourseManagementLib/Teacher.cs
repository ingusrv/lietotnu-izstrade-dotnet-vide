using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CourseManagementLib
{
    public class Teacher : Person
    {
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly ContractDate { get; set; }

        public override string ToString()
        {
            return base.ToString() + ", Contract Date: " + ContractDate;
        }
    }
}
