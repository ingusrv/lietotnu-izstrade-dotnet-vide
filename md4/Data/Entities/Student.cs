using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace md4.Data.Entities
{
    public class Student : Person
    {
        public string? StudentIdNumber { get; set; }

        public Student() { }

        public Student(string name, string surname, Gender gender, string studentIdNumber)
        {
            base.Name = name;
            base.Surname = surname;
            base.Gender = gender;
            StudentIdNumber = studentIdNumber;
        }

        public override string ToString()
        {
            return base.ToString() + ", Student Id Number: " + StudentIdNumber;
        }
    }
}
