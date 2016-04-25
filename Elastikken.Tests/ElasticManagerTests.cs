using System;
using System.Collections.Generic;
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
                        .Nested<AccessoryData>(n => n
                            .Name(c => c.AccessoryDatas)
                            .AutoMap()
                        )
                        .Nested<Inflection>(i => i
                            .Name(h => h.Inflections)
                        )
                        .String(
                            n => n.Name(p => p.Orthography)
                                    .Index(FieldIndexOption.Analyzed)
                                    .Analyzer(Constants.AnalyzerNames.Keyword))
                    )
                );
            _manager.Client.Map<EntryDocument>(mc => mc
                   .AutoMap()
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
                           .Properties(p => p
                               .Nested<Sense>(se => se
                                   .Name(n => n.Sense)
                                   .AutoMap()
                                   .Properties(pr => pr
                                       .Nested<Subsense>(s => s
                                           .Name(n => n.Subsenses)
                                           .AutoMap()
                                           .Properties(pro => pro
                                               .Nested<TargetGroup>(t => t
                                                   .Name(n => n.TGroups)
                                                   .AutoMap()
                                                   .Properties(pat => pat
                                                       .Nested<AnnotatedTarget>(at => at
                                                           .Name(n => n.AnnotatedTargets)
                                                           .AutoMap())))
                                           )
                                       )
                                   )
                               )
                           )
                   //    )
                   //)
               );


            Assert.True(response1.Acknowledged, response1.ServerError?.ToString() ?? response1.DebugInformation);
        }

        [Theory]
        [InlineData("da")]
        public void CostumSearchIndex(string indexName)
        {
            _manager.DeleteIndex(indexName);

            var indexDesc = new CreateIndexDescriptor(indexName)
    .Settings(f =>
        f.Analysis(analysis => analysis
                .Analyzers(analyzers => analyzers
                    .Custom(Constants.AnalyzerNames.Lowercase, a => a
                        .Filters("lowercase")
                        .Tokenizer(Constants.TokenizerNames.NoWhitespaceNGram)))
                .Tokenizers(tokenizers => tokenizers
                        .NGram(Constants.TokenizerNames.NoWhitespaceNGram, t => t
                            .MinGram(2)
                            .MaxGram(500)
                            .TokenChars(TokenChar.Digit, TokenChar.Letter, TokenChar.Punctuation, TokenChar.Punctuation, TokenChar.Symbol)
                        )
                )
        )
        )
     .Mappings(ms => ms.Map<LemmaDocument>(m => m
        .AutoMap()
        .Properties(ps => ps
            .Nested<AccessoryData>(n => n
                 .Name(c => c.AccessoryDatas)
                 .AutoMap()
            )
            .String(n => n.Name(p => p.Orthography).Index(FieldIndexOption.Analyzed).Analyzer(Constants.AnalyzerNames.Lowercase))
          )
      // .String(n => n.Name(c => c.ContactName).CopyTo(fs => fs.Field(Constants.CombinedSearchFieldName)).Index(FieldIndexOption.Analyzed).Analyzer(Constants.ElasticSearch.AnalyzerNames.LowercaseNGram))
      )
    );
            _manager.Client.CreateIndex(indexDesc);
        }

        [Theory]
        [InlineData("da")]
        public void StandardSearchIndex(string indexName)
        {
            _manager.DeleteIndex(indexName);

            var indexDesc = new CreateIndexDescriptor(indexName)
     .Mappings(ms => ms.Map<LemmaDocument>(m => m
        .AutoMap()
        .Properties(ps => ps
            .Nested<AccessoryData>(n => n
                 .Name(c => c.AccessoryDatas)
                 .AutoMap()
            )
            .Nested<Inflection>(i => i
                .Name(n => n.AccessoryDatas)
                .AutoMap()
            )
          )
      // .String(n => n.Name(c => c.ContactName).CopyTo(fs => fs.Field(Constants.CombinedSearchFieldName)).Index(FieldIndexOption.Analyzed).Analyzer(Constants.ElasticSearch.AnalyzerNames.LowercaseNGram))
      )
    );
            _manager.Client.CreateIndex(indexDesc);
        }



        //[Theory]
        //[InlineData("vin", "1")]
        ////[InlineData("vinke", "2")]
        //public void CanAddDocument(string ortography, string ilexId)
        //{
        //    var lemma = new LemmaDocument();
        //    {
        //        Orthography = ortography,
        //        IlexId = ilexId
        //    };

        //    var testList = new List<LemmaDocument> { LemmaDocument };
        //    var lemmaAdded = _manager.AddLemmaDocumentData(testList, "da");
        //    Assert.True(lemmaAdded);

        //    //Lemma lemmaFound = _manager.GetLemmaById(ilexId);
        //    //Assert.Equal(ilexId, lemmaFound.IlexId);

        //}

        //[Theory]
        //[InlineData("Hat", "1", "hatte")]
        //public void CanAddObject(string ortography, string ilexId, string title)
        //{
        //    List<AccessoryData> accessoryDataList = new List<AccessoryData>();
        //    for (int i = 0; i < 3; i++)
        //    {
        //        accessoryDataList.Add(new AccessoryData { IsActive = i % 2 == 0, Title = title + i });
        //    }
        //    var lemma = new Lemma
        //    {
        //        Orthography = ortography,
        //        IlexId = ilexId,
        //        AccessoryDatas = accessoryDataList,
        //    };
        //    var testList = new List<Lemma> { lemma };
        //    var lemmaAdded = _manager.AddLemmaData(testList, "da");
        //    Assert.True(lemmaAdded);
        //}


        //[Theory]
        //[InlineData("hat")]
        //[InlineData("Hat")]
        //public void GetLemmaByOrtography(string orthography)
        //{
        //    var lemma = _manager.GetLemmaByOrtography(orthography);
        //    Assert.NotNull(lemma);
        //    Assert.Equal(orthography, lemma.Orthography);
        //}

        //[Theory]
        //[InlineData("Hat", "1", "hatten")]
        //[InlineData("Kat", "2", "katte")]
        //[InlineData("Fnat", "3", "fnat")]
        //[InlineData("Tag", "4", "tage")]
        //[InlineData("Du", "5", "du's")]
        //[InlineData("Af", "6", "af")]
        //[InlineData("Hylde", "7", "hylden")]
        //[InlineData("tag hatten af", "8", "banan")]
        //public void CanInsertData(string ortography, string ilexId, string title)
        //{
        //    List<AccessoryData> accessoryDataList = new List<AccessoryData>();

        //    for (int i = 0; i < 3; i++)
        //    {
        //        accessoryDataList.Add(new AccessoryData { IsActive = i % 2 == 0, Title = title + i });
        //    }
        //    var lemma = new Lemma
        //    {
        //        Orthography = ortography,
        //        IlexId = ilexId,
        //        AccessoryDatas = accessoryDataList,
        //    };
        //    var testList = new List<Lemma> { lemma };
        //    var lemmaAdded = _manager.AddLemmaData(testList, "da");
        //    Assert.True(lemmaAdded);
        //}


        public void Dispose()
        {
            //var response = _manager.DeleteIndex(indexName);
        }



    //    public IList<EntryDocument> GenerateEntries()
    //    {
    //        var entryList = new List<EntryDocument>
    //        {
    //            new EntryDocument {
    //                Blob = "",
    //                EntryId = new EntryId
    //                {
    //                    idBook = "daen-rød",
    //                    idLemmaPos = "vb.",
    //                    idLemmaRef = "bage",
    //                    idLemmaDescriptionRef = "om bagværk",
    //                    lemmaIdRef = "dale0010415",
    //                    idEntry = "daenrød0004332"
    //                },
    //                Head = new Head
    //                {
    //                    headWord = "bage",
    //                    headPosShortNameGyl ="vb.",
    //},
    //                Body = new Body
    //                {
    //                    headWordRef = "bagende",
    //                    Sense = new Sense{
    //                        Subsenses = new List<Subsense>{
    //                            new Subsense{
    //                                TGroups = new List<TargetGroup>{
    //                                    new TargetGroup{
    //                                        AnnotatedTargets = new List<AnnotatedTarget>{
    //                                            new AnnotatedTarget{
    //                                                translation = "bake",
    //                                                examples = new List<string>(),
                                                     
    //                                            }
    //                                        }
    //                                    }
    //                                }
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        };
    //        return entryList;
    //    }

        public IList<LemmaDocument> GenerateLemmaList2()
        {
            var list = new List<LemmaDocument>
            {
                new LemmaDocument()
                {
                    IlexId = "dale0268737",
                    Orthography = "forundringsudbrud",
                    Id = new Guid().ToString(),
                    PosShortNameGyl = "substantiv",
                    AccessoryDatas = new List<AccessoryData>
                    {
                        new AccessoryData {
                            Category  = "ord der ender på ¤",
                            LemmaRefs = new List<LemmaReference>
                            {
                                new LemmaReference { LemmaPos = "sb.", LemmaRef = "snabel-a"},
                                new LemmaReference { LemmaPos = "sb.", LemmaRef = "en bjørn af teglsten"}
                            }
                        },
                        new AccessoryData {
                            Category  = "synonymer til ¤",
                            LemmaRefs = new List<LemmaReference>
                            {
                                new LemmaReference { LemmaPos = "sb.", LemmaRef = "la", LemmaDescriptionRef = "tone"},
                                new LemmaReference { LemmaPos = "sb.", LemmaRef = "normaltone"}
                            }
                        },
                    },
                    Inflections = new List<Inflection>
                    {
                        new Inflection
                        {
                            CompactPresentation = "forundringsudbruddet; forundringsudbrud; forundringsudbruddene",
                            TablePresentation = "<table-presentation> <row> <cell/> <cell cell-type='header'>ubestemt form</cell> <cell cell-type='header'>bestemt form</cell> </row> <row> <cell cell-type='header'>singularis</cell> <cell cell-type='content'>forundringsudbrud</cell> <cell cell-type='content'>forundringsudbruddet</cell> </row> <row> <cell cell-type='header'>pluralis</cell> <cell cell-type='content'>forundringsudbrud</cell> <cell cell-type='content'>forundringsudbruddene</cell> </row> </table-presentation>",
                            SearchableParadigms = new List<SearchableParadigm>
                            {
                                new SearchableParadigm
                                {
                                    LeaveOut = true,
                                    InflectedFormCategoryNameGyl = "singularis ubestemt",
                                    InflectedFormFullForm = "forundringsudbrud",
                                    InflectedFormCompactForm = "forundringsudbrud"
                                },
                                new SearchableParadigm
                                {
                                    LeaveOut = true,
                                    InflectedFormCategoryNameGyl = "singularis ubestemt, genitiv",
                                    InflectedFormFullForm = "forundringsudbruds",
                                    InflectedFormCompactForm = "forundringsudbruds"
                                },
                                 new SearchableParadigm
                                {
                                    LeaveOut = true,
                                    InflectedFormCategoryNameGyl = "singularis bestemt",
                                    InflectedFormFullForm = "forundringsudbruddet",
                                    InflectedFormCompactForm = "forundringsudbruddet"
                                },
                            }
                        }
                    }
                },
              };
            return list;
        }

        [Fact]
        public void CanAddLemmaList()
        {
            var list = GenerateLemmaList2();
            var tryAddLemma = _manager.AddLemmaDocumentData(list, "da");
            Assert.True(tryAddLemma);

        }
        //[Fact]
        //public void CanAddEntryList()
        //{
        //    var list = GenerateEntries();
        //    var tryAddEntry = _manager.AddEntryData(list, "da");
        //    Assert.True(tryAddEntry);

        //    //var entry = new Entry();
        //    //entry.Blob = JsonConvert.SerializeObject(entry);
        //}
    }
}
