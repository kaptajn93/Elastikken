using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaVariantDescriptionElement
    {
        public LemmaVariantDescriptionElement()
        {
        }

        public LemmaVariantDescriptionElement(XElement vadXElement) : this()
        {
            LemmaProductiveDescription = vadXElement.ChildElementValueOrDefault<string>("productive-description");
            LemmaTechLang = vadXElement.ChildElementValueOrDefault<string>("tech-lang");
            LemmaVarDescription = vadXElement.ChildElementValueOrDefault<string>("description");
            LemmaLangVariants = vadXElement.ChildXElementsOfExtensionType("lang-variant",
                   x => new LemmaLangVariantElement(x));
        }

        public IList<LemmaLangVariantElement> LemmaLangVariants { get; set; }

        public string LemmaVarDescription { get; set; }

        public string LemmaTechLang { get; set; }

        public string LemmaProductiveDescription { get; set; }
    }
}
