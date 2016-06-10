using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Xml;
using System.Xml.Linq;
using Elastikken.Parsing;
using Elastikken.Parsing.Lemma;
using Nest;
using Newtonsoft.Json;
using NLog;
using NLog.Fluent;
using Xunit;

namespace Elastikken.Tests
{
    public class LemmaParserTests : IDisposable
    {
        LemmaParser _parser;
        ElasticManager _manager;
        Logger logger;

        public LemmaParserTests()
        {
            _parser = new LemmaParser();
            _manager = new ElasticManager();
        }

        [Fact]
        public void CanAddParseXmlFileAndAddEntryToElastic()
        {

            var lemmaFile = "C:/Users/hsm/Desktop/Lemma fuldt feed/Test/Lemma.xml";
            var myData = "C:/Users/hsm/Documents/Visual Studio 2015/Projects/Elastikken/Elastikken/Parsing/LemmaData.xml";


            var fileName = lemmaFile;
                
            // Act

            var documents = _parser.ParseXml(new List<string> { fileName });
            var tryAddLemma = _manager.AddLemmaDocumentData(documents.ToList(), "da");
            Assert.NotNull(documents);

            Assert.True(tryAddLemma);
        }

        public void Dispose()
        {
        }

    }
}
