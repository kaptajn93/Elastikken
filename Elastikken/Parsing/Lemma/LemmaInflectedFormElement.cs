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
            infFormXElement.Element("inflected-form").WhenNotNull(iflform =>
            {
                InflectedFormLeaveOut = iflform.AttributeValueOrDefault("leave-out");
                LemmaInflectedCategory = iflform.ChildXElementsOfExtensionType("inflection-category",
               x => new LemmaInflectedCategoryElement(x));
                InfFullForm = iflform.ChildElementValueOrDefault<string>("full-form");
                InfCompactForm = iflform.ChildElementValueOrDefault<string>("compact-form");
            });
        }

        public string InflectedFormLeaveOut { get; set; }
        public IList<LemmaInflectedCategoryElement> LemmaInflectedCategory { get; set; }
        public string InfFullForm { get; set; }
        public string InfCompactForm { get; set; }
    }
}
