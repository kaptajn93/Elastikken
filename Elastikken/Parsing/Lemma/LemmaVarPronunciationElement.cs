using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaVarPronunciationElement
    {
        public LemmaVarPronunciationElement()
        {
        }

        public LemmaVarPronunciationElement(XElement varpXElement) : this()
        {
            varpXElement.Element("sound-file").WhenNotNull(sf =>
            {
                 LemmaSoundFile = sf.AttributeValueOrDefault("sound-file-ref");
            });
           
            LemmaIPA = varpXElement.ChildElementValueOrDefault<string>("IPA");
            LemmaIPApart = varpXElement.ChildElementValueOrDefault<string>("IPA-part");
            LemmaStress = varpXElement.ChildElementValueOrDefault<string>("stress");

        }

        public string LemmaStress { get; set; }

        public string LemmaIPApart { get; set; }

        public string LemmaIPA { get; set; }

        public string LemmaSoundFile { get; set; }
    }
}
