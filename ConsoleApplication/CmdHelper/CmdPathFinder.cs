using BusinessLogic.ViewModel;
using System;
using System.IO;

namespace ConsoleApplication.CmdHelper
{
    public class CmdPathFinder : IPathFinder
    {
        public string FindPath()
        {
            string path = Console.ReadLine();

            if (path != null && File.Exists(path) && path.Contains(".dll"))
            {
                return path;
            }
            Console.WriteLine("Wrong path!");
            return null;
        }
    }
}
