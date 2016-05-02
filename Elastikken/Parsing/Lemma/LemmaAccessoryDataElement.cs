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
            accessoryDataXElement.Element("accessory-data").WhenNotNull(ad =>
            {
                AcDataCatDan = ad.AttributeValueOrDefault("accessory-category-dan");
                AcDataCatEng = ad.AttributeValueOrDefault("accessory-category-eng");
                ad.Element("lemma-ref").WhenNotNull(alr =>
                {
                    AcDataRefLemPos = alr.AttributeValueOrDefault("lemma-pos");
                    AcDataRefLemRef = alr.AttributeValueOrDefault("lemma-ref");
                });
            });
        }
        public string AcDataRefLemRef { get; set; }

        public string AcDataRefLemPos { get; set; }

        public string AcDataCatEng { get; set; }

        public string AcDataCatDan { get; set; }
    }
}
