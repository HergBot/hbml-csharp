// PROJECT		    : HBML C#
// PROGRAMMER	    : Justin
// FIRST VERSION	: 12/08/2017

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HergBotMarkupLanguage
{
    public class HbmlElement
    {
        /// <summary>
        /// The attributes for the element
        /// </summary>
        private Dictionary<string, string> _elementAttributes;

        /// <summary>
        /// The elements children
        /// </summary>
        private Dictionary<string, HbmlElement> _elementChildren;

        /// <summary>
        /// The label for the element
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The value for the element
        /// </summary>
        public string Value { get; set; }

        public bool HasAttributes
        {
            get
            {
                return _elementAttributes.Count != 0;
            }
        }

        public bool HasChildren
        {
            get
            {
                return _elementChildren.Count != 0;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public HbmlElement()
        {
            Label = string.Empty;
            Value = string.Empty;
            _elementAttributes = new Dictionary<string, string>();
            _elementChildren = new Dictionary<string, HbmlElement>();
        }

        public HbmlElement(string label) : this()
        {
            Label = label;
        }

        public HbmlElement(string label, string value) : this()
        {
            Label = label;
            Value = value;
        }

        /// <summary>
        /// Adds an attribute to the element
        /// </summary>
        /// <param name="attributeName">The unique attribute name</param>
        /// <param name="attributeValue">The attribute value</param>
        /// <returns>True if the attribute was added</returns>
        public bool AddAttribute(string attributeName, string attributeValue)
        {
            return AddItemToDictionary(_elementAttributes, attributeName, attributeValue);
        }

        /// <summary>
        /// Gets an attributes value from the element
        /// </summary>
        /// <param name="attributeName">The unique attribute name to get</param>
        /// <param name="attributeValue">The value of the attribute if found</param>
        /// <returns>True if the attribute was found</returns>
        public string GetAttributeValue(string attributeName)
        {
            string attributeValue = null;
            GetItemFromDictionary(_elementAttributes, attributeName, out attributeValue);
            return attributeValue;
        }

        /// <summary>
        /// Add a plain element with just a label and value
        /// </summary>
        /// <param name="elementLabel">The unique element label to add</param>
        /// <param name="elementValue">The element value</param>
        /// <returns>True if the element was added</returns>
        public bool AddElement(string elementLabel, string elementValue)
        {
            HbmlElement newElement = new HbmlElement()
            {
                Label = elementLabel,
                Value = elementValue
            };

            return AddElement(elementLabel, newElement);
        }

        /// <summary>
        /// Adds a HbmlElement object to this element
        /// </summary>
        /// <param name="elementLabel">The unique element label</param>
        /// <param name="element">The element to add</param>
        /// <returns>True if the element was successfully add</returns>
        public bool AddElement(string elementLabel, HbmlElement element)
        {
            return AddItemToDictionary(_elementChildren, elementLabel, element);
        }

        /// <summary>
        /// Gets a child element
        /// </summary>
        /// <param name="elementLabel">The child elements label</param>
        /// <param name="element">The child element if found</param>
        /// <returns>True if the element was found</returns>
        public bool GetChildElement(string elementLabel, out HbmlElement element)
        {
            return GetItemFromDictionary(_elementChildren, elementLabel, out element);
        }

        /// <summary>
        /// Gets the value of a child element
        /// </summary>
        /// <param name="elementLabel">The element label to find</param>
        /// <param name="elementValue">The element value if found</param>
        /// <returns>True if the element is found</returns>
        public bool GetChildElementValue(string elementLabel, out string elementValue)
        {
            HbmlElement foundElement;
            elementValue = string.Empty;
            bool elementFound = GetItemFromDictionary(_elementChildren, elementLabel, out foundElement);

            if (elementFound)
            {
                elementValue = foundElement.Value;
            }

            return elementFound;
        }

        /// <summary>
        /// Get the element and any children as a full string
        /// </summary>
        /// <param name="currentDepth">The current depth of the element</param>
        /// <returns>The full formatted string</returns>
        public string GetElementString(int currentDepth = 0)
        {
            StringBuilder fullString = new StringBuilder();
            StringBuilder tabString = new StringBuilder();

            // Calculate how many tabs we need infront of the tag
            tabString.Insert(0, "\t", currentDepth);

            // Fill in the opening tag for this element
            fullString.Append(tabString);
            fullString.Append($"<{Label}");

            // If there are any attributes add them to the opening tag
            if (_elementAttributes.Any())
            {
                foreach (string elementKey in _elementAttributes.Keys)
                {
                    fullString.Append($" {elementKey}=\"{_elementAttributes[elementKey]}\"");
                }
            }
            // Close the opening tag
            fullString.Append(">");

            // If there are children we need to print them
            if (_elementChildren.Any())
            {
                // Add a new line and increase the depth
                fullString.Append("\n");
                ++currentDepth;
                foreach (HbmlElement element in _elementChildren.Values)
                {
                    fullString.Append(element.GetElementString(currentDepth));
                }
            }
            else
            {
                // We just print the value and close the tag
                fullString.Append($"{Value}");
            }

            // Append the closing tag
            fullString.Append($"</{Label}>\n");

            return fullString.ToString();
        }

        /// <summary>
        /// Generic method to add an item safely to a dictionary
        /// </summary>
        /// <typeparam name="TKey">The type the dictionary key holds</typeparam>
        /// <typeparam name="TValue">The type the dictionary value holds</typeparam>
        /// <param name="dictionary">The dictionary to add to</param>
        /// <param name="key">The dictionary key</param>
        /// <param name="value">The dictionary value to add</param>
        /// <returns>True if the value was added</returns>
        private bool AddItemToDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            // Check if key exists
            if (dictionary.ContainsKey(key))
            {
                return false;
            }

            dictionary.Add(key, value);

            return true;
        }

        /// <summary>
        /// Generic method to get an item safely from a dictionary
        /// </summary>
        /// <typeparam name="TKey">The type the dictionary key holds</typeparam>
        /// <typeparam name="TValue">The type the dictionary value holds</typeparam>
        /// <param name="dictionary">The dictionary to get from</param>
        /// <param name="key">The dictionary key to find</param>
        /// <param name="value">The dictionary value if found</param>
        /// <returns>True if the item was found</returns>
        private bool GetItemFromDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key, out TValue value)
        {
            value = default(TValue);

            // Check if key exists
            if (!dictionary.ContainsKey(key))
            {
                return false;
            }

            value = dictionary[key];

            return true;
        }
    }
}
