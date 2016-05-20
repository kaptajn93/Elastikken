using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    public class LemmaIllustrationElement
    {
        public LemmaIllustrationElement()
        {
            LemmaillFiles = new List<LemmaIllFileElement>();
        }

        public LemmaIllustrationElement(XElement iluAllXElement) : this()
        {
            iluAllXElement.Element("illustration").WhenNotNull(ilu =>
            {
                LemmaillFiles = ilu.ChildXElementsOfExtensionType("ill-file",
                    x => new LemmaIllFileElement(x));
            });
        }

        public IList<LemmaIllFileElement> LemmaillFiles { get; set; }
    }
}
