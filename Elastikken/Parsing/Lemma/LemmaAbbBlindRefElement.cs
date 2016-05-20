using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaAbbBlindRefElement
    {
        public LemmaAbbBlindRefElement()
        {
        }

        public LemmaAbbBlindRefElement(XElement blindXElement) : this()
        {
            BlindRef = blindXElement.Value;
        }

        public string BlindRef { get; set; }
    }
}
