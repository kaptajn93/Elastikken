using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    public class LemmaIllFileElement
    {
        public LemmaIllFileElement()
        {
        }

        public LemmaIllFileElement(XElement illXElement) : this()
        {
            LemmaIllFileRef = illXElement.AttributeValueOrDefault("ill-file-ref");
            LemmaIllFileType = illXElement.AttributeValueOrDefault("ill-type");
        }

        public string LemmaIllFileType { get; set; }

        public string LemmaIllFileRef { get; set; }
    }
}
