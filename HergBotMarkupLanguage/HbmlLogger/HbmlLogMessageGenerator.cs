/*
* PROJECT: HBML C#
* PROGRAMMER: Justin
* FIRST VERSION: 17/06/2019
*/

using HergBot.Logging;

namespace HergBot.MarkupLanguage.HbmlLogging
{
    /// <summary>
    /// Formats the messages for HergBot Markup Language logging
    /// </summary>
    public class HbmlLogMessageGenerator : ILogMessageGenerator
    {
        /// <summary>
        /// The label to use for a log entry element
        /// </summary>
        private const string LOG_ELEMENT_LABEL = "LogEntry";

        /// <summary>
        /// The label to use for the log entry date
        /// </summary>
        private const string LOG_DATE_LABEL = "Date";

        /// <summary>
        /// The label to use for the log entry type
        /// </summary>
        private const string LOG_TYPE_LABEL = "Type";

        /// <summary>
        /// The label to use for the log entry thread name
        /// </summary>
        private const string LOG_THREAD_NAME_LABEL = "Thread";

        /// <summary>
        /// The label to use for the log entry method name
        /// </summary>
        private const string LOG_METHOD_NAME_LABEL = "Method";

        /// <summary>
        /// The label to use for the log entry message
        /// </summary>
        private const string LOG_MESSAGE_LABEL = "Message";

        /// <summary>
        /// Generates a formatted message for HergBot Markup Language logging
        /// </summary>
        /// <param name="timestamp">The logs timestamp</param>
        /// <param name="threadName">The name of the thread the message is logged from</param>
        /// <param name="methodName">The name of the method logging the message</param>
        /// <param name="type">The label for the type of log message</param>
        /// <param name="message">The log message</param>
        /// <returns>A formatted HBML element string for logging</returns>
        public string GenerateLogMessage(string timestamp, string threadName, string methodName, string type, string message)
        {
            // Build the element for the log entry
            HbmlElement logElement = new HbmlElement(LOG_ELEMENT_LABEL);
            logElement.AddElement(LOG_DATE_LABEL, timestamp);
            logElement.AddElement(LOG_TYPE_LABEL, type);
            logElement.AddElement(LOG_THREAD_NAME_LABEL, threadName);
            logElement.AddElement(LOG_METHOD_NAME_LABEL, methodName);
            logElement.AddElement(LOG_MESSAGE_LABEL, message);
            return logElement.ToString();
        }
    }
}
