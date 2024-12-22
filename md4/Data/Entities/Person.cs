using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace md4.Data.Entities
{
    public abstract class Person
    {
        public int Id { get; set; }
        private string? _name;
        private string? _surname;

        public string? Name
        {
            get { return _name; }
            set { if (value != null && value != "") _name = value; }
        }

        public string? Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        public string FullName
        {
            get { return _name + " " + _surname; }
        }

        public Gender? Gender { get; set; }

        public override string ToString()
        {
            return base.ToString() + " - Full Name: " + FullName + ", Gender: " + Gender;
        }
    }

}
