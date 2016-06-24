using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Elastikken.Parsing.Entry;
using Newtonsoft.Json;
using NLog;

namespace Elastikken.Parsing
{
    public class EntryParser : ParserBase<EntryDocument>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public override IEnumerable<EntryDocument> ParseXml(IEnumerable<XElement> elements)
        {
            try
            {
                var entries = from entryXElement in elements
                              let entry = new EntryElement(entryXElement)
                              where entry != null

                              let filename = entryXElement.Attributes("filename").FirstOrDefault()

                              select new EntryDocument(Guid.NewGuid().ToString())
                              {
                                  //id
                                  IdEntry = entry.Id,
                                  IdBook = entry.BookId,
                                  
                                  Unbound = entry.Unbound,
                                  PrioritizeWhenLemma = entry.PrioritizeWhenLemma.Select(s =>
                                  new PrioritizeWhenLemma
                                  {
                                      PrioritizeLemmaPos = entry.PrioritizeWhenLemma?.FirstOrDefault()?.PrioritizeWhenLemmaLemmaPos,
                                      PrioritizeLemmaRef = entry.PrioritizeWhenLemma?.FirstOrDefault()?.PrioritizeWhenLemmaLemmaRef,
                                      PrioritizeLemmaDescRef = entry.PrioritizeWhenLemma?.FirstOrDefault()?.PrioritizeWhenLemmaLemmaDescription,
                                      PrioritizeLemmaIdRef = entry.PrioritizeWhenLemma?.FirstOrDefault()?.PrioritizeWhenLemmaLemmaIdRef
                                  }).ToList(),

                                  //idLemma
                                  EntryIdLemma = new EntryIdLemma
                                  {
                                      IdLemmaPos = entry.IdLemmaLemmaPos,
                                      IdLemmaRef = entry.IdLemmaLemmaRef,
                                      IdLemmaDescriptionRef = entry.IdLemmaLemmaDescriptionRef,
                                      LemmaIdRef = entry.IdLemmaLemmaIdRef,
                                  },
                                  //head
                                  HeadWord = entry.HeadWord,
                                  HeadWordExact = entry.HeadWord,

                                  HeadPosShortNameGyl = entry.HeadPosShortNameGyl,
                                  //body
                                  SenseCount = entry.BodySenses?.Count ?? 0,
                                  Blob = JsonConvert.SerializeObject(entry),
                              };

                return entries;
            }
            catch (Exception ex)
            {
                logger.Error(ex);

                throw;
            }
        }

        public override IEnumerable<EntryDocument> ParseXml(IEnumerable<string> xmlFilesToImport)
        {
            var elements = StreamElements(xmlFilesToImport);
            return ParseXml(elements);
        }

        protected override string NodeName => "entry";
    }
}