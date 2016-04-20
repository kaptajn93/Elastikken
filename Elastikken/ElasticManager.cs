using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Elastikken
{


    public class Constants
    {
        public class AnalyzerNames
        {
            public const string NgramAnalyzer = "ngramAnalyzer";
            public const string Keyword = "keyword";
            public const string Lowercase = "lowercase";
        }

        public class TokenizerNames
        {
            public const string NoWhitespaceNGram = "noWhitespaceNGram";

        }

        public class IndexNames
        {
            public const string Da = "da";
            public const string En = "en";


        }
    }
    
    


    public class ElasticManager
    {
        public Uri NodeUri { get; set; }
        public ConnectionSettings Settings { get; set; }
        public ElasticClient Client { get; set; }

        public ElasticManager(Uri node = null)
        {
            NodeUri = node ?? new Uri("http://192.168.99.100:9200");
            Settings = new ConnectionSettings(NodeUri);
            Client = new ElasticClient(Settings);
        }


        public IDeleteIndexResponse DeleteIndex(string indexName)
        {
            var deleteIndexResponse = Client.DeleteIndex(indexName);
            return deleteIndexResponse;
        }

        public ICreateIndexResponse CreateIndex(string indexName)
        {
            var createIndexResponse = Client.CreateIndex(indexName);
            //Settings.MapDefaultTypeIndices()
            return createIndexResponse;
        }
        public ICreateIndexResponse CreateIndexWithMapping(string indexName)
        {
            var createIndexResponse = Client.CreateIndex(indexName);

            //CreateIndexDescriptor(indexName)
            //  .Mappings(ms => ms
            //       .Map<Lemma>(m => m
            //           .Properties(ps => ps
            //               .String(s => s
            //                   .Name(c => c.Orthography)
            //               )
            //               .Object<AccessoryData>(o => o
            //                   .Name(c => c.AccessoryDatas)
            //                   .Properties(eps => eps
            //                       .String(s => s
            //                           .Name(e => e.Title)
            //                       )
            //                   )
            //               )
            //           )
            //       )
            //   );
            return createIndexResponse;
        }

        public bool AddLemmaData(IList<Lemma> list, string indexName)
        {
            foreach (var lemma in list)
            {
                Client.Index(lemma, c =>
                    c.Index(indexName));
            }

            return true;
        }

        public Lemma GetLemmaById(string ilexId)
        {
            var response = Client.Search<Lemma>(s => s
               .From(0)
               .Size(10)
               .Query(q =>
                    q.Term(p => p.Id, ilexId)
                )
           );
            return response.Documents.FirstOrDefault();

        }









        public Lemma GetLemmaByOrtography(string ortography)
        {
            var response = Client.Search<Lemma>(descriptor => descriptor
                .FielddataFields(fs => fs
                    .Field(f => f.Orthography)
                    .Field(f => f.IlexId))
                //.Source(false)
                .Query(q => 
                    q.Bool(t => 
                        t.Must(m => 
                            //m.Term("ortography", ortography)
                            m.Term(qd => qd.Orthography, ortography)
                            )
                        )
                    )
                );
                    
            

            return response.Documents.FirstOrDefault();
        }

        public void SearchLemmaByOrtography(string ortography)
        {
            throw new NotImplementedException();
        }
    }
}