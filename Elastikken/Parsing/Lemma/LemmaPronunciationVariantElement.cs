using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaPronunciationVariantElement
    {
        public LemmaPronunciationVariantElement()
        {
        }

        public LemmaPronunciationVariantElement(XElement pVarXElement) : this()
        {
          
                LemmaVariantDescription = pVarXElement.ChildXElementsOfExtensionType("variant-description",
                    x => new LemmaVariantDescriptionElement(x));
                LemmaPronunciation = pVarXElement.ChildXElementsOfExtensionType("pronunciation",
                    x => new LemmaVarPronunciationElement(x));
              
        }

        public IList<LemmaVarPronunciationElement> LemmaPronunciation { get; set; }

        public IList<LemmaVariantDescriptionElement> LemmaVariantDescription { get; set; }
    }
}
