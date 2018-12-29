using Interfaces;
using System;
using System.ComponentModel.Composition;
using System.IO;

namespace CMDPathFinder
{
    [Export(typeof(IPathFinder))]
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
