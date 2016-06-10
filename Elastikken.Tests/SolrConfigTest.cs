using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Xml;
using System.Xml.Linq;
using Elastikken.Parsing;
using Nest;
using Newtonsoft.Json;
using Xunit;

namespace Elastikken.Tests
{
    public class SolrConfigTest : IDisposable
    {
       SolrConfig.SolrParser _parser;

        public SolrConfigTest()
        {
            _parser = new SolrConfig.SolrParser();
        }

        [Fact]
        public void CanAddParseXmlFileAndAddEntryToElastic()
        {
            var fileName = "C:/Users/hsm/Desktop/Lemma fuldt feed/Test/Config.xml";
            // Act
            var documents = _parser.ParseXml(new List<string> {fileName});

            Assert.NotNull(documents);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

   
}
