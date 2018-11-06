using BusinessLogic.Logger.Enum;
using BusinessLogic.Logger.Interface;
using System.Collections.Generic;

namespace BusinessLogic.Logger
{
    public class BaseLogFactory : ILogFactory
    {
        #region Protected Methods

        /// <summary>
        /// The list of loggers in this factory
        /// </summary>
        protected List<ILogger> mLoggers = new List<ILogger>();

        /// <summary>
        /// A lock for the logger list to keep it thread-safe
        /// </summary>
        protected object mLoggersLock = new object();

        #endregion

        #region Public Properties

        /// <summary>
        /// The level of logging to output
        /// </summary>
        public LogOutputLevelEnum LogOutputLevel { get; set; }

        #endregion

        #region Constructor

        public BaseLogFactory(LogOutputLevelEnum selectedLogLevel = LogOutputLevelEnum.Informative)
        {
            LogOutputLevel = selectedLogLevel;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="loggers">The loggers to add to the factory, on top of the stock loggers already included</param>
        public BaseLogFactory(List<ILogger> loggers, LogOutputLevelEnum selectedLogLevel = LogOutputLevelEnum.Informative)
        {
            LogOutputLevel = selectedLogLevel;
            mLoggers = loggers;

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the specific logger to this factory
        /// </summary>
        /// <param name="logger">The logger</param>
        public void AddLogger(ILogger logger)
        {
            // Log the list so it is thread-safe
            lock (mLoggersLock)
            {
                // If the logger is not already in the list...
                if (!mLoggers.Contains(logger))
                    // Add the logger to the list
                    mLoggers.Add(logger);
            }
        }

        /// <summary>
        /// Removes the specified logger from this factory
        /// </summary>
        /// <param name="logger">The logger</param>
        public void RemoveLogger(ILogger logger)
        {
            // Log the list so it is thread-safe
            lock (mLoggersLock)
            {
                // If the logger is in the list...
                if (mLoggers.Contains(logger))
                    // Remove the logger from the list
                    mLoggers.Remove(logger);
            }
        }

        /// <summary>
        /// Logs the specific message to all loggers in this factory
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="level">The level of the message being logged</param>
        /// <param name="origin">The method/function this message was logged in</param>
        /// <param name="filePath">The code filename that this message was logged from</param>
        /// <param name="lineNumber">The line of code in the filename this message was logged from</param>
        public void Log(MessageStructure message, LogLevelEnum level = LogLevelEnum.Informative)
        {
            // If we should not log the message as the level is too low...
            if ((int)level < (int)LogOutputLevel)
                return;

            // Log the list so it is thread-safe
            lock (mLoggersLock)
            {
                // Log to all loggers
                mLoggers.ForEach(logger => logger.Log(message, level));
            }
        }

        #endregion
    }
}
