using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Elastikken.Parsing;
using Nest;
using Newtonsoft.Json;
using Xunit;

namespace Elastikken.Tests
{
    public class ElasticManagerTests : IDisposable
    {
        ElasticManager _manager;

        public ElasticManagerTests()
        {
            _manager = new ElasticManager();
        }

        [Fact]
        public void CanCreateManager()
        {
            Assert.NotNull(_manager);
        }

        [Theory]
        [InlineData("da")]
        [InlineData("en")]
        public void CanUpdateIndex(string indexName)
        {
            //Delete
            var response = _manager.DeleteIndex(indexName);
            Assert.True(response.Acknowledged, response.ServerError?.ToString() ?? response.DebugInformation);
            //Create
            var response1 = _manager.CreateIndex(indexName);
            _manager.Client.Map<LemmaDocument>(ms => ms
                    .AutoMap()
                    .Properties(ps => ps
      //ingen ide   .String(n => n.Name(p => p.LemmaOrtography)
                        //      .Index(FieldIndexOption.Analyzed)
                        //      .Analyzer(Constants.AnalyzerNames.Keyword))
                        .Nested<LemmaPos>(lp => lp
                            .Name(lpn => lpn.LemmaPos)
                            .AutoMap())
                        .Nested<LemmaGender>(ge => ge
                            .Name(gn => gn.LemmaGender)
                            .AutoMap())
                        .Nested<LemmaInflection>(i => i
                            .Name(h => h.LemmaInflection)
                            .AutoMap())
                        .Nested<LemmaVariants>(v=>v
                            .Name(vn=>vn.LemmaVariants)
                            .AutoMap())
                        .Nested<LemmaAccessoryData>(n => n
                            .Name(c => c.LemmaAccessoryData)
                            .AutoMap())
                    )
                );

            _manager.Client.Map<EntryDocument>(mc => mc
                   .AutoMap()
                   .Properties(ed => ed
                   .Nested<EntryIdLemma>(el => el
                   .Name(nel => nel.EntryIdLemma)
                   .AutoMap()))

            #region ---Nested entryID/Head/Body
               //.Properties(pe => pe
               //    .Nested<EntryId>(ne => ne
               //        .Name(nae => nae.EntryId)
               //        .AutoMap())
               //    .Nested<Head>(nh => nh
               //        .Name(nu => nu.Head)
               //        .AutoMap())
               //    .Nested<Body>(nb => nb
               //        .Name(n => n.Body)
               //        .AutoMap() 
            #endregion


            #region ---Sense---
               //.Properties(p => p
               //    .Nested<EntrySenseElement>(se => se

               //.Name(n => n.Sense)
               //.AutoMap()
               //.Properties(pr => pr
               //    .Nested<EntrySubsenseElement>(s => s
               //        .Name(n => n.Subsense)
               //        .AutoMap()
               //        .Properties(pro => pro
               //            .Nested<EntryTargetGroupElement>(t => t
               //                .Name(n => n.TargetGroups)
               //                .AutoMap()
               //                .Properties(pat => pat
               //                    .Nested<EntryAnnotatedTargetElement>(at => at
               //                        .Name(n => n.AnnotatedTargets)
               //                        .AutoMap())))
               //        )
               //    )
               //) 

               //)
               //)
            #endregion

               );


            Assert.True(response1.Acknowledged, response1.ServerError?.ToString() ?? response1.DebugInformation);
        }

        public void Dispose()
        {
        }

