using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaTableRowElement
    {

        public LemmaTableRowElement()
        {
        }

        public LemmaTableRowElement(XElement tableRowXElement) : this()
        {
            LemmaRowCells = tableRowXElement.ChildXElementsOfExtensionType("cell", x => new LemmaRowCellElement(x));
        }

        public IList<LemmaRowCellElement> LemmaRowCells { get; set; }
    }
}
