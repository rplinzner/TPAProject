using System;
using System.ComponentModel.Composition;
using Logging;

namespace DBData
{
    [Export(typeof(ILogger))]
    public class DatabaseLogger : ILogger
    {
        public void Log(MessageStructure message, LogLevelEnum level = LogLevelEnum.Informative)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.Log.Add(new LogModel() {
                    Message = message.Message,
                    FileName = message.FileName,
                    Line = message.LineNumber,
                    OriginName = message.OriginName,
                    LogCategory = level.ToString(),
                    Time = DateTime.Now
                });
                context.SaveChanges();
            }
        }
    }
}