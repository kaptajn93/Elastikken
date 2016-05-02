using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Nest;

namespace Elastikken.Parsing
{
    public class EntryElement
    {

        public EntryElement()
        {
            BodySenses = new List<EntrySenseElement>();

        }

        public EntryElement(XElement entryElement) : this()
        {
            ParseId(entryElement);
            ParseHead(entryElement);
            ParseBody(entryElement);

        }

        private void ParseId(XElement entryElement)
        {
            entryElement.Element("id").WhenNotNull(id =>
            {
                Id = id.ChildElementValueOrDefault<string>("id-entry");
                BookId = id.ChildElementValueOrDefault<string>("book");

                id.Element("book").WhenNotNull(i =>
                {
                    //Icon = i.AttributeValueOrDefault("icon");
                    //FullLabel = i.AttributeValueOrDefault("full-label");
                    //ShortLabel = i.AttributeValueOrDefault("short-label");
                    //BookIllustrationType = i.AttributeValueOrDefault("ill-type");
                });

                // TODO: Handle multiple id-lemmas?
                id.Element("id-lemma").WhenNotNull(i =>
                {
                    //IdLemmaLemmaId
                    IdLemmaLemmaPos = i.AttributeValueOrDefault("lemma-pos");
                    IdLemmaLemmaRef = i.AttributeValueOrDefault("lemma-ref");
                    IdLemmaLemmaDescriptionRef = i.AttributeValueOrDefault("lemma-description-ref");
                    IdLemmaLemmaIdRef = i.AttributeValueOrDefault("lemmaid-ref");
                });

               // Unbound = (id.Element("unbound") != null);

                // TODO: Handle multiple prioritize-when-lemma's?
                id.Element("prioritize-when-lemma").WhenNotNull(pri =>
                {
                    //PrioritizeWhenLemmaLemmaId
                    //PrioritizeWhenLemmaLemmaPos = pri.AttributeValueOrDefault("lemma-pos");
                    //PrioritizeWhenLemmaLemmaRef = pri.AttributeValueOrDefault("lemma-ref");
                    //PrioritizeWhenLemmaLemmaDescription = pri.AttributeValueOrDefault("lemma-description-ref");
                    //PrioritizeWhenLemmaLemmaIdRef = pri.AttributeValueOrDefault("lemmaid-ref");
                });
            });
        }

        private void ParseHead(XElement entryElement)
        {
            entryElement.Element("head").WhenNotNull(id =>
            {
                HeadWordExact = id.ChildElementValueOrDefault<string>("headword");
                id.Element("pos").WhenNotNull(i =>
                {
                    HeadPosShortNameGyl = i.AttributeValueOrDefault("short-name-gyl");
                });
            });
        }

        private void ParseBody(XElement entryElement)
        {
            entryElement.Element("body").WhenNotNull(b =>
            {
             //   BodyTargetNode = b.AttributeValueOrDefault("target-nod-id");

                b.Element("reference").WhenNotNull(r =>
                {
                    //BodyRefType = r.AttributeValueOrDefault("reference-type");
                    //BodyBookRef = r.AttributeValueOrDefault("book-ref");
                    //BodyNodeRef = r.AttributeValueOrDefault("node-ref");
                    //BodyHeadwordRef = r.ChildElementValueOrDefault<string>("headword-ref");
                  
                    //BodyHeadwordRef = r
                    //    .Elements("headword-ref")
                    //    .Select(e => e
                    //    .ConvertTextTypeToHtml()
                    //    .InnerXmlOrDefault()
                    //    .Trim())
                    //    .ToList();
                });
                BodySenses = b.ChildXElementsOfExtensionType("sense", x=> new EntrySenseElement(x));
                
            });
        }

        #region --- ID: -- 
     
            #region ---ID LEMMA----
        public string IdLemmaLemmaIdRef { get; set; }

        public string IdLemmaLemmaDescriptionRef { get; set; }

        public string IdLemmaLemmaRef { get; set; }

        public string IdLemmaLemmaPos { get; set; }
        #endregion

        public string BookId { get; set; }

        public string Id { get; set; }
        #endregion

        #region ---head---
        public string HeadPosShortNameGyl { get; set; }

        public string HeadWordExact { get; set; }
        #endregion

        #region --- BODY: --- 

        public IList<EntrySenseElement> BodySenses { get; set; }


        #endregion

        #region ---BLOB---

        //public bool Unbound { get; set; }

        //public string BookIllustrationType { get; set; }

        //public string ShortLabel { get; set; }

        //public string FullLabel { get; set; }

        //public string Icon { get; set; } 

        ////lemmaID
        //public string PrioritizeWhenLemmaLemmaIdRef { get; set; }

        //public string PrioritizeWhenLemmaLemmaDescription { get; set; }

        //public string PrioritizeWhenLemmaLemmaRef { get; set; }

        //public string PrioritizeWhenLemmaLemmaPos { get; set; }

        ////head
        //public int HeadWordLength { get; set; }

        ////body
        //public string BodyTargetNode { get; set; }

        //public string BodyHeadwordRef { get; set; }

        //public string BodyNodeRef { get; set; }

        //public string BodyBookRef { get; set; }

        //public string BodyRefType { get; set; }

        #endregion


    }

}