        //public IList<LemmaDocument> GenerateLemmaList2()
        //{
        //    var list = new List<LemmaDocument>
        //    {
        //        new LemmaDocument()
        //        {
        //            IlexId = "dale0268737",
        //            Orthography = "forundringsudbrud",
        //            Id = new Guid().ToString(),
        //            PosShortNameGyl = "substantiv",
        //            AccessoryDatas = new List<AccessoryData>
        //            {
        //                new AccessoryData {
        //                    Category  = "ord der ender på ¤",
        //                    LemmaRefs = new List<LemmaReference>
        //                    {
        //                        new LemmaReference { LemmaPos = "sb.", LemmaRef = "snabel-a"},
        //                        new LemmaReference { LemmaPos = "sb.", LemmaRef = "en bjørn af teglsten"}
        //                    }
        //                },
        //                new AccessoryData {
        //                    Category  = "synonymer til ¤",
        //                    LemmaRefs = new List<LemmaReference>
        //                    {
        //                        new LemmaReference { LemmaPos = "sb.", LemmaRef = "la", LemmaDescriptionRef = "tone"},
        //                        new LemmaReference { LemmaPos = "sb.", LemmaRef = "normaltone"}
        //                    }
        //                },
        //            },
        //            Inflections = new List<Inflection>
        //            {
        //                new Inflection
        //                {
        //                    CompactPresentation = "forundringsudbruddet; forundringsudbrud; forundringsudbruddene",
        //                    TablePresentation = "<table-presentation> <row> <cell/> <cell cell-type='header'>ubestemt form</cell> <cell cell-type='header'>bestemt form</cell> </row> <row> <cell cell-type='header'>singularis</cell> <cell cell-type='content'>forundringsudbrud</cell> <cell cell-type='content'>forundringsudbruddet</cell> </row> <row> <cell cell-type='header'>pluralis</cell> <cell cell-type='content'>forundringsudbrud</cell> <cell cell-type='content'>forundringsudbruddene</cell> </row> </table-presentation>",
        //                    SearchableParadigms = new List<SearchableParadigm>
        //                    {
        //                        new SearchableParadigm
        //                        {
        //                            LeaveOut = true,
        //                            InflectedFormCategoryNameGyl = "singularis ubestemt",
        //                            InflectedFormFullForm = "forundringsudbrud",
        //                            InflectedFormCompactForm = "forundringsudbrud"
        //                        },
        //                        new SearchableParadigm
        //                        {
        //                            LeaveOut = true,
        //                            InflectedFormCategoryNameGyl = "singularis ubestemt, genitiv",
        //                            InflectedFormFullForm = "forundringsudbruds",
        //                            InflectedFormCompactForm = "forundringsudbruds"
        //                        },
        //                         new SearchableParadigm
        //                        {
        //                            LeaveOut = true,
        //                            InflectedFormCategoryNameGyl = "singularis bestemt",
        //                            InflectedFormFullForm = "forundringsudbruddet",
        //                            InflectedFormCompactForm = "forundringsudbruddet"
        //                        },
        //                    }
        //                }
        //            }
        //        },
        //      };
        //    return list;
        //}

        public IList<LemmaDocument> GenerateLemmaList()
        {
            var list = new List<LemmaDocument>();
            return list;
        }
        [Fact]
        public void CanAddLemmaList()
        {
            var list = GenerateLemmaList();
            var tryAddLemma = _manager.AddLemmaDocumentData(list, "da");
            Assert.True(tryAddLemma);
        }

        public IList<EntryDocument> GenerateEntryList()
        {
            var list = new List<EntryDocument>
            {
                new EntryDocument()
                {
                    IdEntry = "idEntryIDDD",
                    IdBook = "idBookID",
                    HeadPosShortNameGyl = "SHORTNAMEGYL",
                    HeadWord = "HEADWORD",
                    SenseCount = 1,
                    Blob = "Blobiblob",
                  #region ---Sense---
		  //Sense = new List<EntrySenseElement>
                    //{
                    //    new EntrySenseElement()
                    //    {
                    //        TargetNodeId = "SenseNodeID",
                    //        Subsense = new List<EntrySubsenseElement>
                    //        {
                    //         new EntrySubsenseElement
                    //            {
                    //                TargetNodeId = "SubNodeID",
                    //                TargetGroups = new List<EntryTargetGroupElement>
                    //                {
                    //                    new EntryTargetGroupElement
                    //                    {
                    //                        AnnotatedTargets = new List<EntryAnnotatedTargetElement>
                    //                        {
                    //                          new EntryAnnotatedTargetElement
                    //                          {
                    //                              Translation = "Baked",
                    //                              Examples = new List<string>()
                    //                          }
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //} 
	#endregion

                }
            };
            return list;
        }

        [Fact]
        public void CanAddEntryList()
        {

            var list = GenerateEntryList();
            var tryAddEntry = _manager.AddEntryData(list, "da");
            Assert.True(tryAddEntry);

        }
    }
}
