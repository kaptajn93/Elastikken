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
            tableRowXElement.Element("row").WhenNotNull(r =>
            {
                TpCellName = r.ChildElementValueOrDefault<string>("cell");
                tableRowXElement.Element("cell").WhenNotNull(c =>
                {
                    TpCellTyper = c.AttributeValueOrDefault("cell-type");
                });
            });


        }
        public string TpCellTyper { get; set; }
        public string TpCellName { get; set; }
    }
}
