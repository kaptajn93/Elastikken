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
    public class EntryDocument
    {
        public EntryDocument()
        {

        }

        public EntryDocument(string id)
        {

        }
        //id
        public string IdBook { get; set; }
        public string IdEntry { get; set; }
      //  public EntryIdLemma EntryIdLemma { get; set; }
        //head
        public string HeadWord { get; set; }
        public string HeadPosShortNameGyl { get; set; }
        //body
        public IList<EntrySenseElement> Sense { get; set; }
        public string Blob { get; set; }
        //public string BodyHeadWordRef { get; set; }
        // public IList<TargetGroup> Subsense { get; set; }
        //public IList<AnnotatedTarget> TargetGroup { get; set; }
       
            //test counts
        public int SenseCount { get; set; }
        public int SubsenseCount { get; set; }
        // 
       
       // public EntryElement EntryElement { get; set; }
        public EntryElement GetDeserializedBlobAndDeleteBlob()
        {
            var element = JsonConvert.DeserializeObject<EntryElement>(Blob);
            return element;
        }
    }

    //public class EntryIdLemma
    //{
    //    public string IdLemmaPos { get; set; }
    //    public string IdLemmaRef { get; set; }
    //    public string IdLemmaDescriptionRef { get; set; }
    //    public string LemmaIdRef { get; set; }

    //}
}

public class Sense
{
    public IList<EntrySubsenseElement> Subsenses { get; set; }
    public string TargetNodeId { get; set; }
}

public class Subsense
{
    public IList<EntryTargetGroupElement> TGroups { get; set; }
    public string TargetNodeId { get; set; }
}

public class TargetGroup
{
    public IList<EntryAnnotatedTargetElement> AnnotatedTargets { get; set; }
    public string TargetNodeId { get; set; }
}

public class AnnotatedTarget
{
    public string Translation { get; set; }
    //public string Bendings { get; set; }
    public IList<string> Examples { get; set; }
}



