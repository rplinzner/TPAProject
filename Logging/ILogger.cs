namespace Logging
{
    public interface ILogger
    {
        /// <summary>
        /// Handles the logged message being passed in
        /// </summary>
        /// <param name="message">The message being log</param>
        /// <param name="level">The level of the log message</param>
        void Log(MessageStructure message, LogLevelEnum level = LogLevelEnum.Informative);
    }
}