using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.ClassesForTesting
{
    public static class StaticClass1
    {
        private static int staticField;

        public static string Method(string arg1)
        {
            return arg1;
        }

        public static int ExtensionMethod(this int arg2)
        {
            return arg2;
        }
    }
}
