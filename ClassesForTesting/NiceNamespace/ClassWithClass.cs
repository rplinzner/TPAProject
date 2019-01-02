using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.ClassesForTesting.NiceNamespace
{
    public class ClassWithClass
    {
        public int Property1 { get; set; }
        public string Property2 { get; set; }

        private NestedClass nestedClass;
        public class NestedClass
        {
            private string a;
            private int b;  
        }
    }
}
