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

            if (path != null && File.Exists(path) && (path.EndsWith(".dll") || path.EndsWith(".xml")))
            {
                return path;
            }
            return null;
        }

        public string SaveToPath()
        {
            string path = Console.ReadLine();

            if (path != null && path.EndsWith(".xml"))
            {
                return path;
            }
            return null;
        }
    }
}
