using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace md2
{
    public interface IDataManager
    {
        public DataRepository Data { get; set; }

        string Print();
        bool Save(string path);
        bool Load(string path);
        void CreateTestData();
        void Reset();
    }

}
