using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using NLog;

namespace Elastikken.Parsing
{
    public class SolrConfig
    {
        // Parser
        public class SolrParser : ParserBase<SolrConfigDocument>
        {
            public override IEnumerable<SolrConfigDocument> ParseXml(IEnumerable<XElement> elements)
            {
                try
                {
                    var solrs = from solrXElement in elements
                        let solr = new SolrElement(solrXElement)
                        let book = new SolrBookElement(solrXElement)

                        where solr != null

                        let filename = solrXElement.Attributes("filename").FirstOrDefault()

                        select new SolrConfigDocument(Guid.NewGuid().ToString())
                        {
                            Books = new Books
                            {
                                Book = solr.Book?.Select(b =>
                                    new Book
                                    {
                                        BookId = book.BookId,
                                        PrioritizeBook = book.PrioritizeBook
                                    }).ToList()
                            }
                        };
                    return solrs;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
            public override IEnumerable<SolrConfigDocument> ParseXml(IEnumerable<string> xmlFilesToImport)
            {
                var elements = StreamElements(xmlFilesToImport);
                return ParseXml(elements);
            }
            protected override string NodeName => "Books";
        }

        //  Element-------------------------------------------------------------------

        public class SolrElement
        {
            public SolrElement()
            {
            }
            public SolrElement(XElement solrElement) : this()
            {
                ParseSolr(solrElement);
            }

            private void ParseSolr(XElement solrElement)
            {
                Book = solrElement.ChildXElementsOfExtensionType("Book", b => new SolrBookElement(b));
            }
            public IList<SolrBookElement> Book { get; set; }
        }
        //----------------------------------------------------------------------------------
        public class SolrBookElement
        {
            public SolrBookElement()
            {
            }

            public SolrBookElement(XElement bookXElement) : this()
            {
                bookXElement.Element("Book").WhenNotNull(book =>
                {
                    BookId = book.ChildElementValueOrDefault<string>("Id");
                    PrioritizeBook = book.ChildElementValueOrDefault<string>("Priority");
                });
            }

            public string PrioritizeBook { get; set; }
            public string BookId { get; set; }
        }


        //  Document----------------------------------------------------------------------

        public class SolrConfigDocument
        {
            public SolrConfigDocument()
            {
                Books = new Books();
            }
            public SolrConfigDocument(string id) : this()
            {
            }

            public Books Books { get; set; }
        }
        public class Books
        {
            public IList<Book> Book { get; set; }
        }

        public class Book
        {
            public string PrioritizeBook { get; set; }
            public string BookId { get; set; }
        }
    }
}
