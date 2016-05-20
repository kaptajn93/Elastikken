using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaAccessoryDataElement
    {
        public LemmaAccessoryDataElement()
        {
        }

        public LemmaAccessoryDataElement(XElement accessoryDataXElement) : this()
        {
                AcDataCatDan = accessoryDataXElement.AttributeValueOrDefault("accessory-category-dan");
                AcDataCatEng = accessoryDataXElement.AttributeValueOrDefault("accessory-category-eng");
                AccessoryDataRefs = accessoryDataXElement.ChildXElementsOfExtensionType("lemma-ref",
                    x => new LemmaAccessoryDataRefElement(x));
        }

        public IList<LemmaAccessoryDataRefElement> AccessoryDataRefs { get; set; }


        public string AcDataCatEng { get; set; }

        public string AcDataCatDan { get; set; }
    }
}
