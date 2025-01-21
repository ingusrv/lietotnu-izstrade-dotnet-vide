using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace nmd3
{
    // https://learn.microsoft.com/en-us/dotnet/standard/assembly/unloadability
    public class AssemblyContext : AssemblyLoadContext
    {
        public AssemblyContext() : base(isCollectible: true) { }

        protected override Assembly? Load(AssemblyName name)
        {
            return null;
        }
    }
}
