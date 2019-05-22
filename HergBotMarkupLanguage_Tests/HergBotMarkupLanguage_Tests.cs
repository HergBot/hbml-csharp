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
            _testElement = new HbmlElement();
        }

        [Test]
        public void CreateElement()
        {
            Assert.AreEqual(_testElement.Label, string.Empty);
            Assert.AreEqual(_testElement.Value, string.Empty);
            Assert.IsFalse(_testElement.HasAttributes);
            Assert.IsFalse(_testElement.HasChildren);
        }

        [Test]
        public void CreateElement_WithLabel()
        {
            HbmlElement element = new HbmlElement(TEST_LABEL);

            Assert.AreEqual(element.Label, TEST_LABEL);
            Assert.AreEqual(element.Value, string.Empty);
            Assert.IsFalse(element.HasAttributes);
            Assert.IsFalse(element.HasChildren);
        }

        [Test]
        public void CreateElement_WithLabelAndValue()
        {
            HbmlElement element = new HbmlElement(TEST_LABEL, TEST_VALUE);

            Assert.AreEqual(element.Label, TEST_LABEL);
            Assert.AreEqual(element.Value, TEST_VALUE);
            Assert.IsFalse(element.HasAttributes);
            Assert.IsFalse(element.HasChildren);
        }

        [Test]
        public void AddAttribute()
        {
            _testElement.AddAttribute(TEST_ATTRIBUTE_NAME, TEST_ATTRIBUTE_VALUE);
        }

        [Test]
        public void AddAttribute_AlreadyExists()
        {

        }

        [Test]
        public void GetAttribute_DoesNotExist()
        {

        }
    }
}
