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
            return createIndexResponse;
        }

        public bool AddLemmaDocumentData(IList<LemmaDocument> list, string indexName)
        {
            foreach (var LemmaDocument in list)
            {
                Client.Index(LemmaDocument, c =>
                    c.Index(indexName));
            }
            return true;
        }

        public LemmaDocument GetLemmaDocumentById(string lemmaId)
        {
            var response = Client.Search<LemmaDocument>(s => s
               .From(0)
               .Size(10)
               .Query(q =>
                    q.Term(p => p.LemmaId, lemmaId)
                )
           );
            return response.Documents.FirstOrDefault();

        }

        //public LemmaDocument GetLemmaDocumentByOrtography(string ortography)
        //{
        //    var response = Client.Search<LemmaDocument>(descriptor => descriptor
        //        .FielddataFields(fs => fs
        //            .Field(f => f.LemmaOrtography)
        //            .Field(f => f.IlexId))
        //        //.Source(false)
        //        .Query(q =>
        //            q.Bool(t =>
        //                t.Must(m =>
        //                    //m.Term("ortography", ortography)
        //                    m.Term(qd => qd.Orthography, ortography)
        //                    )
        //                )
        //            )
        //        );



        //    return response.Documents.FirstOrDefault();
        //}

        public void SearchLemmaDocumentByOrtography(string ortography)
        {
            throw new NotImplementedException();
        }

        public bool AddEntryData(IList<EntryDocument> list, string indexName)
        {
            foreach (var entrydocument in list)
            {
                Client.Index(entrydocument, c =>
                    c.Index(indexName));
            }

            return true;
        }

    }
}