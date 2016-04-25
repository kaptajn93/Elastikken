using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;

namespace Elastikken
{
    public class Program
    {
        public static Uri node;
        public static ConnectionSettings settings;
        public static ElasticClient client;

        static void Main(string[] args)
        {
            //var elasticManager = new ElasticManager();
          
            var elasticManager = new ElasticManager();


          //  var response = client.Get<Lemma>(1, idx => idx.Index("da"));

          //  Console.WriteLine(response);

            //foreach (var i in GenerateEntryList())
            //{
            //    client.Index(i, c =>
            //   c.Index("da"));
            //}
            //client.DeleteIndex("da");
            //client.CreateIndex("da");

            //foreach (var i in GenerateLemmaList())
            //{
            //    client.Index(i, c =>
            //   c.Index("da"));
            }
            //public ISearchResponse<Lemma> findAll()
            //{
            //    var response = client.Search<Lemma>(s => s
            //        .From(0)
            //        .Size(10)
            //        .Query(q => q.Raw(@"{""match_all"": {} }"))
            //        );

            //    foreach (var doc in response.Documents)
            //    {
            //        Console.WriteLine(doc);
            //    }

            //    Console.ReadLine();

            //    return response;
            //}

            // --- nyt
        //    var descriptor = new CreateIndexDescriptor("myindex")
        //         .Mappings(ms => ms
        //            .Map<Lemma>(m => m.AutoMap())
        //            .Map<AccessoryData>(m => m.AutoMap()));

        //    client.Serializer.Serialize(descriptor, Stream.Null, SerializationFormatting.Indented);
        //}




        //public static IList<Lemma> GenerateLemmaList()
        //{
        //    var list = new List<Lemma>();

        //    for (int i = 0; i < 10; i++)
        //    {
        //        list.Add(new Lemma
        //        {
        //            IlexId = i.ToString(),
        //            Orthography = ((char)i).ToString(),
        //            Id = "id" + i,
        //            PosShortNameGyl = "posShort" + i,
        //            AccessoryDatas = new List<AccessoryData>
        //            {
        //                new AccessoryData { IsActive = i%2 == 0, Title = "AD" + i}
        //            },

        //        });
        //    }

        //    list.Add(new Lemma
        //    {
        //        Orthography = "kimchy"
        //    });

        //    return list;
        //}

        //public static IList<Entry> GenerateEntryList()
        //{
        //    var list = new List<Entry>();

        //    for (int i = 0; i < 10; i++)
        //    {
        //        list.Add(new Entry
        //        {
        //            IlexId = i.ToString(),
        //            headWord = ((char)i).ToString(),
        //            id = "id" + i,
        //            bookId = "bog" + i,
        //            lemmaId = "lemmaId" + i,

        //            type = "Entry"
        //        });
        //    }

        //    return list;
        //}
    }
}