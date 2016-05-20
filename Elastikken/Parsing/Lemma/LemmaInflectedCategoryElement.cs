using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaInflectedCategoryElement
    {
        public LemmaInflectedCategoryElement()
        {
        }

        public LemmaInflectedCategoryElement(XElement infCatXElement) : this()
        {
           
                InfCatNameDan = infCatXElement.AttributeValueOrDefault("name-dan");
                InfCatNameGyl = infCatXElement.AttributeValueOrDefault("name-gyl");
                InfCatNameEng = infCatXElement.AttributeValueOrDefault("name-eng");
                InfCatNameLat = infCatXElement.AttributeValueOrDefault("name-lat");
                InfCatShortNameDan = infCatXElement.AttributeValueOrDefault("short-name-dan");
                InfCatShortNameGyl = infCatXElement.AttributeValueOrDefault("short-name-gyl");
                InfCatShortNameEng = infCatXElement.AttributeValueOrDefault("short-name-eng");
                InfCatShortNameLat = infCatXElement.AttributeValueOrDefault("short-name-lat");
        }
        #region ---Infected-form inf-Category---

        public string InfCatShortNameLat { get; set; }

        public string InfCatShortNameEng { get; set; }

        public string InfCatShortNameGyl { get; set; }

        public string InfCatShortNameDan { get; set; }

        public string InfCatNameLat { get; set; }

        public string InfCatNameEng { get; set; }

        public string InfCatNameGyl { get; set; }

        public string InfCatNameDan { get; set; }

        #endregion
    }
}
