using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using HergBot.MarkupLanguage.HbmlLogging;

namespace HergBotMarkupLanguage_Tests
{
    public class HbmlLogMessageGenerator_Tests
    {
        private const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        private const string TEST_THREAD_NAME = "TestThread";

        private const string TEST_METHOD_NAME = "TestMethod";

        private const string TEST_TYPE = "TestType";

        private const string TEST_MESSAGE = "Test Message";

        private const string TAB = "    ";

        private HbmlLogMessageGenerator _generator;

        [SetUp]
        public void SetUp()
        {
            _generator = new HbmlLogMessageGenerator();
        }

        [Test]
        public void GenerateLogMessage_Format()
        {
            string timestamp = DateTime.Now.Date.ToString(DATE_TIME_FORMAT);
            string expected = $"<LogEntry>\n{TAB}<Date>{timestamp}</Date>\n{TAB}<Type>{TEST_TYPE}</Type>\n{TAB}<Thread>{TEST_THREAD_NAME}</Thread>\n{TAB}<Method>{TEST_METHOD_NAME}</Method>\n{TAB}<Message>{TEST_MESSAGE}</Message>\n</LogEntry>";
            string actual = _generator.GenerateLogMessage(
                timestamp,
                TEST_THREAD_NAME,
                TEST_METHOD_NAME,
                TEST_TYPE,
                TEST_MESSAGE
            );
            Assert.AreEqual(expected, actual);
        }
    }
}
