using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaInflectionElement
    {
        public LemmaInflectionElement()
        {
        }

        public LemmaInflectionElement(XElement infXElement) : this()
        {
            infXElement.Element("inflection").WhenNotNull(inf =>
            {
                LemmaSearchableParadigm = inf.ChildXElementsOfExtensionType("searchable-paradigm",
                 x => new LemmaSearchableParadigmElement(x));
                LemmaCompactPresentation = inf.ChildElementValueOrDefault<string>("compact-presentation");
                LemmaTablePresentation = infXElement.ChildXElementsOfExtensionType("table-presentation",
                x => new LemmaTablePresElement(x));
            });
        }

        public IList<LemmaSearchableParadigmElement> LemmaSearchableParadigm { get; set; }
        public string LemmaCompactPresentation { get; set; }
        public IList<LemmaTablePresElement> LemmaTablePresentation { get; set; }
    }
}
