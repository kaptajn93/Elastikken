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
            infCatXElement.Element("inflection-category").WhenNotNull(icat =>
            {
                InfCatNameDan = icat.AttributeValueOrDefault("name-dan");
                InfCatNameGyl = icat.AttributeValueOrDefault("name-gyl");
                InfCatNameEng = icat.AttributeValueOrDefault("name-eng");
                InfCatNameLat = icat.AttributeValueOrDefault("name-lat");
                InfCatShortNameDan = icat.AttributeValueOrDefault("short-name-dan");
                InfCatShortNameGyl = icat.AttributeValueOrDefault("short-name-gyl");
                InfCatShortNameEng = icat.AttributeValueOrDefault("short-name-eng");
                InfCatShortNameLat = icat.AttributeValueOrDefault("short-name-lat");
            });


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
