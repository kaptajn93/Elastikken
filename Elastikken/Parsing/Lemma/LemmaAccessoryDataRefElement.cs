using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaAccessoryDataRefElement
    {
        public LemmaAccessoryDataRefElement()
        {
        }

        public LemmaAccessoryDataRefElement(XElement AcDaRefXElement) : this()
        {
            AcDataRefLemPos = AcDaRefXElement.AttributeValueOrDefault("lemma-pos");
            AcDataRefLemRef = AcDaRefXElement.AttributeValueOrDefault("lemma-ref");
        }
        public string AcDataRefLemRef { get; set; }

        public string AcDataRefLemPos { get; set; }
    }
}
