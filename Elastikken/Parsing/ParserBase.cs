using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Elastikken.Parsing
{
    public interface IParserBase<T>
    {    
        IEnumerable<T> ParseXml(IEnumerable<string> xmlFilesToImport);
    }

    public abstract class ParserBase<T> : IParserBase<T>
    {
        protected abstract string NodeName { get; }


        public abstract IEnumerable<T> ParseXml(IEnumerable<string> xmlFilesToImport);
        public abstract IEnumerable<T> ParseXml(IEnumerable<XElement> elements);


        protected IEnumerable<XElement> StreamElements(IEnumerable<string> xmlFilesToImport)
        {
            foreach (var filePath in xmlFilesToImport)
            {
                var filename = Path.GetFileName(filePath);
                var nodeIndex = 0;
                using (var reader = GetXmlReader(filePath))
                {
                    try
                    {
                        reader.MoveToContent();
                    }
                    catch (Exception exception)
                    {
                        throw new ParsingException(filePath, "Could not move to XML reader contents", nodeIndex, exception);
                    }
                    while (true)
                    {
                        XElement element;
                        try
                        {
                            if (reader.NodeType != XmlNodeType.Element || reader.Name != NodeName)
                            {
                                if (reader.Read()) continue;
                                break;
                            }
                            nodeIndex++;
                            element = (XElement)XNode.ReadFrom(reader);

                            // add a custom filename-attribute so that we are able to track from which file an entry/document
                            // was imported (helpful in case of validation errors) 
                            element.SetAttributeValue("filename", filename);
                        }
                        catch (Exception exception)
                        {
                            throw new ParsingException(filePath, nodeIndex, exception);
                        }
                        yield return element;
                    }
                }
            }
        }

        protected virtual XmlReader GetXmlReader(string fileName)
        {
            return XmlReader.Create(fileName);
        }

        protected virtual XElement LoadFile(string file)
        {
            return XElement.Load(file);
        }

        
    }
}
