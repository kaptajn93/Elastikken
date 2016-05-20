using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Elastikken.Parsing
{
    public static class XElementExtensionMethods
    {
        public static string InnerXmlExcludingElements(this XElement xElement, string elementNameToExlude)
        {
            var result = new XElement(xElement);
            IEnumerable<XElement> elementsToExclude = result.Elements(elementNameToExlude);
            elementsToExclude.Remove();
            IEnumerable<XNode> remainingNodes = result.Nodes();
            string innerXml = string.Concat(remainingNodes).Trim();

            return innerXml;
        }

        public static IList<T> ChildXElementsOfExtensionType<T>(this XElement element, string childElementName, Func<XElement, T> extensionTypeFromXElement)
        {
            var result = new List<T>();
            IEnumerable<XElement> childElements = element.Elements(childElementName);

            foreach (XElement childElement in childElements)
            { 
                T convertedChildElementType = extensionTypeFromXElement(childElement);
                result.Add(convertedChildElementType);
            }

            return result;
        }

        public static ICollection<T> XPathSelectElementsOfType<T>(this XElement element, string xPath,
            Func<XElement, T> extensionTypeFromXElement)
        {
            var result = new Collection<T>();
            var childElements = element.XPathSelectElements(xPath);

            foreach (XElement childElement in childElements)
            {
                T convertedChildElementType = extensionTypeFromXElement(childElement);
                result.Add(convertedChildElementType);
            }

            return result;
        }

        public static T ChildElementOfType<T>(this XElement element, string childElementName,
            Func<XElement, T> elementTypeFromXElement)
        {
            XElement childElement = element.Element(childElementName);
            T convertedChildElementType = elementTypeFromXElement(childElement);
            return convertedChildElementType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <param name="childElementName"></param>
        /// <param name="recurse"></param>
        /// <returns></returns>
        public static T ChildElementValueOrDefault<T>(this XElement element, string childElementName,
            bool recurse = false)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            XElement childElement = element.Element(childElementName);
            string result = "";
            if (childElement != null)
            {
                result = recurse
                    ? childElement.Value
                    : childElement.ValueWithoutChildren();

                result = result.Trim();
            }

            //Cast ConvertFromString(string text) : object to (T)
            return (T)converter.ConvertFromString(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="childElementName"></param>
        /// <returns></returns>
        public static string ChildElementInnerXmlOrDefault(this XElement element, string childElementName)
        {
            XElement childElement = element.Element(childElementName);
            return childElement.InnerXmlOrDefault();
        }

        public static XElement ElementOrEmpty(this XElement element, string childElementName)
        {
            return element.Element(childElementName) ?? new XElement(childElementName);
        }

        public static string InnerXmlOrDefault(this XElement element)
        {
            if (element == null) return string.Empty;

            var xReader = element.CreateReader();
            xReader.MoveToContent();
            return xReader.ReadInnerXml();
        }

        public static string OuterXmlOrDefault(this XElement element)
        {
            if (element == null) return string.Empty;

            var xReader = element.CreateReader();
            xReader.MoveToContent();
            return xReader.ReadOuterXml();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static string AttributeValueOrDefault(this XElement element, string attributeName)
        {
            XAttribute attr = element.Attribute(attributeName);
            string result = (attr != null) ? attr.Value : "";
            return result;
        }

        public static T AttributeValueOrDefault<T>(this XElement element, string attributeName)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            XAttribute attr = element.Attribute(attributeName);
            string result = (attr != null) ? attr.Value : "";

            //Cast ConvertFromString(string text) : object to (T)
            return (T)converter.ConvertFromString(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        public static void WhenNotNull(this XElement element, Action<XElement> action)
        {
            if (element != null)
                action(element);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="func"></param>
        public static T WhenNotNull2<T>(this XElement element, Func<XElement, T> func)
        {
            if (element != null)
                return func(element);
            return default(T);
        }

        public static string ValueWithoutChildren(this XElement element)
        {
            var textNode = element.Nodes().OfType<XText>().FirstOrDefault();
            return textNode == null ? "" : textNode.Value;
        }

        public static void RenameDescendants(this XElement element, string descendantName, string newDescendantName)
        {
            foreach (var nodeToRename in element.Descendants(descendantName))
            {
                nodeToRename.Name = newDescendantName;
            }
        }

        public static void AddAttributeToDescendants(this XElement element, string descendantName,
            string attributeName, string attributeValue)
        {
            foreach (var descendant in element.Descendants(descendantName))
            {
                if (descendant.Attribute(attributeName) == null)
                {
                    descendant.Add(new XAttribute(attributeName, attributeValue));
                }
            }
        }


        public static XElement ConvertForeignTextTypeToHtml(this XElement element)
        {
            if (element != null)
            {
                foreach (var typefaceElement in element.Descendants("typeface"))
                {
                    var typeFaceValueAttribute = typefaceElement.Attribute("typeface-value");
                    if (typeFaceValueAttribute == null) continue;
                    typefaceElement.RemoveAttributes();
                    typefaceElement.Add(new XAttribute("class", typeFaceValueAttribute.Value));
                    typefaceElement.Name = "span";
                }
                element.RenameDescendants("newline", "br");
                element.RenameDescendants("subscript", "sub");
                element.RenameDescendants("superscript", "sup");

                foreach (var refElement in element.Descendants("inline-reference"))
                {
                    var bookRef = refElement.AttributeValueOrDefault("book-ref");
                    var nodeRef = refElement.AttributeValueOrDefault("node-ref");
                    refElement.RemoveAttributes();
                    refElement.Add(new XAttribute("class", "inline-reference"));
                    refElement.Add(new XAttribute("data-book-ref", bookRef));
                    refElement.Add(new XAttribute("data-node-ref", nodeRef));
                    refElement.Name = "span";
                }

            }
            return element;
        }

        /// <summary>
        /// Converts all presentation XML in the text-type schema elements to valid HTML. E.g. convert
        /// elements such as subscript to HTML equivalent sub
        /// </summary>
        public static XElement ConvertTextTypeToHtml(this XElement element)
        {
            if (element != null)
            {
                element.RenameDescendants("subscript", "sub");
                element.RenameDescendants("superscript", "sup");
                element.RenameDescendants("focus", "em");

                element.ConvertDescendantsToHtmlMoveNameToClass("integer", "span");
                foreach (var numeratorElement in element.Elements("numerator"))
                {
                    numeratorElement.AddElementAfterSelf("span", "/", "oblique");
                }
                element.ConvertDescendantsToHtmlMoveNameToClass("numerator", "sup");
                element.ConvertDescendantsToHtmlMoveNameToClass("denominator", "sub");
                element.ConvertDescendantsToHtmlMoveNameToClass("spot-translation", "span");
                element.ConvertMark();

                foreach (var xElement in element.Elements("reg-tm"))
                {
                    xElement.AddFirst(new XElement("span", new XAttribute("class", "reg-tm-symbol"), "®"));
                }

                element.ConvertDescendantsToHtmlMoveNameToClass("reg-tm", "span");

                element.ConvertTable();
                element.ConvertInfo();
                element.ConvertGram();
            }

            return element;
        }

        /// <summary>
        /// Find all descendants of <paramref name="element"/> named <paramref name="descendantName"/>. Replace each of
        /// them with an HTML-valid element by moving the current element name to the class attribute and rename the
        /// element itself to <paramref name="htmlElementName"/>
        /// </summary>
        public static void ConvertDescendantsToHtmlMoveNameToClass(this XElement element, string descendantName,
            string htmlElementName)
        {
            element.AddAttributeToDescendants(descendantName, "class", descendantName);
            element.RenameDescendants(descendantName, htmlElementName);
        }

        public static void AddElementAfterSelf(this XElement element, string htmlElementName, string value, string cssClass = null)
        {
            cssClass = cssClass ?? string.Format("{0}-{1}", element.Name.LocalName, "after");
            element.AddAfterSelf(new XElement(htmlElementName, new XAttribute("class", cssClass), value));
        }

        public static void ConvertGram(this XElement element)
        {
            var gramsToRemove = new List<XElement>();

            foreach (var descendant in element.Descendants("gram"))
            {
                //gram elements with no text contents should not be shown 
                //(as they have no inflections to table-presentation for)
                if (string.IsNullOrEmpty(descendant.InnerXmlOrDefault().Trim()))
                {
                    gramsToRemove.Add(descendant);
                }
                else
                {
                    var lemmaId = descendant.AttributeValueOrDefault("lemmaid-ref");
                    var lemmaLang = ""; // LanguageIsoCodeMap.GetIsoCode(descendant.AttributeValueOrDefault("lemma-lang"));
                    var title = WebUtility.HtmlEncode(descendant.ConvertTextTypeToHtml().InnerXmlOrDefault());
                    descendant.RemoveAll();
                    descendant.Add(new XAttribute("data-lemmaid-ref", lemmaId));
                    descendant.Add(new XAttribute("data-lemma-lang", lemmaLang));
                    descendant.Add(new XAttribute("data-title", title));
                    descendant.Add(new XAttribute("class", "resultpage-center-item-list-item-grammar-icon"));
                }
            }
            foreach (var gram in gramsToRemove)
            {
                gram.Remove();
            }

            element.RenameDescendants("gram", "i");

        }

        public static void ConvertInfo(this XElement element)
        {
            element.AddAttributeToDescendants("info", "class", "resultpage-center-item-list-item-info-icon");
            foreach (var descendant in element.Descendants("info"))
            {
                descendant.Add(new XAttribute("data-title", WebUtility.HtmlEncode(descendant.ConvertTextTypeToHtml().InnerXmlOrDefault())));
                descendant.RemoveNodes();
            }
            element.RenameDescendants("info", "i");
        }



        public static void ConvertMark(this XElement element)
        {
            foreach (var markElement in element.Descendants("mark"))
            {
                var beforeAttribute = markElement.Attribute("tegn-før-gyl");
                if (beforeAttribute != null)
                {
                    var elementName = GetMarkSymbolElementNameByTypeAttribute(markElement, "tegn-før-typ");

                    markElement.AddFirst(new XElement(elementName, new XAttribute("class", "mark-symbol mark-before"),
                        beforeAttribute.Value));
                }


                var afterAttribute = markElement.Attribute("tegn-efter-gyl");
                if (afterAttribute != null)
                {
                    var elementName = GetMarkSymbolElementNameByTypeAttribute(markElement, "tegn-efter-typ");
                    markElement.Add(new XElement(elementName, new XAttribute("class", "mark-symbol mark-after"),
                        afterAttribute.Value));
                }
                markElement.RemoveAttributes();
            }
        }

        private static string GetMarkSymbolElementNameByTypeAttribute(XElement markElement, string typeAttributeName)
        {
            var typeAttribValue = markElement.AttributeValueOrDefault(typeAttributeName);
            switch (typeAttribValue)
            {
                case "subscript":
                    return "sub";
                case "superscript":
                    return "sup";
                default:
                    return "span";
            }
        }

        private static void ConvertTable(this XElement element)
        {
            foreach (var cellElement in element.Descendants("cell").Union(element.Descendants("cellspan")))
            {
                XAttribute cellTypeAttribute = cellElement.Attribute("cell-type");
                XAttribute cellCountAttribute = cellElement.Attribute("cellcount");
                cellElement.RemoveAttributes();

                if (cellTypeAttribute != null)
                {
                    string cellType = cellTypeAttribute.Value;

                    if (cellType.Length > 0)
                    {
                        cellType = "cell-type-" + cellType;
                        cellElement.SetAttributeValue("class", cellType);
                    }
                }

                if (cellCountAttribute != null)
                {
                    string cellCount = cellCountAttribute.Value;

                    if (cellCount.Length > 0)
                    {
                        cellElement.SetAttributeValue("colspan", cellCount);
                    }
                }
            }

            element.RenameDescendants("row", "tr");
            element.RenameDescendants("cell", "td");
            element.RenameDescendants("cellspan", "td");
        }
    }
}