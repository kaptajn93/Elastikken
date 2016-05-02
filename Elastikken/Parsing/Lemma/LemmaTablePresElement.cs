using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaTablePresElement
    {

        public LemmaTablePresElement()
        {
        }

        public LemmaTablePresElement(XElement tpXElement) : this()
        {
            tpXElement.Element("table-presentation").WhenNotNull(tp =>
            {
                 LemmaTableRow = tp.ChildXElementsOfExtensionType("row",
                x => new LemmaTableRowElement(x));
            });
           
        }

        public IList<LemmaTableRowElement> LemmaTableRow { get; set; }
    }
}
