/*
* PROJECT: HBML C#
* PROGRAMMER: Justin
* FIRST VERSION: 12/08/2017
*/

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HergBotMarkupLanguage
{
    public class HbmlElement
    {
        /// <summary>
        /// Number of spaces to use as a tab
        /// </summary>
        private const int TAB_SPACES = 4;

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

        /// <summary>
        /// Checks if the element has attributes
        /// </summary>
        public bool HasAttributes
        {
            get
            {
                return _elementAttributes.Any();
            }
        }

        /// <summary>
        /// Checks if the element has children
        /// </summary>
        public bool HasChildren
        {
            get
            {
                return _elementChildren.Any();
            }
        }

        /// <summary>
        /// Constructor to initialize variable defaults
        /// </summary>
        private HbmlElement()
        {
            Label = string.Empty;
            Value = string.Empty;
            _elementAttributes = new Dictionary<string, string>();
            _elementChildren = new Dictionary<string, HbmlElement>();
        }

        /// <summary>
        /// Initializes an element with just a label
        /// </summary>
        /// <param name="label"></param>
        public HbmlElement(string label) : this()
        {
            Label = label;
        }

        /// <summary>
        /// Initializes an element with a label and value
        /// </summary>
        /// <param name="label">The elements label</param>
        /// <param name="value">The elements value</param>
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
        /// <returns>The attribute value or null if nothing is found</returns>
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

            return AddElement(newElement);
        }

        /// <summary>
        /// Adds a HbmlElement object to this element
        /// </summary>
        /// <param name="element">The element to add</param>
        /// <returns>True if the element was successfully added</returns>
        public bool AddElement(HbmlElement element)
        {
            return AddItemToDictionary(_elementChildren, element.Label, element);
        }

        /// <summary>
        /// Gets a child element
        /// </summary>
        /// <param name="elementLabel">The child elements label</param>
        /// <returns>The element or null if nothing is found</returns>
        public HbmlElement GetChildElement(string elementLabel)
        {
            HbmlElement element = null;
            GetItemFromDictionary(_elementChildren, elementLabel, out element);
            return element;
        }

        /// <summary>
        /// Gets the value of a child element
        /// </summary>
        /// <param name="elementLabel">The element label to find</param>
        /// <returns>The child element's value or null if no element is found</returns>
        public string GetChildElementValue(string elementLabel)
        {
            HbmlElement foundElement;
            bool elementFound = GetItemFromDictionary(_elementChildren, elementLabel, out foundElement);

            if (elementFound)
            {
                return foundElement.Value;
            }

            return null;
        }

        /// <summary>
        /// Turns the element into its string form
        /// </summary>
        /// <returns>The element string</returns>
        public override string ToString()
        {
            return GetElementString();
        }

        /// <summary>
        /// Get the element and any children as a full string
        /// </summary>
        /// <param name="currentDepth">The current depth of the element</param>
        /// <returns>The full formatted string</returns>
        private string GetElementString(int currentDepth = 0)
        {
            StringBuilder fullString = new StringBuilder();
            string tabString = FormatTabString(currentDepth);
            string valueTabString = FormatTabString(currentDepth + 1);

            // Insert a new line if we are into a nested element so we dont have a newline character
            // on single elements
            if (currentDepth > 0)
            {
                fullString.Append("\n");
            }

            // Fill in the opening tag
            fullString.Append(FormatOpeningTag(tabString));

            // Print the value
            fullString.Append(FormatValue(valueTabString));

            // If there are children we need to print them
            if (HasChildren)
            {
                ++currentDepth;
                foreach (HbmlElement element in _elementChildren.Values)
                {
                    fullString.Append(element.GetElementString(currentDepth));
                }
            }

            // Append the closing tag
            fullString.Append(FormatClosingTag(tabString));

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

        /// <summary>
        /// Format the opening tag depending on attributes of the element
        /// </summary>
        /// <param name="tabString">The current tab string to use for indentation</param>
        /// <returns>The complete opening tag</returns>
        private string FormatOpeningTag(string tabString)
        {
            if (HasAttributes)
            {
                string attributes = string.Join(
                    " ",
                    _elementAttributes.Select(attribute => $"{attribute.Key}=\"{attribute.Value}\"")
                );
                return $"{tabString}<{Label} {attributes}>";
            }

            return $"{tabString}<{Label}>";
        }

        /// <summary>
        /// Format the closing tag depending on if the element has children
        /// </summary>
        /// <param name="tabString">The current tab string to use for indentation</param>
        /// <returns>The complete closing tag</returns>
        private string FormatClosingTag(string tabString)
        {
            if (HasChildren)
            {
                return $"\n{tabString}</{Label}>";
            }

            return $"</{Label}>";
        }

        /// <summary>
        /// Format the value depending on if the element has children
        /// </summary>
        /// <param name="tabString">The current tab string for the value to use for indentation</param>
        /// <returns>The value string</returns>
        private string FormatValue(string tabString)
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                return string.Empty;
            }

            if (HasChildren)
            {
                return $"\n{tabString}{Value}";
            }

            return Value;
        }

        /// <summary>
        /// Formats a string of spaces to use for indentation based on the depth of element nesting
        /// </summary>
        /// <param name="depth">The amount of nesting depth in the elements</param>
        /// <returns>A string of spaces to use for indentation</returns>
        private string FormatTabString(int depth)
        {
            StringBuilder tabString = new StringBuilder();
            // Calculate how many spaces we need infront of the tag
            tabString.Insert(0, " ", depth * TAB_SPACES);
            return tabString.ToString();
        }
    }
}
