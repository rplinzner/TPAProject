using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesForTesting
{
    public class Class1 : Interface1
    {
        public int Property1 { get; set; }
        private AbstractClass1 abstractClass;

        public Class1()
        {

        }

        public Class1(int property1)
        {
            Property1 = property1;
        }

        public void Method1()
        {

        }

        public int Method2(int arg)
        {
            return arg;
        }
    }
}
