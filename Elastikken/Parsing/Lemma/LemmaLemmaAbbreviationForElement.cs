using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaLemmaAbbreviationForElement
    {
        public LemmaLemmaAbbreviationForElement()
        {
        }

        public LemmaLemmaAbbreviationForElement(XElement abbXElement) : this()
        {
            abbXElement.Element("abbreviation-for").WhenNotNull(va =>
            {
                BlindRef = va.ChildXElementsOfExtensionType("blind-ref",
                    x => new LemmaAbbBlindRefElement(x));
                LemmaAbbRef = va.ChildXElementsOfExtensionType("lemma-ref",
                    x => new LemmaAbbRefElement(x));

            });
        }

        public IList<LemmaAbbBlindRefElement> BlindRef { get; set; }

        public IList<LemmaAbbRefElement> LemmaAbbRef { get; set; }

       
    }
}
