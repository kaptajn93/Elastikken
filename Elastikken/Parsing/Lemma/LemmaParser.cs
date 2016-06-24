using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Nest;
using NLog;

namespace Elastikken.Parsing.Lemma
{
    public class LemmaParser : ParserBase<LemmaDocument>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public override IEnumerable<LemmaDocument> ParseXml(IEnumerable<XElement> elements)
        {
            try
            {
                var entries = from lemmaXElement in elements
                              let lemma = new LemmaElement(lemmaXElement)
                              let pos = new LemmaPosElement(lemmaXElement)
                              let gen = new LemmaGenderElement(lemmaXElement)
                              let inf = new LemmaInflectionElement(lemmaXElement)
                              let va = new LemmaVariantsElement(lemmaXElement)

                              let pro = new LemmaPronunciationAllElement(lemmaXElement)
                              let ill = new LemmaIllustrationElement(lemmaXElement)
                              let abb = new LemmaLemmaAbbreviationForElement(lemmaXElement)
                    

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
                                  LemmaGender =
                                                new LemmaGender
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
                                  LemmaInflection =
                                                new LemmaInflection
                                                {
                                                    SearchableParadigms = inf.LemmaSearchableParadigms?.Select(sp =>
                                                        new LemmaSearchableParadigm
                                                        {
                                                            LemmaInflectedForms = sp.LemmaInflectedForms?.Select(li =>
                                                                new LemmaInflectedForm
                                                                {
                                                                    LeaveOut = li.InflectedFormLeaveOut,
                                                                    InflectedFormCategories = li.LemmaInflectedCategorys.Select(infc =>
                                                                        new LemmaInflectedFormCategory
                                                                        {
                                                                            InfCatNameDan = infc.InfCatNameDan,
                                                                            InfCatNameEng = infc.InfCatNameEng,
                                                                            InfCatNameGyl = infc.InfCatNameGyl,
                                                                            InfCatNameLat = infc.InfCatNameLat,
                                                                            InfCatShortNameDan = infc.InfCatShortNameDan,
                                                                            InfCatShortNameEng = infc.InfCatShortNameEng,
                                                                            InfCatShortNameGyl = infc.InfCatShortNameGyl,
                                                                            InfCatShortNameLat = infc.InfCatShortNameLat
                                                                        }).ToList(),
                                                                    InflectedFormCompactForm = li.InfCompactForm,
                                                                    InflectedFormFullForm = li.InfFullForm,
                                                                }).ToList()
                                                        }).ToList(),
                                                    CompactPresentation = inf.LemmaCompactPresentation,
                                                    TablePresentations = inf.LemmaTablePresentations?.Select(tp =>
                                                        new LemmaTablePresentation
                                                        {
                                                            LemmaTpRows = tp.LemmaTableRow?.Select(tr =>
                                                                new LemmaTpRow
                                                                {
                                                                    LemmaTpRowCellses = tr.LemmaRowCells?.Select(rc =>
                                                                        new LemmaTpRowCells
                                                                        {
                                                                            CellName = rc.TpCellName,
                                                                            CellType = rc.TpCellTyper
                                                                        }).ToList()
                                                                }).ToList()
                                                        }).ToList()
                                                },
                                  LemmaVariants = new LemmaVariants
                                  {
                                      LemmaVariantsRefPos = va.LemRefLemPos,
                                      LemmaVariantsRefRef = va.LemRefLemRef
                                  },
                                  LemmaMeAsFirst = lemma.MeFirstComp,
                                  LemmaMeAsLast = lemma.MeLastComp,

                                  LemmaAccessoryDatas = lemma.LemmaAccessoryData.Select(ac =>
                                      new LemmaAccessoryData()
                                      {
                                          CategoryDan = ac.AcDataCatDan,
                                          CategoryEng = ac.AcDataCatEng,

                                          LemmaAccessDataReferencesRefs = ac.AccessoryDataRefs?.Select(adr =>
                                              new LemmaReference
                                              {
                                                  LemmaPos = adr.AcDataRefLemPos,
                                                  LemmaRef = adr.AcDataRefLemRef
                                              }).ToList()
                                      }).ToList(),



                                  Usage = new LemmaUsage
                                  {
                                      Usage = lemma.LemmaUsage
                                  },
                                  PronuciatetionAll = new LemmaPronuciatetionAll
                                  {
                                      ProVariants = pro.LemmaPronunciationVariant?.Select(pv =>
                                      new LemmaPronuciatetionVariant
                                      {
                                          VariantDescriptions = pv.LemmaVariantDescription?.Select(vd =>
                                          new LemmaVariantDescription
                                          {
                                              LangVariants = vd.LemmaLangVariants?.Select(lv =>
                                              new LemmaLangVariant
                                              {
                                                  LangVariant = lv.LemmaLangVar
                                              }).ToList(),

                                              TechLang = vd.LemmaTechLang,
                                              VariantsDescription = vd.LemmaVarDescription,
                                              LemmaProDescription = vd.LemmaProductiveDescription

                                          }).ToList(),
                                          LemmaPronunciations = pv.LemmaPronunciation?.Select(lp =>
                                              new LemmaPronunciation
                                              {
                                                  IPA = lp.LemmaIPA,
                                                  IPApart = lp.LemmaIPApart,
                                                  SoundFile = lp.LemmaSoundFile,
                                                  Stress = lp.LemmaStress
                                              }).ToList()

                                      }).ToList()
                                  },

                                  Illustration = new LemmaIllustration
                                  {
                                      IllustrationFiles = ill.LemmaillFiles?.Select(lf =>
                                      new LemmaIllustrationFile
                                      {
                                          IllFileRef = lf.LemmaIllFileRef,
                                          IllFileType = lf.LemmaIllFileType
                                      }).ToList()
                                  },

                                  AbbreviationFor = new LemmaAbbreviationFor
                                  {
                                      LemmablindRefs = abb.BlindRef?.Select(br =>
                                      new LemmablindRef
                                      {
                                          LemmaBlindRef = br.BlindRef
                                      }).ToList(),
                                      AbbrevationRefs = abb.LemmaAbbRef?.Select(lr =>
                                      new LemmaAbbrevationRef
                                      {
                                          AbbDescRef = lr.AbbLemmaDescRef,
                                          AbbPos = lr.AbbLemmaPos,
                                          AbbRef = lr.AbbLemmaRef
                                      }).ToList()
                                  },

                                  IllustrationsCount = ill.LemmaillFiles?.Count ?? 0,
                                  SoundfilesCount = pro.LemmaPronunciationVariant?.Select(pv => pv.LemmaPronunciation?.Select(pr => 
                                        pr.LemmaSoundFile)).Count() ?? 0,


                                  

                              };
                return entries;
            }
            catch (Exception ex)
            {
                logger.Error(ex);

                throw;
            }
        }



        public override IEnumerable<LemmaDocument> ParseXml(IEnumerable<string> xmlFilesToImport)
        {
            var elements = StreamElements(xmlFilesToImport);
            return ParseXml(elements);
        }

        protected override string NodeName => "lemma";

    }


}