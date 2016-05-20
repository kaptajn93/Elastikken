using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaAbbRefElement
    {
        public LemmaAbbRefElement()
        {
        }

        public LemmaAbbRefElement(XElement refXElement) : this()
        {
            AbbLemmaRef = refXElement.AttributeValueOrDefault("lemma-pos");
            AbbLemmaPos = refXElement.AttributeValueOrDefault("lemma-ref");
            AbbLemmaDescRef =refXElement.AttributeValueOrDefault("lemma-description-ref");
        }

        public string AbbLemmaDescRef { get; set; }

        public string AbbLemmaPos { get; set; }

        public string AbbLemmaRef { get; set; }
    }
}
