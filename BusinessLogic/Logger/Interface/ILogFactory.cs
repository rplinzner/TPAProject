using System.Security;
using BusinessLogic.Logger.Enum;

namespace BusinessLogic.Logger.Interface
{
    public interface ILogFactory
    {
        void AddLogger(ILogger logger);
        void RemoveLogger(ILogger logger);
        void Log(MessageStructure message, LogLevelEnum level = LogLevelEnum.Informative);
    }
}