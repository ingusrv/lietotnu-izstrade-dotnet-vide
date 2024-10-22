using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace md2
{
        public abstract class Person
    {
        private string _name;
        private string _surname;

        public string Name
        {
            get { return _name; }
            set { if (value != null && value != "") _name = value; }
        }

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        public string FullName
        {
            get { return _name + " " + _surname; }
        }

        public Gender Gender { get; set; }

        public override string ToString()
        {
            return base.ToString() + " - Full Name: " + FullName + ", Gender: " + Gender;
        }
    }

}
