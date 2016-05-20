using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaLangVariantElement
    {
        public LemmaLangVariantElement()
        {
        }

        public LemmaLangVariantElement(XElement lVarXElement) : this()
        {
            LemmaLangVar = lVarXElement.Value;

        }

        public string LemmaLangVar { get; set; }
    }
}
