using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Elastikken.Parsing.Lemma;
using Newtonsoft.Json;

namespace Elastikken.Parsing
{
    public class LemmaParser : ParserBase<LemmaDocument>
    {


        public override IEnumerable<LemmaDocument> ParseXml(IEnumerable<XElement> elements)
        {
            var entries = from lemmaXElement in elements
                          let lemma = new LemmaElement(lemmaXElement)
                          let pos = new LemmaPosElement(lemmaXElement)
                          let gen = new LemmaGenderElement(lemmaXElement)
                          let inf = new LemmaInflectionElement(lemmaXElement)
                          let variants = new LemmaVariantsElement(lemmaXElement)
                          let access = new LemmaAccessoryDataElement(lemmaXElement)

                          let infForm = new LemmaInflectedFormElement(lemmaXElement)
                          let infFormCat = new LemmaInflectedCategoryElement(lemmaXElement)
                          let tableRowPres = new LemmaTableRowElement(lemmaXElement)

                          where lemma != null

                          let filename = lemmaXElement.Attributes("filename").FirstOrDefault()

                          select new LemmaDocument(Guid.NewGuid().ToString())
                          {
                              LemmaId = lemma.LemmaId,
                              LemmaOrtography = lemma.LemmaOrtography,
                              LemmaLanguage = lemma.LemmaLanguage,
                              LemmaPos = new LemmaPos
                              {
                                  PosNameDan = pos.PosNameDan,
                                  PosNameEng = pos.PosNameEng,
                                  PosNameGyl = pos.PosNameGyl,
                                  PosNameLat = pos.PosNameLat,
                                  PosShortNameDan = pos.PosShortNameDan,
                                  PosShortNameEng = pos.PosShortNameEng,
                                  PosShortNameGyl = pos.PosShortNameGyl,
                                  PosShortNameLat = pos.PosShortNameLat,
                              },
                              LemmaGender = new LemmaGender
                              {
                                  GenNameDan = gen.GenNameDan,
                                  GenNameEng = gen.GenNameEng,
                                  GenNameGyl = gen.GenNameGyl,
                                  GenNameLat = gen.GenNameLat,
                                  GenShortNameDan = gen.GenShortNameDan,
                                  GenShortNameEng = gen.GenShortNameEng,
                                  GenShortNameGyl = gen.GenShortNameGyl,
                                  GenShortNameLat = gen.GenShortNameLat,

                              },
                              LemmaInflection = new LemmaInflection
                              {
                                  SearchableParadigms = new List<LemmaSearchableParadigm>
                                  {
                                        new LemmaSearchableParadigm
                                        {
                                            LemmaInflectedForms = new List<LemmaInflectedForm>
                                            {
                                                new LemmaInflectedForm
                                                {
                                                    LeaveOut = infForm.InflectedFormLeaveOut,
                                                    InflectedFormCategory = new LemmaInflectedFormCategory
                                                    {
                                                        InfCatNameDan = infFormCat.InfCatNameDan,
                                                        InfCatNameEng = infFormCat.InfCatNameEng,
                                                        InfCatNameGyl = infFormCat.InfCatNameGyl,
                                                        InfCatNameLat = infFormCat.InfCatNameLat,
                                                        InfCatShortNameDan = infFormCat.InfCatShortNameDan,
                                                        InfCatShortNameEng = infFormCat.InfCatShortNameEng,
                                                        InfCatShortNameGyl = infFormCat.InfCatShortNameGyl,
                                                        InfCatShortNameLat = infFormCat.InfCatShortNameLat,
                                                    },
                                                    InflectedFormCompactForm = infForm.InfCompactForm,
                                                    InflectedFormFullForm = infForm.InfFullForm,
                                                }
                                            }
                                        }
                                  },
                                  CompactPresentation = inf.LemmaCompactPresentation,
                                  TablePresentations = new List<LemmaTablePresentation>
                                  {
                                      new LemmaTablePresentation
                                      {
                                          LemmaTpRows = new List<LemmaTpRow>
                                          {
                                              new LemmaTpRow
                                              {
                                                  LemmaTpRowCellses = new List<LemmaTpRowCells>
                                                  {
                                                      new LemmaTpRowCells
                                                      {
                                                          CellName = tableRowPres.TpCellName,
                                                          CellType = tableRowPres.TpCellTyper
                                                      }
                                                  }
                                              }
                                          }
                                      }
                                  }
                              },

                              LemmaVariants = new LemmaVariants
                              {
                                  LemmaVariantsRefPos = variants.LemRefLemPos,
                                  LemmaVariantsRefRef = variants.LemRefLemRef
                              },

                              LemmaMeAsFirst = lemma.MeFirstComp,
                              LemmaMeAsLast = lemma.MeLastComp,

                              LemmaAccessoryData = new LemmaAccessoryData
                              {
                                  CategoryDan = access.AcDataCatDan,
                                  CategoryEng = access.AcDataCatEng,
                                  
                                  LemmaAccessDataReferencesRefs = new List<LemmaReference>
                                  {
                                      new LemmaReference
                                      {
                                          LemmaPos = access.AcDataRefLemPos,
                                          LemmaRef = access.AcDataRefLemRef
                                          
                                      }
                                  },
                              }
                          };

            return entries;
        }

        public override IEnumerable<LemmaDocument> ParseXml(IEnumerable<string> xmlFilesToImport)
        {
            var elements = StreamElements(xmlFilesToImport);
            return ParseXml(elements);
        }

        protected override string NodeName => "lemma";
    }
}