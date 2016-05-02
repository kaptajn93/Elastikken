using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaVariantsElement
    {
        public LemmaVariantsElement()
        {
        }

        public LemmaVariantsElement(XElement variantXElement) : this()
        {
            variantXElement.Element("variants").WhenNotNull(va =>
            {
                va.Element("lemma-ref").WhenNotNull(lr =>
                {
                    LemRefLemPos = lr.AttributeValueOrDefault("lemma-pos");
                    LemRefLemRef = lr.AttributeValueOrDefault("lemma-ref");
                });
            });
           
        }
        public string LemRefLemRef { get; set; }
        public string LemRefLemPos { get; set; }
    }
}
