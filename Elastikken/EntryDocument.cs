using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Elastikken.Parsing;
using Nest;
using Newtonsoft.Json;

namespace Elastikken
{

    /// <summary>
    ///     Afspejler documentet som skal indsættes i ElasaticSearch
    /// </summary>
    [Nest.ElasticsearchType]
    public class EntryDocument : BaseDocument
    {
        public EntryDocument()
        {
            //Way to get sense from deserialize blob and use entryElement

            // var x = new EntryDocument();
            // x.GetDeserializedBlobAndDeleteBlob();
            //var senses =  x.EntryElement.BodySenses;


        }

        public EntryDocument(string id)
        {

        }
        //id
        public string IdBook { get; set; }
        public string IdEntry { get; set; }
        public EntryIdLemma EntryIdLemma { get; set; }
        // /id

        public bool Unbound { get; set; }
        public IList<PrioritizeWhenLemma> PrioritizeWhenLemma { get; set; }


        //head

        //skal ikke lagres
        public string HeadWordExact { get; set; }
        public string HeadWord { get; set; }

        public string HeadPosShortNameGyl { get; set; }
        //body
        public string Blob { get; set; }
        //public string BodyHeadWordRef { get; set; }
        // public IList<TargetGroup> Subsense { get; set; }
        //public IList<AnnotatedTarget> TargetGroup { get; set; }

        //test counts
        public int SenseCount { get; set; }
        //public int SubsenseCount { get; set; }
        // 

        [Nest.Nested(Ignore = true)]
        public EntryElement EntryElement { get; set; }

        public EntryElement GetDeserializedBlobAndDeleteBlob()
        {
            //var element = JsonConvert.DeserializeObject<EntryElement>(Blob);
            this.EntryElement = JsonConvert.DeserializeObject<EntryElement>(Blob);
            return this.EntryElement;
        }
    }

   

    public class EntryIdLemma
    {
        public string IdLemmaPos { get; set; }
        public string IdLemmaRef { get; set; }
        public string IdLemmaDescriptionRef { get; set; }
        public string LemmaIdRef { get; set; }
    }

    public class PrioritizeWhenLemma
    {
        public string PrioritizeLemmaPos { get; set; }
        public string PrioritizeLemmaRef { get; set; }
        public string PrioritizeLemmaDescRef { get; set; }
        public string PrioritizeLemmaIdRef { get; set; }
    }
}

//public class Sense
//{
//    public IList<EntrySubsenseElement> Subsenses { get; set; }
//    public string TargetNodeId { get; set; }
//}

//public class Subsense
//{
//    public IList<EntryTargetGroupElement> TGroups { get; set; }
//    public string TargetNodeId { get; set; }
//}

//public class TargetGroup
//{
//    public IList<EntryAnnotatedTargetElement> AnnotatedTargets { get; set; }
//    public string TargetNodeId { get; set; }
//}

//public class AnnotatedTarget
//{
//    public string Translation { get; set; }
//    //public string Bendings { get; set; }
//    public IList<string> Examples { get; set; }
//}



