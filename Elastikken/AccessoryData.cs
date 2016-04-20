using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Newtonsoft.Json;

namespace Elastikken
{
    [ElasticsearchType(Name = "AccessoryData")]
    public class AccessoryData
    {
        //[String(Analyzer = "danish", TermVector = TermVectorOption.WithOffsets)]
        //public string Title { get; set; }
        //public bool IsActive { get; set; }


        public string Category { get; set; }

        public IList<LemmaReference> LemmaRefs { get; set; }

    }

    public class LemmaReference
    {
        public string LemmaPos { get; set; }
        public string LemmaRef { get; set; }
        public string LemmaDescriptionRef { get; set; }

    }
}
