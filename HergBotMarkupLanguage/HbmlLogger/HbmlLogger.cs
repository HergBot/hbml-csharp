/*
* PROJECT: HBML C#
* PROGRAMMER: Justin
* FIRST VERSION: 17/06/2019
*/

using HergBot.Logging;

namespace HergBot.MarkupLanguage.HbmlLogging
{
    public class HbmlLogger : FileLogger
    {
        /// <summary>
        /// The extension to use for the HBML logs
        /// </summary>
        private const string HBML_LOG_EXTENSION = ".hbml";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configFile">The path to the logging configuration file</param>
        public HbmlLogger(string configFile) : base(configFile, new HbmlLogMessageGenerator(), HBML_LOG_EXTENSION)
        {

        }
    }
}
