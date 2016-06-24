using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Entry
{
    public class PrioritizeWhenLemmaElement
    {
        public PrioritizeWhenLemmaElement()
        {
        }

        public PrioritizeWhenLemmaElement(XElement priXElement) : this()
        {
           
                PrioritizeWhenLemmaLemmaPos = priXElement.AttributeValueOrDefault("lemma-pos");
                PrioritizeWhenLemmaLemmaRef = priXElement.AttributeValueOrDefault("lemma-ref");
                PrioritizeWhenLemmaLemmaDescription = priXElement.AttributeValueOrDefault("lemma-description-ref");
                PrioritizeWhenLemmaLemmaIdRef = priXElement.AttributeValueOrDefault("lemmaid-ref");

        }

        public string PrioritizeWhenLemmaLemmaIdRef { get; set; }
        public string PrioritizeWhenLemmaLemmaDescription { get; set; }
        public string PrioritizeWhenLemmaLemmaRef { get; set; }
        public string PrioritizeWhenLemmaLemmaPos { get; set; }
    }
}
