using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaPosElement
    {
        public LemmaPosElement()
        {
        }

        public LemmaPosElement(XElement posXElement) : this()
        {
            posXElement.Element("pos").WhenNotNull(pos =>
            {
                PosNameDan = pos.AttributeValueOrDefault("name-dan");
                PosNameGyl = pos.AttributeValueOrDefault("name-gyl");
                PosNameEng = pos.AttributeValueOrDefault("name-eng");
                PosNameLat = pos.AttributeValueOrDefault("name-lat");
                PosShortNameDan = pos.AttributeValueOrDefault("short-name-dan");
                PosShortNameGyl = pos.AttributeValueOrDefault("short-name-gyl");
                PosShortNameEng = pos.AttributeValueOrDefault("short-name-eng");
                PosShortNameLat = pos.AttributeValueOrDefault("short-name-lat");

            });
        }

        public string PosShortNameEng { get; set; }

        public string PosShortNameGyl { get; set; }

        public string PosShortNameDan { get; set; }

        public string PosNameLat { get; set; }

        public string PosShortNameLat { get; set; }

        public string PosNameEng { get; set; }

        public string PosNameGyl { get; set; }

        public string PosNameDan { get; set; }

    }
}
