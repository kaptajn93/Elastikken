using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Nest;

namespace Elastikken
{
    [Nest.ElasticsearchType]
    public class LemmaDocument
    {
        /// <summary>
        ///     Afspejler documentet som skal indsættes i ElasaticSearch
        /// </summary>
        public LemmaDocument()
        {
        }

        public LemmaDocument(string id)
        {
        }

        public string LemmaLanguage { get; set; }

        public string LemmaId { get; set; }

        public string LemmaOrtography { get; set; }

        public LemmaPos LemmaPos { get; set; }

        public LemmaGender LemmaGender { get; set; }

        public LemmaInflection LemmaInflection { get; set; }

        public LemmaVariants LemmaVariants { get; set; }

        public string LemmaMeAsFirst { get; set; }
        public string LemmaMeAsLast { get; set; }

        public LemmaAccessoryData LemmaAccessoryData { get; set; }



    }

    public class LemmaPos
    {
        public string PosNameDan { get; set; }
        public string PosNameGyl { get; set; }
        public string PosNameEng { get; set; }
        public string PosNameLat { get; set; }
        public string PosShortNameDan { get; set; }
        public string PosShortNameGyl { get; set; }
        public string PosShortNameEng { get; set; }
        public string PosShortNameLat { get; set; }
    }

    public class LemmaGender
    {
        public string GenNameDan { get; set; }
        public string GenNameGyl { get; set; }
        public string GenNameEng { get; set; }
        public string GenNameLat { get; set; }
        public string GenShortNameDan { get; set; }
        public string GenShortNameGyl { get; set; }
        public string GenShortNameEng { get; set; }
        public string GenShortNameLat { get; set; }
    }

    public class LemmaVariants
    {
        public string LemmaVariantsRefPos;
        public string LemmaVariantsRefRef;
    }

    public class LemmaAccessoryData
    {
        public string CategoryDan { get; set; }
        public string CategoryEng { get; set; }

        public IList<LemmaReference> LemmaAccessDataReferencesRefs { get; set; }
    }

    public class LemmaReference
    {
        public string LemmaPos { get; set; }
        public string LemmaRef { get; set; }

    }

    public class LemmaInflection
    {
        public string CompactPresentation { get; set; }

        public IList<LemmaTablePresentation> TablePresentations { get; set; }
        /// <summary>
        ///     Contains the entire table-presentation element. 
        /// </summary>
        public IList<LemmaSearchableParadigm> SearchableParadigms { get; set; }
    }

    public class LemmaSearchableParadigm
    {
        public IList<LemmaInflectedForm> LemmaInflectedForms { get; set; } 

    }

    public class LemmaInflectedForm
    {
        public string LeaveOut { get; set; }
        public LemmaInflectedFormCategory InflectedFormCategory{ get; set; }
        public string InflectedFormFullForm { get; set; }
        public string InflectedFormCompactForm { get; set; }
    }

    public class LemmaInflectedFormCategory
    {
        public string InfCatNameDan { get; set; }
        public string InfCatNameGyl { get; set; }
        public string InfCatNameEng { get; set; }
        public string InfCatNameLat { get; set; }
        public string InfCatShortNameDan { get; set; }
        public string InfCatShortNameGyl { get; set; }
        public string InfCatShortNameEng { get; set; }
        public string InfCatShortNameLat { get; set; }
    }

    public class LemmaTablePresentation
    {
        public IList<LemmaTpRow> LemmaTpRows {get; set; }
    }

    public class LemmaTpRow
    {
        public IList<LemmaTpRowCells> LemmaTpRowCellses { get; set; } 
    }

    public class LemmaTpRowCells
    {
        public string CellType { get; set; }
        public string CellName { get; set; }

    }
}