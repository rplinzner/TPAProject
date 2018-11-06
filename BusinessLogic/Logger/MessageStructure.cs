using System.Runtime.CompilerServices;

namespace BusinessLogic.Logger
{
    public struct MessageStructure
    {
        public string Message { get; set; }
        public string OriginName { get; set; }
        public string FileName { get; set; }
        public int LineNumber { get; set; }

        public MessageStructure(string message, [CallerMemberName] string originName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0)
        {
            Message = message;
            OriginName = originName;
            FileName = fileName;
            LineNumber = lineNumber;
        }
    }
}