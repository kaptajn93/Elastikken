using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaPronunciationAllElement
    {
        public LemmaPronunciationAllElement()
        {
        }

        public LemmaPronunciationAllElement(XElement proAllXElement) : this()
        {
            proAllXElement.Element("pronunciation-all").WhenNotNull(pro =>
            {
                LemmaPronunciationVariant = pro.ChildXElementsOfExtensionType("pronunciation-variant",
                    x => new LemmaPronunciationVariantElement(x));
            });
        }

        public IList<LemmaPronunciationVariantElement> LemmaPronunciationVariant { get; set; }
    }
}
