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
            LemmaInflectedForms = new List<LemmaInflectedFormElement>();
        }


        public LemmaSearchableParadigmElement(XElement searchXElement) : this()
        {

            LemmaInflectedForms = searchXElement.ChildXElementsOfExtensionType("inflected-form",x => new LemmaInflectedFormElement(x));
            
            //searchXElement.Element("inflected-form").WhenNotNull(se =>
            //{
            //    LemmaInflectedForm = se.ChildXElementsOfExtensionType("inflected-form",
            //     x => new LemmaInflectedFormElement(x));
            //});
           


        }

        public IList<LemmaInflectedFormElement> LemmaInflectedForms { get; set; }


    }
}
