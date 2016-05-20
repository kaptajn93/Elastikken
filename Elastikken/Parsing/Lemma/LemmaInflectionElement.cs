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
            infXElement.Element("inflection").WhenNotNull(gen =>
            {
                LemmaSearchableParadigms = gen.ChildXElementsOfExtensionType("searchable-paradigm",
                    x => new LemmaSearchableParadigmElement(x));
                LemmaCompactPresentation = gen.ChildElementValueOrDefault<string>("compact-presentation");
                LemmaTablePresentations = gen.ChildXElementsOfExtensionType("table-presentation",
                    x => new LemmaTablePresElement(x));
            });
        }

        public IList<LemmaSearchableParadigmElement> LemmaSearchableParadigms { get; set; }
        public string LemmaCompactPresentation { get; set; }
        public IList<LemmaTablePresElement> LemmaTablePresentations { get; set; }
    }
}
