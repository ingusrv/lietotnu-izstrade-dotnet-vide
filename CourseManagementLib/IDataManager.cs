using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementLib
{
    public interface IDataManager
    {
        string Print();
        bool Save(string path);
        bool Load(string path);
        bool CreateTestData();
        bool Reset();
    }

}
