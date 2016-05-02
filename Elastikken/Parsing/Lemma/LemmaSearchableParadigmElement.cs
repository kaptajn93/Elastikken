using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing.Lemma
{
    class LemmaSearchableParadigmElement
    {
        public LemmaSearchableParadigmElement()
        {
        }

        public LemmaSearchableParadigmElement(XElement searchXElement) : this()
        {
            searchXElement.Element("searchable-paradigm").WhenNotNull(search =>
            {
                LemmaInflectedForm = search.ChildXElementsOfExtensionType("inflected-form",
                 x => new LemmaInflectedFormElement(x));
            });
            
        }

        public IList<LemmaInflectedFormElement> LemmaInflectedForm { get; set; }

      
    }
}
