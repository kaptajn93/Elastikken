using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaRowCellElement
    {

        public LemmaRowCellElement()
        {
        }

        public LemmaRowCellElement(XElement rowCellXElement) : this()
        {
                TpCellTyper = rowCellXElement.AttributeValueOrDefault("cell-type");
                TpCellName = rowCellXElement.Value;
           
        }

        public string TpCellTyper { get; set; }
        public string TpCellName { get; set; }
    }
}
