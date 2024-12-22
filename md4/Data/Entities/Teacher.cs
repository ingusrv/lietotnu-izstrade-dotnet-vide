using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace md4.Data.Entities
{
    public class Teacher : Person
    {
        public DateTime? ContractDate { get; set; }

        public override string ToString()
        {
            return base.ToString() + ", Contract Date: " + ContractDate;
        }
    }
}
