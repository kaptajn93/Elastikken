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
    public class EntryParserTests : IDisposable
    {
        EntryParser _parser;
        ElasticManager _manager;

        #region --- TEST DATA: ---

        // <summary>
        /// This XML is constructed for testing, and not a real sample
        /// </summary>
        private const string Xml =
            "<entry>" +
            "<id>" +
            "<book full-label=\"GYLDENDALS RØDE\" short-label=\"RØD\" icon=\"gyldendal-logo.jpg\">daen-rød</book>" +
            "<id-lemma lemma-pos=\"vb.\" lemma-ref=\"bage\" lemma-description-ref=\"om bagværk\" lemmaid-ref=\"dale0010415\"/>" +
            "<id-entry> daenrød0004332 </id-entry>" +
            "</id>" +
            "<head show=\"false\">" + // Have not found any show attribute in real samples
            "<headword>bage<inner-xml>blarf</inner-xml> sample</headword>" +
            "<reg-tm/>" +
            "<about-headword>ordet deles ikke</about-headword>" +
            "<inflected-form>pl. af</inflected-form>" +
            "<pos name-dan=\"udsagnsord\" name-gyl=\"verbum\" name-eng=\"verb\" name-lat=\"verbum\" short-name-dan=\"uo.\" short-name-gyl=\"vb.\" short-name-eng=\"v.\" short-name-lat=\"vb.\"/>" +
            "<about-pos name-dan=\"flertal\" name-gyl=\"pluralis\" name-eng=\"plural\" name-lat=\"pluralis\" short-name-dan=\"fl.\" short-name-gyl=\"pl.\" short-name-eng=\"pl.\" short-name-lat=\"pl.\"/>" +
            "<inflection>" +
            "<compact-presentation>pos.: islandsk, (itk.) islandsk, (bf. + pl.) islandske</compact-presentation>" +
            "</inflection>" +
            "<variant-form>" +
            "<description>fakedescr.</description>" +
            "<variant>allergy drugs</variant>" +
            "<reg-tm/>" +
            "</variant-form>" +
            "<abbreviation-for>asymmetric digital <subscript>fake*</subscript> subscriber line</abbreviation-for>" +
            "<abbreviated>dB</abbreviated>" +
            "<shortform-for>cannot<test>test</test></shortform-for>" +
            "<description>head<test>description</test></description>" +
            "</head>" +
            "<body target-node-id=\"id-787878\">" +
            "<info>Retskrivningsordbogen indeholder oplysninger om ords stavning og bøjning</info>" +
            "<relations name-dan=\"synonymer\" name-gyl=\"synonymer\" name-eng=\"synonyms\" name-lat=\"synonymer\" short-name-dan=\"syn.\" short-name-gyl=\"syn.\" short-name-eng=\"syn.\" short-name-lat=\"syn.\">" +
            "<annotated-relation>" +
            "<reference book-ref=\"dan-ret\" node-ref=\"100065503_AAC\">" +
            "<headword-ref>sidenhen</headword-ref>" +
            "</reference>" +
            "</annotated-relation>" +
            "</relations>" +
            "<description>mus.</description>" +
            "<box name-dan=\"oprindelse\" name-gyl=\"etymologi\" name-eng=\"etymology\" name-lat=\"etymologi\" short-name-dan=\"opr.\" short-name-gyl=\"etym.\" short-name-eng=\"etym.\" short-name-lat=\"etym.\">" +
            "<text>lat. <focus>litteral</focus> + <focus>-isme</focus></text>" +
            "</box>" +
            "<illustration file=\"0192806483.international-paper-sizes.2.jpg\"/>" +
            "<productive-description>prod-desc.</productive-description>" +
            "<reference reference-type=\"se også \" book-ref=\"daen-rød\" node-ref=\"100002673_ACC\">" +
            "<headword-ref>bagende</headword-ref>" +
            "</reference>" +
            "<sense target-node-id=\"454545\">" +
            "<subsense>" +
            "<target-group>" +
            "<annotated-target>" +
            "<translation>bake <gram lemma-pos=\"vb.\" lemma-ref=\"bake\" lemma-lang=\"eng\">bøjes: bake; baked; baked</gram>" +
            "</translation>" +
            "<example>bread</example>" +
            "<example>cakes</example>" +
            "<example>we bake every Monday</example>" +
            "</annotated-target>" +
            "</target-group>" +
            "</subsense>" +
            "</sense>" +
            "<sense>" +
            "<description>andengenerationsindvandrer</description>" +
            "<subsense>" +
            "<target-group>" +
            "<annotated-target>" +
            "<translation>second-generation immigrant</translation>" +
            "</annotated-target>" +
            "</target-group>" +
            "</subsense>" +
            "</sense>" +
            "<sense target-node-id=\"545454\">" +
            "<description>gymnasieelev</description>" +
            "<subsense>" +
            "<target-group>" +
            "<annotated-target>" +
            "<target-language-definition>pupil in the 2. class of the gymnasium</target-language-definition>" +
            "</annotated-target>" +
            "</target-group>" +
            "</subsense>" +
            "</sense>" +
            "</body>" +
            "</entry>";


        #endregion

        public EntryParserTests()
        {
            _parser = new EntryParser();
            _manager = new ElasticManager();
        }

        [Fact]
        public void CanParseIdEntry()
        {
            // Arrange
            var element = XElement.Parse(Xml);
            var elements = new List<XElement> { element };

            // Act
            var documents = _parser.ParseXml(elements);
            var entry = documents.FirstOrDefault();

            // Asssert:
            var expected = "daenrød0004332";

            Assert.NotNull(entry);
            Assert.Equal(expected, entry.IdEntry);
        }

        [Fact]
        public void CanParseIdBook()
        {
            // Arrange
            var element = XElement.Parse(Xml);
            var elements = new List<XElement> { element };

            // Act
            var documents = _parser.ParseXml(elements);
            var entry = documents.FirstOrDefault();

            // Asssert:
            var expected = "daen-rød";

            Assert.NotNull(entry);
            Assert.Equal(expected, entry.IdBook);
        }

        [Fact]
        public void CanParseHeadWord()
        {
            // Arrange
            var element = XElement.Parse(Xml);
            var elements = new List<XElement> { element };

            // Act
            var documents = _parser.ParseXml(elements);
            var entry = documents.FirstOrDefault();

            // Asssert:
            var expected = "bage";

            Assert.NotNull(entry);
            Assert.Equal(expected, entry.HeadWord);
        }

        [Fact]
        public void CanParseHeadPSNG()
        {
            // Arrange
            var element = XElement.Parse(Xml);
            var elements = new List<XElement> { element };

            // Act
            var documents = _parser.ParseXml(elements);
            var entry = documents.FirstOrDefault();

            // Asssert:
            var expected = "vb.";

            Assert.NotNull(entry);
            Assert.Equal(expected, entry.HeadPosShortNameGyl);
        }

        [Fact]
        public void CanParseEntryAndGetSensesByBlob()
        {
            // Arrange
            var element = XElement.Parse(Xml);
            var elements = new List<XElement> { element };
            // Act
            var documents = _parser.ParseXml(elements);
            var entry = documents.FirstOrDefault();
            var entryElement = entry.GetDeserializedBlobAndDeleteBlob();

            // Asssert:
            var expected = "daenrød0004332";
            Assert.NotNull(entryElement);
            Assert.Equal(expected, entryElement.Id);

            Assert.Equal(2, entryElement.BodySenses.Count);
        }

        [Fact]
        public void CanParseIdLemmaFromBlob()
        {
            // Arrange
            var element = XElement.Parse(Xml);
            var elements = new List<XElement> { element };
            // Act
            var documents = _parser.ParseXml(elements);
            var entry = documents.FirstOrDefault();
            var entryElement = entry.GetDeserializedBlobAndDeleteBlob();
            var lemmaPos = entryElement.IdLemmaLemmaPos;
            var lemmaRef = entryElement.IdLemmaLemmaRef;
            var lemmaDescRef = entryElement.IdLemmaLemmaDescriptionRef;
            var lemmaIdRef = entryElement.IdLemmaLemmaIdRef;
            // Asssert:
            var pos = "vb.";
            var lemRef = "bage";
            var descRef = "om bagværk";
            var idRef = "dale0010415";
            Assert.NotNull(entry);
            Assert.NotNull(entryElement);
            Assert.Equal(pos, lemmaPos);
            Assert.Equal(lemRef, lemmaRef);
            Assert.Equal(descRef, lemmaDescRef);
            Assert.Equal(idRef, lemmaIdRef);
        }

        #region ---Test node counts---

        [Fact]
        public void CanParseSenseCount()
        {
            // Arrange
            var element = XElement.Parse(Xml);
            var elements = new List<XElement> { element };

            // Act
            var documents = _parser.ParseXml(elements);
            var entry = documents.FirstOrDefault();

            // Asssert:
            var expected = 3;

            Assert.NotNull(entry);
            Assert.Equal(expected, entry.SenseCount);
        }

        [Fact]
        public void CanParseRefCount()
        {
            // Arrange
            var element = XElement.Parse(Xml);
            var elements = new List<XElement> { element };

            // Act
            var documents = _parser.ParseXml(elements);
            var entry = documents.FirstOrDefault();

            // Asssert:
            var expected = "bagende";

            Assert.NotNull(entry);
            // Assert.Equal(expected, entry.BodyHeadWordRef);
        }

        [Fact]
        public void CanParseSubsenseCount()
        {
            // Arrange
            var element = XElement.Parse(Xml);
            var elements = new List<XElement> { element };

            // Act
            var documents = _parser.ParseXml(elements);
            var entry = documents.FirstOrDefault();
            var entryElm = entry.GetDeserializedBlobAndDeleteBlob();
            var sense = entryElm.BodySenses;

            // Asssert:
            var expected = 3;
            var expectedCount = 0;
            for (int i = 0; i < sense.Count; i++)
            {
                expectedCount += sense[i].Subsense.Count;
            }

            Assert.NotNull(entry);
            Assert.Equal(expected, expectedCount);
        }

        [Fact]
        public void CanParseSenseExamplesCount()
        {
            // Arrange
            var element = XElement.Parse(Xml);
            var elements = new List<XElement> { element };

            // Act
            var documents = _parser.ParseXml(elements);
            var entry = documents.FirstOrDefault();
            var entryElm = entry.GetDeserializedBlobAndDeleteBlob();
            var sense = entryElm.BodySenses;

            // Asssert:
            var expected = 3;
            Assert.NotNull(entry);
            Assert.Equal(expected, sense[0].Subsense[0].TargetGroups[0].AnnotatedTargets[0].Examples.Count);
        }

        [Fact]
        public void CanParseSenseExamplesCountFromBlob()
        {
            // Arrange
            var element = XElement.Parse(Xml);
            var elements = new List<XElement> { element };

            // Act
            var documents = _parser.ParseXml(elements);
            var entry = documents.FirstOrDefault();
            var entryElement = entry.GetDeserializedBlobAndDeleteBlob();

            // Asssert:
            var expected = 3;
            Assert.NotNull(entry);
            Assert.NotNull(entryElement);
            Assert.Equal(expected,
                entryElement.BodySenses[0].Subsense[0].TargetGroups[0].AnnotatedTargets[0].Examples.Count);
        }

        #endregion


        #region ---Test each xml node---

        [Fact]
        public void CanParseSenseTargetNodeId()
        {
            // Arrange
            var senseXml = "<sense target-node-id=\"454545\">" +
                           "<subsense>" +
                           "</subsense>" +
                           "</sense>";
            // Act
            var element = XElement.Parse(senseXml);
            var senseElement = new EntrySenseElement(element);
            // Asssert:
            var expected = "454545";
            var subCount = 1;

            Assert.Equal(expected, senseElement.TargetNodeId);
            Assert.Equal(subCount, senseElement.Subsense.Count);
            Assert.Equal(1, senseElement.Subsense.Count);
        }

        [Fact]
        public void CanParseSubSenseWithTargetNodeId()
        {
            // Arrange
            var subXml =
                "<subsense target-node-id=\"505050\">" +
                "<target-group>" +
                "</target-group>" +
                "<target-group>" +
                "</target-group>" +
                "<target-group>" +
                "</target-group>" +
                "</subsense>";
            // Act
            var element = XElement.Parse(subXml);
            var subElement = new EntrySubsenseElement(element);
            // Asssert:
            var expected = "505050";
            var tarCount = 3;

            Assert.Equal(expected, subElement.TargetNodeId);
            Assert.Equal(tarCount, subElement.TargetGroups.Count);
        }

        [Fact]
        public void CanParseTargetNode()
        {
            // Arrange
            var targetXml =
                "<target-group>" +
                "<annotated-target>" +
                "</annotated-target>" +
                "<annotated-target>" +
                "</annotated-target>" +
                "<annotated-target>" +
                "</annotated-target>" +
                "</target-group>"
                ;
            // Act
            var element = XElement.Parse(targetXml);
            var tarElement = new EntryTargetGroupElement(element);
            // Asssert:
            var anoCount = 3;

            Assert.Equal(anoCount, tarElement.AnnotatedTargets.Count);
        }

        [Fact]
        public void CanParseAnotatedTargetNode()
        {
            // Arrange
            var anoXml =

                "<annotated-target>" +
                "<translation>bake<gram lemma-pos='vb.' lemma-ref='bake' lemma-lang='eng'>bøjes:bake;baked;baked</gram>" +
                "</translation>" +
                "<example> bread" +
                "</example>" +
                "<example> cake" +
                "</example>" +
                "<example >we bake every Monday" +
                "</example>" +
                "</annotated-target>"

                ;
            // Act
            var element = XElement.Parse(anoXml);
            var anoElement = new EntryAnnotatedTargetElement(element);
            // Asssert:
            var expCount = 3;
            var transWordCount = 4;

            Assert.Equal(expCount, anoElement.Examples.Count);
            Assert.Equal(transWordCount, anoElement.Translation.Length);
        }

        #endregion

        [Fact]
        public void CanAddParseXmlAndPostEntryListToElastic()
        {
            // Arrange
            var element = XElement.Parse(Xml);
            var elements = new List<XElement> { element };
            // Act
            var documents = _parser.ParseXml(elements);
            var entryDocuments = documents as IList<EntryDocument> ?? documents.ToList();

            var tryAddEntry = _manager.AddEntryData(entryDocuments, "da");

            Assert.NotNull(entryDocuments);
            Assert.True(tryAddEntry);
            Assert.True(tryAddEntry);

        }

        public string XmlReader()
        {
            XmlTextReader reader = new XmlTextReader("C:/Users/hsm/Documents/Visual Studio 2015/Projects/Elastikken/Elastikken/Parsing/EntryData.xml");
            string contents = "";
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    contents += "<" + reader.Name;
                    if (!reader.HasAttributes)
                        contents += ">";
                    else
                    {
                        for (int i = 0; i < reader.AttributeCount; i++)
                        {
                            reader.MoveToNextAttribute();
                            contents += " " + reader.Name + "='" + reader.Value + "'";
                        }
                        reader.MoveToElement();
                        if (reader.Name == "id-lemma" || reader.Name == "pos")
                            contents += "/>";
                        else
                        {
                            contents += ">";
                        }
                    }
                }
                if (reader.NodeType == XmlNodeType.Text)
                    contents += reader.Value + "";
                if (reader.NodeType == XmlNodeType.EndElement)
                    contents += "</" + reader.Name + ">";
            }
            return contents;
        }
        
        [Fact]
        public void CanAddParseXmlFileAndAddEntryToElastic()
        {

            var fileName =
                //"C:/Users/hsm/Documents/Visual Studio 2015/Projects/Elastikken/Elastikken/Parsing/EntryData.xml";
                "C:/Users/hsm/Desktop/Lemma fuldt feed/feed-fuldt-sommer14_Dansk/entry-dan-sko.xml";
            // Act
            var documents = _parser.ParseXml(new List<string> {fileName});
            var tryAddEntry = _manager.AddEntryData(documents.ToList(), "da");

            Assert.NotNull(documents);
            Assert.True(tryAddEntry);
            Assert.True(tryAddEntry);

        }

        public void Dispose()
        {

        }
    }
}
