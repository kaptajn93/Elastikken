using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaInflectedFormElement
    {
        public LemmaInflectedFormElement()
        {
        }

        public LemmaInflectedFormElement(XElement infFormXElement) : this()
        {
                        InflectedFormLeaveOut = infFormXElement.AttributeValueOrDefault("leave-out");
                        LemmaInflectedCategorys = infFormXElement.ChildXElementsOfExtensionType("inflection-category",
                            x => new LemmaInflectedCategoryElement(x));
                        InfFullForm = infFormXElement.ChildElementValueOrDefault<string>("full-form");
                        InfCompactForm = infFormXElement.ChildElementValueOrDefault<string>("compact-form");
                }

        public string InflectedFormLeaveOut { get; set; }
        public IList<LemmaInflectedCategoryElement> LemmaInflectedCategorys { get; set; }
        public string InfFullForm { get; set; }
        public string InfCompactForm { get; set; }
    }
}
