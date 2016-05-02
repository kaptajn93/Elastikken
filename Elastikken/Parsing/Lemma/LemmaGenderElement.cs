using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaGenderElement
    {
        public LemmaGenderElement()
        {
        }

        public LemmaGenderElement(XElement genXElement) : this()
        {
            genXElement.Element("gender").WhenNotNull(gen =>
            {
                GenNameDan = gen.AttributeValueOrDefault("name-dan");
                GenNameGyl = gen.AttributeValueOrDefault("name-gyl");
                GenNameEng = gen.AttributeValueOrDefault("name-eng");
                GenNameLat = gen.AttributeValueOrDefault("name-lat");
                GenShortNameDan = gen.AttributeValueOrDefault("short-name-dan");
                GenShortNameGyl = gen.AttributeValueOrDefault("short-name-gyl");
                GenShortNameEng = gen.AttributeValueOrDefault("short-name-eng");
                GenShortNameLat = gen.AttributeValueOrDefault("short-name-lat");
            });
        }
        #region ---Gender--
        public string GenShortNameLat { get; set; }

        public string GenShortNameEng { get; set; }

        public string GenShortNameGyl { get; set; }

        public string GenShortNameDan { get; set; }

        public string GenNameLat { get; set; }

        public string GenNameEng { get; set; }

        public string GenNameGyl { get; set; }

        public string GenNameDan { get; set; }
        #endregion
    }
}
