using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Elastikken.Parsing
{
    public class EntryAnnotatedTargetElement
    {
        public EntryAnnotatedTargetElement()
        {
            Examples = new List<string>();
                        
        }



        public EntryAnnotatedTargetElement(XElement anTXElement)
        {

         anTXElement.Element("translation").WhenNotNull(trans =>
           {
               Translation = trans.InnerXmlOrDefault();

               #region ---if you want Gram---
               //    Translation = trans.InnerXmlOrDefault();
               //trans.Element("gram").WhenNotNull(g =>
               //{
               //    TranslationGram = g.InnerXmlOrDefault();
               //    TranslationGramLemmaPos = g.AttributeValueOrDefault("lemma-pos");
               //    TranslationGramLemmaRef = g.AttributeValueOrDefault("lemma-ref");
               //    TranslationGramLemmaDescriptionRef = g.AttributeValueOrDefault("lemma-description-ref");
               //    TranslationGramLemmaLang = /*LanguageIsoCodeMap.GetIsoCode(*/ g.AttributeValueOrDefault("lemma-lang");
               //    TranslationGramLemmaIdRef = g.AttributeValueOrDefault("lemmaid-ref");
               //    Translation = g

               //}); 
               #endregion
           }
        );
            Examples = anTXElement
                .Elements("example")
                .Select(e => e
                    .ConvertTextTypeToHtml()
                    .InnerXmlOrDefault()
                    .Trim())
                .ToList();


        }

        public string Translation { get; set; }

        public List<string> Examples { get; set; }

        public object TranslationGramLemmaIdRef { get; set; }

        public object TranslationGramLemmaLang { get; set; }

        public object TranslationGramLemmaDescriptionRef { get; set; }

        public object TranslationGramLemmaRef { get; set; }

        public object TranslationGramLemmaPos { get; set; }

        public object TranslationGram { get; set; }
    }
}
 