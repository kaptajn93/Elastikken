using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Elastikken
{
    //[ElasticsearchType(Name = "lemma")]
    public class Lemma
    {
        //    [String(Analyzer = "keyword", NullValue = "null")]
        public string Id { get; set; }

        //   [String(Analyzer = "keyword", NullValue = "null")]

        [Nest.String(Analyzer = Constants.AnalyzerNames.Keyword, Index = FieldIndexOption.Analyzed)]
        public string Orthography { get; set; }

        //   [String(Analyzer = "keyword", NullValue = "null")]
        public string PosShortNameGyl { get; set; }

        //  [String(Analyzer = "keyword", NullValue = "null")] 
        public string IlexId { get; set; }

       //[Object(Path = "AccessoryDatas", Store = false)]
        public IList<AccessoryData> AccessoryDatas { get; set; }

        public IList<Inflection> Inflections { get; set; } 


        //public override string ToString()
        //{
        //    return $"{Id}, AccessoryData Count: {AccessoryData?.Count ?? 0}";
        //}
    }
}