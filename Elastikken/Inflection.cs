using System.Collections.Generic;

namespace Elastikken
{

    public class Inflection
    {
        /// <summary>
        /// 
        /// </summary>
        public string CompactPresentation { get; set; }

        /// <summary>
        ///     Contains the entire table-presentation element. 
        /// </summary>
        public string TablePresentation { get; set; }

        public IList<SearchableParadigm> SearchableParadigms { get; set; }
        
    }

    public class SearchableParadigm
    {
        public bool LeaveOut { get; set; }
        public string InflectedFormCategoryNameGyl { get; set; }
        public string InflectedFormFullForm { get; set; }
        public string InflectedFormCompactForm { get; set; }
        
    }










}
