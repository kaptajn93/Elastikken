using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Elastikken.Parsing
{
    public class EntryParser : ParserBase<EntryDocument>
    {

        public override IEnumerable<EntryDocument> ParseXml(IEnumerable<XElement> elements)
        {
            var entries = from entryXElement in elements
                          let entry = new EntryElement(entryXElement)
                          let ano = new EntryAnnotatedTargetElement(entryXElement)
                          let sense = new EntrySenseElement(entryXElement)
                          let sub = new EntrySubsenseElement(entryXElement)

                          //let book = _configurationRepository.TryGetBookById(entry.BookId)
                          //where book != null
                          where entry != null

                          let filename = entryXElement.Attributes("filename").FirstOrDefault()

                          //from sense in entry.BodySenses
                          //from subSense in sense.SubSenses
                          //from targetGroup in subSense.TargetGroups
                          //from annotatedTarget in targetGroup.AnnotatedTargets

                          select new EntryDocument(Guid.NewGuid().ToString())
                          {
                              //id
                              IdEntry = entry.Id,
                              IdBook = entry.BookId,
                              //idLemma
                                    //EntryIdLemma = new EntryIdLemma
                                    //{
                                    //    IdLemmaPos = entry.IdLemmaLemmaPos,
                                    //    IdLemmaRef = entry.IdLemmaLemmaRef,
                                    //    IdLemmaDescriptionRef = entry.IdLemmaLemmaDescriptionRef,
                                    //    LemmaIdRef = entry.IdLemmaLemmaIdRef,
                                    //},
                              //head
                              HeadWord = entry.HeadWordExact,
                              HeadPosShortNameGyl = entry.HeadPosShortNameGyl,
                              //body
                              SenseCount = entry.BodySenses?.Count ?? 0,
                              Sense = entry.BodySenses,
                              SubsenseCount = sense.Subsense?.Count ?? 0, 
                              //BodyHeadWordRef = entry.BodyHeadwordRef,
                              //blob
                              Blob = JsonConvert.SerializeObject(entry),



                              //Priority = book.Priority,
                              //IsBeta = book.IsBeta,
                              //IlexId = entry.Id,
                              //IdLemmaLemmaId = entry.IdLemmaLemmaIdRef,
                              //Unbound = entry.Unbound,

                              //PrioritizeWhenLemmaLemmaId = entry.PrioritizeWhenLemmaLemmaIdRef,

                              ////HeadWordInexact = entry.HeadWordStripped,
                              //HeadWordExact = entry.HeadWordStripped,
                              //HeadWordLength = entry.HeadWordStripped.Length,
                              //HeadPosShortNameGyl = entry.HeadPosShortNameGyl,

                              ////BodyTargetNodeId = entry.BodyTargetNodeId,

                              //AnnotatedTargetTranslationExact = annotatedTarget.TranslationExact,
                              //AnnotatedTargetTranslationGramLemmaId = annotatedTarget.GetTranslationGramLemmaId(),

                              //TargetNodeIds = string.Join(",",
                              //    entry.BodyTargetNodeId ?? "",
                              //    entry.ShowForeignTargetNodeId ?? "",
                              //    sense.TargetNodeId ?? "",
                              //    subSense.TargetNodeId ?? "")
                              //    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                              //    .ToList()
                              //,

                              //ShowForeignTargetNodeId = entry.ShowForeignTargetNodeId,



                              //EntryElement = entry

                              //FeedImportFileName = filename != null ? filename.Value : "N/A"
                          };

            return entries;
        }

        public override IEnumerable<EntryDocument> ParseXml(IEnumerable<string> xmlFilesToImport)
        {
            var elements = StreamElements(xmlFilesToImport);
            return ParseXml(elements);
        }

        protected override string NodeName => "entry";
    }
}