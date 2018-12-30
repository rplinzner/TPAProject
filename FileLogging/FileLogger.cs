using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.IO;
using Logging;

namespace FileLogging
{
    [Export(typeof(ILogger))]
    public class FileLogger : ILogger
    {
        public string FilePath { get; set; }

        public LogOutputLevelEnum OutputLevel { get; set; }

        public FileLogger()
        {
            GetSettings();
        }

        public async void Log(MessageStructure message, LogLevelEnum level)
        {
            if ((int)level < (int)OutputLevel)
            {
                return;
            }
            using (TextWriter fileStream = new StreamWriter(File.Open(FilePath, FileMode.Append)))
            {
                string msg =
                    $"[{DateTime.Now:yyyy-MM-dd hh:mm:ss tt}] " + $"[{level}]".PadRight(15) +
                    $" [{message.FileName}] in {message.OriginName}() line {message.LineNumber}: {message.Message}";
                await fileStream.WriteLineAsync(msg);
            }
        }

        public void GetSettings()
        {
            FilePath = ConfigurationManager.AppSettings["loggingFilename"] ?? "Log.txt";
            string LogLvl = ConfigurationManager.AppSettings["loggingLevel"] ?? "1";
            int level = int.Parse(LogLvl);
            OutputLevel = (LogOutputLevelEnum)level;

        }
    }
}