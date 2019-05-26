using NUnit.Framework;

using HergBotMarkupLanguage;

namespace HergBotMarkupLanguage_Tests
{
    [TestFixture]
    public class HbmlElement_Test
    {
        private const string TEST_LABEL = "TestLabel";

        private const string TEST_VALUE = "TestValue";

        private const string TEST_ATTRIBUTE_NAME = "TestAttributeName";

        private const string TEST_ATTRIBUTE_VALUE = "TestAttributeValue";

        private HbmlElement _testElement;

        [SetUp]
        protected void SetUp()
        {
            _testElement = new HbmlElement(TEST_LABEL);
        }

        [Test]
        public void CreateElement()
        {
            Assert.AreEqual(TEST_LABEL, _testElement.Label);
            Assert.AreEqual(string.Empty, _testElement.Value);
            Assert.IsFalse(_testElement.HasAttributes);
            Assert.IsFalse(_testElement.HasChildren);
        }


        [Test]
        public void CreateElement_WithLabelAndValue()
        {
            HbmlElement element = new HbmlElement(TEST_LABEL, TEST_VALUE);

            Assert.AreEqual(TEST_LABEL, element.Label);
            Assert.AreEqual(TEST_VALUE, element.Value);
            Assert.IsFalse(element.HasAttributes);
            Assert.IsFalse(element.HasChildren);
        }

        [Test]
        public void AddAttribute()
        {
            _testElement.AddAttribute(TEST_ATTRIBUTE_NAME, TEST_ATTRIBUTE_VALUE);
            string attribute = _testElement.GetAttributeValue(TEST_ATTRIBUTE_NAME);
            Assert.AreEqual(TEST_ATTRIBUTE_VALUE, attribute);
        }

        [Test]
        public void AddAttribute_AlreadyExists()
        {
            _testElement.AddAttribute(TEST_ATTRIBUTE_NAME, TEST_ATTRIBUTE_VALUE);
            bool secondAdded = _testElement.AddAttribute(TEST_ATTRIBUTE_NAME, TEST_ATTRIBUTE_VALUE);
            Assert.IsFalse(secondAdded);
        }

        [Test]
        public void AddAttribute_HasAttributes()
        {
            _testElement.AddAttribute(TEST_ATTRIBUTE_NAME, TEST_ATTRIBUTE_VALUE);
            Assert.IsTrue(_testElement.HasAttributes);
        }

        [Test]
        public void GetAttribute_DoesNotExist()
        {
            string attribute = _testElement.GetAttributeValue(TEST_ATTRIBUTE_NAME);
            Assert.IsNull(attribute);
        }

        [Test]
        public void AddElement()
        {
            _testElement.AddElement(TEST_LABEL, TEST_VALUE);
            HbmlElement child = _testElement.GetChildElement(TEST_LABEL);
            Assert.IsNotNull(child);
        }

        [Test]
        public void AddElement_Constructed()
        {
            HbmlElement childAdded = new HbmlElement(TEST_LABEL, TEST_VALUE);
            _testElement.AddElement(childAdded);
            HbmlElement childReceived = _testElement.GetChildElement(childAdded.Label);
            Assert.AreEqual(childAdded, childReceived);
        }

        [Test]
        public void AddElement_LabelAlreadyExists()
        {
            _testElement.AddElement(TEST_LABEL, TEST_VALUE);
            bool secondAdded = _testElement.AddElement(TEST_LABEL, TEST_VALUE);
            Assert.IsFalse(secondAdded);
        }

        [Test]
        public void GetChildElement_DoesNotExist()
        {
            HbmlElement child = _testElement.GetChildElement(TEST_LABEL);
            Assert.IsNull(child);
        }

        [Test]
        public void GetChildElementValue()
        {
            _testElement.AddElement(TEST_LABEL, TEST_VALUE);
            string childValue = _testElement.GetChildElementValue(TEST_LABEL);
            Assert.AreEqual(TEST_VALUE, childValue);
        }

        [Test]
        public void GetChildElementValue_DoesNotExist()
        {
            string childValue = _testElement.GetChildElementValue(TEST_LABEL);
            Assert.IsNull(childValue);
        }

        [Test]
        public void ToString_Base()
        {
            string expected = $"<{TEST_LABEL}></{TEST_LABEL}>";
            Assert.AreEqual(expected, _testElement.ToString());
        }

        [Test]
        public void ToString_NoChildren()
        {
            _testElement.Value = TEST_VALUE;
            string expected = $"<{TEST_LABEL}>{TEST_VALUE}</{TEST_LABEL}>";
            Assert.AreEqual(expected, _testElement.ToString());
        }

        [Test]
        public void ToString_WithChildren()
        {
            _testElement.AddElement(TEST_LABEL, TEST_VALUE);
            string expected = $"<{TEST_LABEL}>\n    <{TEST_LABEL}>{TEST_VALUE}</{TEST_LABEL}>\n</{TEST_LABEL}>";
            Assert.AreEqual(expected, _testElement.ToString());
        }

        [Test]
        public void ToString_WithChildrenAndLabel()
        {
            _testElement.Value = TEST_VALUE;
            _testElement.AddElement(TEST_LABEL, TEST_VALUE);
            string expected = $"<{TEST_LABEL}>\n    {TEST_VALUE}\n    <{TEST_LABEL}>{TEST_VALUE}</{TEST_LABEL}>\n</{TEST_LABEL}>";
            Assert.AreEqual(expected, _testElement.ToString());
        }

        [Test]
        public void ToString_MultiLayered()
        {
            HbmlElement childElement = new HbmlElement(TEST_LABEL, TEST_VALUE);
            childElement.AddElement(TEST_LABEL, TEST_VALUE);
            _testElement.Value = TEST_VALUE;
            _testElement.AddAttribute(TEST_ATTRIBUTE_NAME, TEST_ATTRIBUTE_VALUE);
            _testElement.AddElement(childElement);
            string expected = $"<{TEST_LABEL} {TEST_ATTRIBUTE_NAME}=\"{TEST_ATTRIBUTE_VALUE}\">\n    {TEST_VALUE}\n    <{TEST_LABEL}>\n        {TEST_VALUE}\n        <{TEST_LABEL}>{TEST_VALUE}</{TEST_LABEL}>\n    </{TEST_LABEL}>\n</{TEST_LABEL}>";
            Assert.AreEqual(expected, _testElement.ToString());
        }

        [Test]
        public void ToString_WithAttrbutes()
        {
            _testElement.AddAttribute(TEST_ATTRIBUTE_NAME, TEST_ATTRIBUTE_VALUE);
            string expected = $"<{TEST_LABEL} {TEST_ATTRIBUTE_NAME}=\"{TEST_ATTRIBUTE_VALUE}\"></{TEST_LABEL}>";
            Assert.AreEqual(expected, _testElement.ToString());
        }
    }
}
