using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaElement
    {
        public LemmaElement()
        {

        }

        public LemmaElement(XElement lemmaElement) : this()
        {
            LemmaLanguage = lemmaElement.AttributeValueOrDefault("language");
            LemmaId = lemmaElement.AttributeValueOrDefault("lemid");
            LemmaOrtography = lemmaElement.ChildElementValueOrDefault<string>("ortography");

            ParsePos(lemmaElement);
            ParseGender(lemmaElement);
            ParseInflection(lemmaElement);

            ParseVariants(lemmaElement);

            MeFirstComp = lemmaElement.ChildElementValueOrDefault<string>("me-as-first-component");
            MeLastComp = lemmaElement.ChildElementValueOrDefault<string>("me-as-last-component");

            ParseAccessoryData(lemmaElement);

        }
        private void ParsePos(XElement lemmaElement)
        {
            LemmaPos = lemmaElement.ChildXElementsOfExtensionType("pos", x => new LemmaPosElement(x));
        }

        private void ParseGender(XElement lemmaElement)
        {
            LemmaGender = lemmaElement.ChildXElementsOfExtensionType("gender", x => new LemmaGenderElement(x));
        }

        private void ParseInflection(XElement lemmaElement)
        {
            LemmaInflection = lemmaElement.ChildXElementsOfExtensionType("inflection",
                x => new LemmaInflectionElement(x));
        }

        private void ParseVariants(XElement lemmaElement)
        {
            LemmaVariants = lemmaElement.ChildXElementsOfExtensionType("variants",
                x => new LemmaVariantsElement(x));
        }

        private void ParseAccessoryData(XElement lemmaElement)
        {
            LemmaAccessoryData = lemmaElement.ChildXElementsOfExtensionType("accessory-data",
                x => new LemmaAccessoryDataElement(x));
        }

        public string LemmaLanguage { get; set; }
        public string LemmaId { get; set; }
        public string LemmaOrtography { get; set; }
        public IList<LemmaPosElement> LemmaPos { get; set; }
        public IList<LemmaGenderElement> LemmaGender { get; set; }
        public IList<LemmaInflectionElement> LemmaInflection { get; set; }
        public IList<LemmaVariantsElement> LemmaVariants { get; set; }
        public string MeLastComp { get; set; }
        public string MeFirstComp { get; set; }
        public IList<LemmaAccessoryDataElement> LemmaAccessoryData { get; set; }
    }
}



