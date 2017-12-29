using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using xave.com.generator.cus;
using xave.com.generator.cus.Voca;
using xave.web.generator.helper.Util;

namespace xave.web.generator.helper.Logic
{
    [Serializable]
    internal class BodyExtractLogic
    {
        #region :  Members
        //private POCD_MT000040Entry[] entrys;
        //private POCD_MT000040Observation observation;
        //private POCD_MT000040Act act;
        //private POCD_MT000040Encounter encounter;
        //private POCD_MT000040Organizer organizer;
        //private POCD_MT000040Procedure procedure;
        //private POCD_MT000040ObservationMedia observationMedia;
        //private POCD_MT000040RegionOfInterest regionOfInterest;
        //private POCD_MT000040SubstanceAdministration substanceAdministration;
        //private POCD_MT000040Supply supply;

        //private string effectiveTimeValue;
        //private string lowTimeValue;
        //private string highTimeValue;
        //private string period;

        private List<string> tableHead;
        private List<string> tableBody;
        private List<string> values;
        private List<string> textValue;

        private StrucDocTh[] thArray;
        //private DescriptionObject description;
        #endregion

        /// <summary>
        /// Extract Problem List Section
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        internal List<ProblemObject> ExtractProblems(POCD_MT000040Section section)
        {
            textValue = new List<string>();
            ParsingNarrtiveBlock(section);

            if (section.entry != null)
            {
                int i = 0; // 인덱스 지정
                List<ProblemObject> returnobj = new List<ProblemObject>();
                foreach (POCD_MT000040Act act in section.entry.Select(s => s.Item).OfType<POCD_MT000040Act>())
                {
                    ProblemObject obj = new ProblemObject();
                    obj.StartDate = act.effectiveTime != null ? act.effectiveTime.value : null;

                    if (act.effectiveTime.Items != null) //time Extract
                    {
                        List<string> times = act.effectiveTime.Items.OfType<IVXB_TS>().Select(s => s.value).ToList();
                        obj.StartDate = times != null ? times.FirstOrDefault() : null;
                        //obj.EndDate = times != null ? times.LastOrDefault() : null;                        
                    }
                    if (act.entryRelationship != null)
                    {
                        foreach (ANY any in act.entryRelationship.Select(s => s.Item).OfType<POCD_MT000040Observation>().Where(w => w.value != null).Select(p => p.value).SingleOrDefault())
                        {
                            var code = (any as CD);
                            obj.ProblemCode = code.code;
                            obj.ProblemName = code.displayName;
                            obj.KostomCodes = code.translation != null && code.translation.Any() && code.translation.Where(w => w.codeSystem == OID.KOSTOM).Any() ?
                                code.translation.Where(w => w.codeSystem == OID.KOSTOM).Select(s => new KostomObject() { Code = s.code, DisplayName = s.displayName }).ToArray() : null;
                        }
                    }

                    if (tableBody != null)
                    {
                        textValue = tableBody[i].Split('|').ToList();
                        obj.ProblemName_KOR = !string.IsNullOrEmpty(textValue.ElementAtOrDefault(2)) ?
                            textValue.ElementAtOrDefault(2).Replace("(", "").Replace(")", "").Replace(obj.ProblemName, "").Trim() : null;
                        obj.AcuityScale = textValue.ElementAtOrDefault(3);
                    }

                    returnobj.Add(obj);
                    i++;
                }
                return returnobj;
            }
            else return null;
        }

        internal List<MedicationObject> ExtractMedications(POCD_MT000040Section section, string type = null)
        {
            textValue = new List<string>();
            ParsingNarrtiveBlock(section);

            if (section.entry != null)
            {
                int i = 0; // 인덱스 지정
                List<MedicationObject> retunObj = new List<MedicationObject>();
                foreach (POCD_MT000040SubstanceAdministration substanceAdministration in section.entry.Select(s => s.Item).OfType<POCD_MT000040SubstanceAdministration>())
                {
                    MedicationObject obj = new MedicationObject();

                    obj.DoseQuantity = substanceAdministration.doseQuantity != null ? substanceAdministration.doseQuantity.value : null;
                    obj.DoseQuantityUnit = substanceAdministration.doseQuantity != null ? substanceAdministration.doseQuantity.unit : null;
                    obj.RepeatNumber = substanceAdministration.repeatNumber != null ? substanceAdministration.repeatNumber.value : null;
                    obj.Usage = substanceAdministration.text != null && substanceAdministration.text.Text != null ? substanceAdministration.text.Text.ElementAtOrDefault(0) : null;

                    if (substanceAdministration.effectiveTime != null)
                    {
                        foreach (SXCM_TS effectiveTime in substanceAdministration.effectiveTime)
                        {
                            switch (effectiveTime.GetType().Name)
                            {
                                case "IVL_TS":
                                    IVL_TS temp = effectiveTime as IVL_TS;
                                    obj.StartDate = temp.value;
                                    if (temp.Items != null)
                                    {
                                        List<string> times = temp.Items.OfType<IVXB_TS>().Select(s => s.value).ToList();
                                        obj.StartDate = times != null ? times.FirstOrDefault() : null;
                                        //obj.EndDate = times != null ? times.LastOrDefault() : null;                                        
                                    }
                                    break;
                                case "PIVL_TS":
                                    PIVL_TS pivl_ts = effectiveTime as PIVL_TS;
                                    //obj.Period = pivl_ts.period.value;
                                    obj.Period = pivl_ts.period != null ? pivl_ts.period.value : null;
                                    break;
                            }
                        }
                    }
                    if (substanceAdministration.consumable != null && substanceAdministration.consumable.manufacturedProduct != null && substanceAdministration.consumable.manufacturedProduct.Item != null)
                    {
                        if (substanceAdministration.consumable.manufacturedProduct.Item.GetType().Name == "POCD_MT000040Material")
                        {
                            var material = substanceAdministration.consumable.manufacturedProduct.Item as POCD_MT000040Material;
                            obj.MedicationCode = material.code != null ? material.code.code : null;
                            obj.MedicationName = material.code != null ? material.code.displayName : null;
                            obj.MajorComponentCode = material.code.translation != null ? material.code.translation[0].code : null;
                            obj.MajorComponent = material.code.translation != null ? material.code.translation[0].displayName : null;
                        }
                    }

                    if (tableBody != null && !string.IsNullOrEmpty(type) && type == OID.EMS_KR)
                    {
                        textValue = tableBody[i].Split('|').ToList();
                        obj.BeginningDate = !string.IsNullOrEmpty(textValue.ElementAtOrDefault(1)) ? Regex.Replace(textValue.ElementAtOrDefault(1), @"\D", "") : null;
                    }

                    retunObj.Add(obj);
                    i++;
                }
                return retunObj;
            }
            else
            {
                return null;
            }
        }

        internal List<ProcedureObject> ExtractProcedure(POCD_MT000040Section section, string type = null)
        {
            textValue = new List<string>();
            ParsingNarrtiveBlock(section);

            if (section.entry != null)
            {
                int i = 0; // 인덱스 지정
                List<ProcedureObject> returnObj = new List<ProcedureObject>();
                foreach (var item in section.entry.Select(s => s.Item).OfType<POCD_MT000040Procedure>())
                {
                    var obj = new ProcedureObject();
                    obj.Date = item.effectiveTime != null ? item.effectiveTime.value : null;
                    obj.ProcedureCode_ICD9CM = item.code != null ? item.code.code : null;
                    obj.ProcedureName_ICD9CM = item.code != null ? item.code.displayName : null;
                    obj.KostomCodes = item.code.translation != null ? item.code.translation.Select(s => new KostomObject() { Code = s.code, DisplayName = s.displayName }).ToArray() : null;

                    if (tableBody != null)
                    {
                        textValue = tableBody[i].Split('|').ToList();
                        obj.PostDiagnosisName = textValue.ElementAtOrDefault(2);
                        obj.Anesthesia = textValue.ElementAtOrDefault(3);
                    }
                    returnObj.Add(obj);
                    i++;
                }
                return returnObj;
            }
            else
            {
                if (type == OID.EMS_KR) // 진료회신서
                {
                    var returnObj = new List<ProcedureObject>();
                    foreach (var item in tableBody)
                    {
                        var obj = new ProcedureObject();
                        textValue = item.Split('|').ToList();
                        if (!string.IsNullOrEmpty(textValue.ElementAtOrDefault(0)))
                        {
                            obj.Date = Regex.Replace(textValue.ElementAtOrDefault(0), @"\D", "");
                        }
                        obj.Text = textValue.ElementAtOrDefault(1);
                        returnObj.Add(obj);
                    }
                    return returnObj;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Extract Plan of Treatment
        /// </summary>
        /// <param name="section">POCD_MT000040Section</param>
        /// <param name="type">DocumentType</param>
        /// <returns>PlanOfTreatmentObject</returns>
        internal PlanOfTreatmentObject ExtractPlanOfTreatment(POCD_MT000040Section section)
        {
            textValue = new List<string>();
            ParsingNarrtiveBlock(section);
            if (tableBody != null)
            {
                PlanOfTreatmentObject returnobj = new PlanOfTreatmentObject();
                textValue = tableBody[0].Split('|').ToList();
                if (!string.IsNullOrEmpty(textValue.ElementAtOrDefault(0)))
                {
                    returnobj.PlannedDate = Regex.Replace(textValue.ElementAtOrDefault(0), @"\D", "");
                }
                returnobj.Text = textValue.ElementAtOrDefault(1);
                return returnobj;
            }
            else return null;
        }

        internal List<LaboratoryTestObject> ExtractLaboratoryTest(POCD_MT000040Section section, string type = null)
        {
            if (section.entry != null)
            {
                List<LaboratoryTestObject> returnobj = new List<LaboratoryTestObject>();
                foreach (POCD_MT000040Organizer organizer in section.entry.Select(s => s.Item).OfType<POCD_MT000040Organizer>())
                {
                    var obj = new LaboratoryTestObject();
                    obj.Date = organizer.effectiveTime != null ? organizer.effectiveTime.value : null;
                    obj.LabType = organizer.id != null && organizer.id.Any() && !string.IsNullOrEmpty(organizer.id[0].extension) ?
                        (LaboratoryType)Enum.Parse(typeof(LaboratoryType), organizer.id[0].extension, true) : LaboratoryType.None;

                    string code = organizer.code != null ? organizer.code.code : null;
                    string displayName = organizer.code != null ? organizer.code.displayName : null;
                    obj.KostomCodes = organizer.code.translation != null ?
                        organizer.code.translation.Select(s => new KostomObject() { Code = s.code, DisplayName = s.displayName }).ToArray() : null;

                    switch (obj.LabType)
                    {
                        case LaboratoryType.Specimen:
                            obj.EntryCode = code;
                            obj.EntryName = displayName;
                            break;
                        case LaboratoryType.Pathology:
                        case LaboratoryType.Radiology:
                        case LaboratoryType.Functional:
                            obj.TestCode = code;
                            obj.TestName = displayName;
                            break;
                        case LaboratoryType.None:
                        default:
                            break;
                    }

                    if (organizer.component != null)
                    {
                        foreach (POCD_MT000040Observation observation in organizer.component.Select(s => s.Item).OfType<POCD_MT000040Observation>())
                        {
                            obj.TestName = observation.text != null && observation.text.Text != null && obj.LabType == LaboratoryType.Specimen ? observation.text.Text.ElementAtOrDefault(0) : obj.TestName;
                            if (observation.value != null)
                            {
                                observation.value.OfType<PQ>().All(d => { obj.ResultValue = d.value; return true; });
                                observation.value.OfType<ST>().All(d => { obj.ResultValue = d.Text != null ? d.Text.ElementAtOrDefault(0) : null; return true; });
                            }
                            if (observation.referenceRange != null && observation.referenceRange.Any(w => w.observationRange != null))
                            {
                                ST st = observation.referenceRange.ElementAtOrDefault(0).observationRange.value as ST;
                                obj.Reference = st != null && st.Text != null ? st.Text.ElementAtOrDefault(0) : null;
                            }
                        }
                    }
                    returnobj.Add(obj);
                }

                #region : 영상검사 관련 추가 - 170907
                if (returnobj != null && returnobj.Where(w => w.LabType == LaboratoryType.Radiology).Any())
                {
                    #region: 전원소견서
                    if (type == OID.EMS_KR)
                    {
                        int i = 0;
                        foreach (var item in returnobj.Where(w => w.LabType == LaboratoryType.Radiology))
                        {
                            var strucdocItem = section.text.Items.OfType<StrucDocList>() != null ? section.text.Items.OfType<StrucDocList>().FirstOrDefault().item : null;
                            foreach (var strucdoc in strucdocItem.Where(w => w.Text != null && w.Text[0].Contains("영상검사")))
                            {
                                var table = strucdoc.Items.OfType<StrucDocTable>().FirstOrDefault();
                                var trArray = table != null ? table.tbody.FirstOrDefault().tr : null;
                                var td = trArray != null ? trArray[i].Items.OfType<StrucDocTd>().LastOrDefault() : null;
                                item.OffLineYN = td != null && td.Text != null && td.Text[0].ToUpper().Contains("TRUE") ? true : false;
                            }
                            i++;
                        }
                    }
                    #endregion
                    #region: 기타 서식
                    else
                    {
                        if (returnobj.Where(w => w.LabType == LaboratoryType.Radiology).Any())
                        {
                            var strucdocItems = section.text.Items.OfType<StrucDocList>() != null ? section.text.Items.OfType<StrucDocList>().FirstOrDefault().item : null;
                            var strucdocItem = strucdocItems.Where(w => w.Text != null && w.Text[0].Trim().Contains("영상검사")).FirstOrDefault();
                            var table = strucdocItem != null ? strucdocItem.Items.OfType<StrucDocTable>().FirstOrDefault() : null;
                            var trArray = table != null && table.tbody != null && table.tbody.Any() ? table.tbody.FirstOrDefault().tr : null;

                            if (trArray != null && trArray.Any())
                            {
                                int i = 0;
                                foreach (var item in returnobj.Where(w => w.LabType == LaboratoryType.Radiology))
                                {
                                    var td = trArray[i].Items.OfType<StrucDocTd>().LastOrDefault();
                                    var linkHtml = td != null && td.Items != null && td.Items.OfType<StrucDocLinkHtml>().Any() ? td.Items.OfType<StrucDocLinkHtml>().FirstOrDefault() : null;
                                    if (linkHtml != null && linkHtml.href != null && !string.IsNullOrEmpty(linkHtml.href))
                                    {
                                        item.PacsURL = linkHtml.href;
                                        item.AccessionNumber = linkHtml.rel;
                                        item.KosUid = linkHtml.rev;
                                        //if (!string.IsNullOrEmpty(item.PacsURL))
                                        //{
                                        //    // ex) http://himeserver.irm.kr:8080/hime-view/dicomWebView.html?kos_uid=1.2.410.100110.10.31100813.20170924151053&access_token_key=null 
                                        //    string[] array = item.PacsURL.Split('?');
                                        //    item.WebPacsBaseURL = array != null && array.Any() ? array.FirstOrDefault() : null;
                                        //    foreach (var value in array.Where(w => w.Contains("kos_uid")))
                                        //    {
                                        //        string[] values = value.Split('&');
                                        //        item.KosUid = values != null && values.Any() ? values.FirstOrDefault().Replace("kos_uid=", "") : null;
                                        //    }
                                        //}
                                    }
                                    i++;
                                }
                            }
                        }
                    }
                    #endregion
                }
                #endregion

                return returnobj;
            }
            else return null;
        }

        /// <summary>
        /// Extract Allergies
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        internal List<AllergyObject> ExtractAllergies(POCD_MT000040Section section)
        {
            textValue = new List<string>();
            ParsingNarrtiveBlock(section);

            if (section.entry != null)
            {
                int i = 0;
                var returnobj = new List<AllergyObject>();
                foreach (POCD_MT000040Act act in section.entry.Select(s => s.Item).OfType<POCD_MT000040Act>())
                {
                    foreach (POCD_MT000040Observation observation in act.entryRelationship.Select(s => s.Item).OfType<POCD_MT000040Observation>())
                    {
                        AllergyObject obj = new AllergyObject();
                        if (observation.effectiveTime.Items != null)
                        {
                            List<string> times = observation.effectiveTime.Items.OfType<IVXB_TS>().Select(s => s.value).ToList();
                            obj.StartDate = times != null ? times.FirstOrDefault() : null;

                            if (observation.participant != null && observation.participant.Any(w => w.participantRole != null) && observation.participant.Any(w => w.participantRole.Item != null))
                            {
                                POCD_MT000040PlayingEntity playingEntity = observation.participant.ElementAtOrDefault(0).participantRole.Item as POCD_MT000040PlayingEntity;
                                obj.Allergy = playingEntity.name != null && playingEntity.name.Any(w => w.Text != null) ?
                                    playingEntity.name.ElementAtOrDefault(0).Text.ElementAtOrDefault(0) : null;
                            }
                            if (observation.value != null)
                            {
                                foreach (CD cd in observation.value.OfType<CD>())
                                {
                                    obj.AllergyTypeCode = cd.code;
                                    obj.AllergyType = cd.displayName;
                                }
                            }
                        }
                        if (tableBody != null)
                        {
                            textValue = tableBody[i].Split('|').ToList();
                            //obj.AllergyType = textValue.ElementAtOrDefault(1);
                            obj.Reaction = textValue.ElementAtOrDefault(3);
                            obj.MedicationName = textValue.ElementAtOrDefault(4);
                            obj.MedicationCode = textValue.ElementAtOrDefault(5);
                            obj.AdverseReaction = textValue.ElementAtOrDefault(6);
                        }

                        returnobj.Add(obj);
                        i++;
                    }
                }
                return returnobj;
            }
            else
            {
                textValue = new List<string>();
                ParsingNarrtiveBlock(section);
                if (tableBody != null)
                {
                    List<AllergyObject> returnobj = new List<AllergyObject>();
                    foreach (string item in tableBody)
                    {
                        AllergyObject obj = new AllergyObject();
                        textValue = item.Split('|').ToList();
                        if (!string.IsNullOrEmpty(textValue.ElementAtOrDefault(0)))
                        {
                            obj.StartDate = Regex.Replace(textValue.ElementAtOrDefault(0), @"\D", "");
                        }
                        obj.AllergyType = textValue.ElementAtOrDefault(1);
                        obj.Allergy = textValue.ElementAtOrDefault(2);
                        obj.Reaction = textValue.ElementAtOrDefault(3);
                        obj.MedicationName = textValue.ElementAtOrDefault(4);
                        obj.MedicationCode = textValue.ElementAtOrDefault(5);
                        obj.AdverseReaction = textValue.ElementAtOrDefault(6);

                        returnobj.Add(obj);
                    }
                    return returnobj;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Extract Vital Sign
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        internal List<VitalSignsObject> ExtractVitalSigns(POCD_MT000040Section section, string type = null)
        {
            textValue = new List<string>();
            ParsingNarrtiveBlock(section);

            if (section.entry != null && section.entry.Any())
            {
                #region : 전원소견서
                if (type == OID.EMS_KR) //전원소견서
                {
                    var returnobj = new List<VitalSignsObject>();
                    var strucdocItem = section.text.Items.OfType<StrucDocList>() != null ? section.text.Items.OfType<StrucDocList>().FirstOrDefault().item : null;
                    var firstStrucdocItem = strucdocItem != null ? strucdocItem.FirstOrDefault() : null;
                    if (firstStrucdocItem != null)
                    {
                        VitalSignsObject vital = new VitalSignsObject();
                        vital.Distinction = DistinctionType.PreProcedure;
                        var table = firstStrucdocItem.Items.OfType<StrucDocTable>().FirstOrDefault();
                        var trArray = table != null ? table.tbody.FirstOrDefault().tr : null;

                        var td1 = trArray != null ? trArray[0].Items.OfType<StrucDocTd>().ElementAtOrDefault(0) : null;
                        vital.AwarenessCondition = td1 != null && td1.Text != null ? td1.Text[0] : null;
                        var td2 = trArray != null ? trArray[0].Items.OfType<StrucDocTd>().ElementAtOrDefault(1) : null;
                        vital.BP_Diastolic = td2 != null && td2.Text != null ? td2.Text[0] : null;
                        var td3 = trArray != null ? trArray[0].Items.OfType<StrucDocTd>().ElementAtOrDefault(2) : null;
                        vital.BP_Systolic = td3 != null && td3.Text != null ? td3.Text[0] : null;
                        var td4 = trArray != null ? trArray[0].Items.OfType<StrucDocTd>().ElementAtOrDefault(3) : null;
                        vital.BodyTemperature = td4 != null && td4.Text != null ? td4.Text[0] : null;
                        var td5 = trArray != null ? trArray[0].Items.OfType<StrucDocTd>().ElementAtOrDefault(4) : null;
                        vital.HeartRate = td5 != null && td5.Text != null ? td5.Text[0] : null;
                        var td6 = trArray != null ? trArray[0].Items.OfType<StrucDocTd>().ElementAtOrDefault(5) : null;
                        vital.ETC = td6 != null && td6.Text != null ? td6.Text[0] : null;
                        returnobj.Add(vital);
                    }
                    var lastStrucdocItem = strucdocItem != null ? strucdocItem.LastOrDefault() : null;

                    if (lastStrucdocItem != null)
                    {
                        VitalSignsObject vital = new VitalSignsObject();
                        vital.Distinction = DistinctionType.PostProcedure;
                        var table = lastStrucdocItem.Items.OfType<StrucDocTable>().FirstOrDefault();
                        var trArray = table != null ? table.tbody.FirstOrDefault().tr : null;

                        var td1 = trArray != null ? trArray[0].Items.OfType<StrucDocTd>().ElementAtOrDefault(0) : null;
                        vital.AwarenessCondition = td1 != null && td1.Text != null ? td1.Text[0] : null;
                        var td2 = trArray != null ? trArray[0].Items.OfType<StrucDocTd>().ElementAtOrDefault(1) : null;
                        vital.BP_Diastolic = td2 != null && td2.Text != null ? td2.Text[0] : null;
                        var td3 = trArray != null ? trArray[0].Items.OfType<StrucDocTd>().ElementAtOrDefault(2) : null;
                        vital.BP_Systolic = td3 != null && td3.Text != null ? td3.Text[0] : null;
                        var td4 = trArray != null ? trArray[0].Items.OfType<StrucDocTd>().ElementAtOrDefault(3) : null;
                        vital.BodyTemperature = td4 != null && td4.Text != null ? td4.Text[0] : null;
                        var td5 = trArray != null ? trArray[0].Items.OfType<StrucDocTd>().ElementAtOrDefault(4) : null;
                        vital.HeartRate = td5 != null && td5.Text != null ? td5.Text[0] : null;
                        var td6 = trArray != null ? trArray[0].Items.OfType<StrucDocTd>().ElementAtOrDefault(5) : null;
                        vital.ETC = td6 != null && td6.Text != null ? td6.Text[0] : null;
                        returnobj.Add(vital);
                    }
                    return returnobj;
                }
                #endregion
                #region : 기타서식
                else
                {
                    var returnobj = new List<VitalSignsObject>();
                    foreach (POCD_MT000040Organizer organizer in section.entry.Select(s => s.Item).OfType<POCD_MT000040Organizer>())
                    {
                        VitalSignsObject obj = new VitalSignsObject();
                        obj.Date = organizer.effectiveTime != null ? organizer.effectiveTime.value : null;
                        foreach (POCD_MT000040Observation observation in organizer.component.Select(s => s.Item).OfType<POCD_MT000040Observation>())
                        {
                            if (observation.code != null)
                            {
                                switch (observation.code.code)
                                {
                                    case "8302-2":
                                        obj.Height = observation.value != null ? ((PQ)observation.value.ElementAtOrDefault(0)).value : null;
                                        break;
                                    case "3141-9":
                                        obj.Weight = observation.value != null ? ((PQ)observation.value.ElementAtOrDefault(0)).value : null;
                                        break;
                                    case "8462-4":
                                        obj.BP_Diastolic = observation.value != null ? ((PQ)observation.value.ElementAtOrDefault(0)).value : null;
                                        break;
                                    case "8480-6":
                                        obj.BP_Systolic = observation.value != null ? ((PQ)observation.value.ElementAtOrDefault(0)).value : null;
                                        break;
                                    case "8310-5":
                                        obj.BodyTemperature = observation.value != null ? ((PQ)observation.value.ElementAtOrDefault(0)).value : null;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        returnobj.Add(obj);
                    }
                    return returnobj;
                }
                #endregion
            }
            else return null;
        }

        /// <summary>
        /// Extract Smoking Status
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        internal SocialHistoryObject ExtractSocialHistory(POCD_MT000040Section section)
        {
            //textValue = new List<string>();
            //ParsingNarrtiveBlock(section);               

            if (section.entry != null && section.entry.Any())
            {
                var returnobj = new SocialHistoryObject();
                foreach (var observation in section.entry.Select(s => s.Item).OfType<POCD_MT000040Observation>().Where(w => w.templateId.Any(a => a.root == EntryOID.SMOKING_STATUS_MEANINGFUL_USE)))
                {
                    returnobj.SmokingStatus = (observation.value.OfType<CD>() != null) ? observation.value.OfType<CD>().FirstOrDefault().displayName : null;
                    returnobj.SmokingStatusCode = (observation.value.OfType<CD>() != null) ? observation.value.OfType<CD>().FirstOrDefault().code : null;
                }
                foreach (var observation in section.entry.Select(s => s.Item).OfType<POCD_MT000040Observation>().Where(w => w.templateId.Any(a => a.root == EntryOID.SOCIAL_HISTORY_OBSERVATION)))
                {
                    if (observation.code != null && observation.code.translation != null)
                    {
                        returnobj.FrequencyCode = (observation.code.translation.ElementAtOrDefault(0) != null) ? observation.code.translation.ElementAtOrDefault(0).code : null;
                        returnobj.Frequency = (observation.code.translation.ElementAtOrDefault(0) != null) ? observation.code.translation.ElementAtOrDefault(0).displayName : null;

                        returnobj.AlcoholConsumptionCode = (observation.code.translation.ElementAtOrDefault(1) != null) ? observation.code.translation.ElementAtOrDefault(1).code : null;
                        returnobj.AlcoholConsumption = (observation.code.translation.ElementAtOrDefault(1) != null) ? observation.code.translation.ElementAtOrDefault(1).displayName : null;

                        returnobj.OverdrinkingCode = (observation.code.translation.ElementAtOrDefault(2) != null) ? observation.code.translation.ElementAtOrDefault(2).code : null;
                        returnobj.Overdrinking = (observation.code.translation.ElementAtOrDefault(2) != null) ? observation.code.translation.ElementAtOrDefault(2).displayName : null;
                    }
                }
                return returnobj;
            }
            else return null;
        }

        /// <summary>
        /// Extract Immunizations (완료)
        /// </summary>        
        /// <param name="section">POCD_MT000040Section</param>
        /// <returns>ImmunizationObject</returns>
        internal List<ImmunizationObject> ExtractImmunizations(POCD_MT000040Section section)
        {
            textValue = new List<string>();
            ParsingNarrtiveBlock(section);

            if (section.entry != null && section.entry.Any())
            {
                int i = 0;
                var returnobj = new List<ImmunizationObject>();
                foreach (POCD_MT000040SubstanceAdministration substanceAdministration in section.entry.Select(s => s.Item).OfType<POCD_MT000040SubstanceAdministration>())
                {
                    var obj = new ImmunizationObject();
                    obj.RepeatNumber = substanceAdministration.repeatNumber != null ? substanceAdministration.repeatNumber.value : null;
                    obj.ImmunizationCode = substanceAdministration.code != null ? substanceAdministration.code.code : null;
                    obj.ImmunizationName = substanceAdministration.code != null ? substanceAdministration.code.displayName : null;
                    if (substanceAdministration.consumable != null && substanceAdministration.consumable.manufacturedProduct != null && substanceAdministration.consumable.manufacturedProduct.Item != null)
                    {
                        var material = substanceAdministration.consumable.manufacturedProduct.Item as POCD_MT000040Material;
                        obj.VaccineName = material.name != null && material.name.Text != null ? material.name.Text.ElementAtOrDefault(0) : null;
                        //if (material.code != null)
                        //{
                        //    obj.ImmunizationCode = material.code.code;
                        //    obj.ImmunizationName = material.code.displayName;
                        //}
                    }
                    foreach (SXCM_TS p in substanceAdministration.effectiveTime)
                    {
                        switch (p.GetType().Name)
                        {
                            case "IVL_TS":
                                IVL_TS temp = p as IVL_TS;
                                obj.Date = temp.value;
                                if (temp.Items != null)
                                {
                                    List<string> times = temp.Items.OfType<IVXB_TS>().Select(s => s.value).ToList();
                                    obj.Date = times != null ? times.FirstOrDefault() : null;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    if (tableBody != null)
                    {
                        textValue = tableBody[i].Split('|').ToList();
                        obj.ImmunizationName = textValue.ElementAtOrDefault(1);
                    }
                    returnobj.Add(obj);
                    i++;
                }
                return returnobj;
            }
            else
            {
                textValue = new List<string>();
                ParsingNarrtiveBlock(section);

                if (tableBody != null)
                {
                    var returnobj = new List<ImmunizationObject>();
                    foreach (var item in tableBody)
                    {
                        var obj = new ImmunizationObject();
                        textValue = item.Split('|').ToList();
                        obj.Date = textValue.ElementAtOrDefault(0);
                        //obj.VaccineCode = textValue[1];
                        obj.ImmunizationName = textValue.ElementAtOrDefault(1);
                        obj.VaccineName = textValue.ElementAtOrDefault(2);
                        //obj.RepeatNumber = textValue[4];
                        returnobj.Add(obj);
                    }
                    return returnobj;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Extract History of infection
        /// </summary>
        /// <param name="section">POCD_MT000040Section</param>
        /// <returns>InfectionObject</returns>
        internal InfectionObject ExtractInfection(POCD_MT000040Section section)
        {
            textValue = new List<string>();
            ParsingNarrtiveBlock(section);
            if (tableBody != null)
            {
                var returnobj = new InfectionObject();
                //textValue = tableBody[0].Split('|').ToList();                
                tableBody.ForEach(d => textValue.AddRange(d.Split('|').ToList()));

                returnobj.InfectionName = textValue.ElementAtOrDefault(0);
                if (!string.IsNullOrEmpty(textValue.ElementAtOrDefault(1)))
                {
                    returnobj.OnsetDate = Regex.Replace(textValue.ElementAtOrDefault(1), @"\D", "");
                }
                if (!string.IsNullOrEmpty(textValue.ElementAtOrDefault(2)))
                {
                    returnobj.ReportedDate = Regex.Replace(textValue.ElementAtOrDefault(2), @"\D", "");
                }
                if (!string.IsNullOrEmpty(textValue.ElementAtOrDefault(4)))
                {
                    returnobj.DiagnosisDate = Regex.Replace(textValue.ElementAtOrDefault(4), @"\D", "");
                }

                returnobj.Classification = textValue.ElementAtOrDefault(3);
                returnobj.AdmissionYN = !string.IsNullOrEmpty(textValue.ElementAtOrDefault(5)) && string.Equals(textValue.ElementAtOrDefault(5), "True") ? true : false;
                returnobj.TestResult = textValue.ElementAtOrDefault(6);
                returnobj.SuspectedArea = textValue.ElementAtOrDefault(7);
                return returnobj;
            }
            else
            {
                return null;
            }
        }

        #region :  Parsing Narrative Block
        /// <summary>
        /// Narrative Block을 Type 별로 Parsing
        /// </summary>
        /// <param name="section"></param>
        private void ParsingNarrtiveBlock(POCD_MT000040Section section)
        {
            tableHead = null;
            tableBody = null;
            values = new List<string>();

            if (section != null && section.text != null)
            {
                StrucDocText textObject = section.text;
                if (textObject.Items != null)
                {
                    foreach (object item in textObject.Items)
                    {
                        switch (item.GetType().Name)
                        {
                            #region : Table
                            case "StrucDocTable": //Table Head                                
                                tableHead = new List<string>();
                                tableBody = new List<string>();
                                StrucDocTable table = item as StrucDocTable;
                                if (table.thead != null && table.thead.tr != null)
                                {
                                    foreach (StrucDocTr trList in table.thead.tr.Where(w => w.Items != null))
                                    {
                                        thArray = trList.Items.OfType<StrucDocTh>().ToArray();
                                        foreach (StrucDocTh th in thArray.Where(w => w.Text != null))
                                        {
                                            tableHead.AddRange(th.Text);
                                        }
                                    }
                                }
                                if (table.tbody != null) //Table Body
                                {
                                    foreach (StrucDocTbody tbody in table.tbody)
                                    {
                                        foreach (StrucDocTr tr in tbody.tr.Where(w => w.Items != null))
                                        {
                                            StringBuilder sb = new StringBuilder();
                                            foreach (StrucDocTd td in tr.Items.OfType<StrucDocTd>())
                                            {
                                                if (td.Text != null)
                                                {
                                                    sb.Append(string.Join(null, td.Text) + "|");
                                                }
                                                else
                                                {
                                                    sb.Append("|");
                                                }
                                            }
                                            if (sb.Length > 0)
                                            {
                                                sb.Remove(sb.ToString().LastIndexOf('|'), 1);
                                            }
                                            tableBody.Add(sb.ToString());
                                        }
                                    }
                                }
                                break;
                            #endregion

                            #region : List
                            case "StrucDocList":
                                StrucDocList list = item as StrucDocList;
                                values = new List<string>();
                                foreach (var docitem in list.item)
                                {
                                    if (docitem.Text != null)
                                    {
                                        values.AddRange(docitem.Text);
                                    }
                                }
                                break;
                            #endregion

                            #region : Paragraph
                            case "StrucDocParagraph":
                                StrucDocParagraph paragraph = item as StrucDocParagraph;
                                if (paragraph.Text != null)
                                {
                                    values.AddRange(paragraph.Text);
                                }
                                else
                                {
                                    values.Add(null);
                                }
                                break;
                            #endregion

                            #region : Content
                            case "StrucDocContent":
                                values = new List<string>();
                                StrucDocContent content = item as StrucDocContent;
                                if (content.Text != null && content.Text.Count() > 0)
                                {
                                    values.AddRange(content.Text);
                                }
                                break;
                            #endregion

                            default:
                                break;
                        }
                    }
                }
                else
                {
                    values = new List<string>();
                    if (textObject.Text != null)
                    {
                        foreach (var textValue in textObject.Text)
                        {
                            values.Add(textValue);
                        }
                    }
                }

            }
        }
        #endregion

        internal string ExtractDescription(POCD_MT000040Section section)
        {
            ParsingNarrtiveBlock(section);
            if (values != null)
            {
                return string.Join("\r\n", values.ToArray());
                //return values[0];
            }
            else
                return null;
        }

        internal void ExtractBppcSection(POCD_MT000040Section section, CDAObject ReturnObject)
        {
            switch (ReturnObject.DocumentInformation.DocumentType)
            {
                case OID.BPPC_CONSENT:
                    var table = section.text.Items.OfType<StrucDocTable>().LastOrDefault();
                    if (table != null && table.tbody != null && table.tbody.Any() && table.tbody.Any(s => s.tr != null) && table.tbody.Any(s => s.tr.Any(w => w.Items != null)))
                    {
                        var exceptDepartment = table.tbody[0].tr[7].Items.OfType<StrucDocTd>().FirstOrDefault().Items.OfType<StrucDocParagraph>();

                        if (exceptDepartment != null && exceptDepartment.Any())
                        {
                            //ReturnObject.Consent.ExceptDepartmentCodeList = exceptDepartment.Select(s => s.Text.ElementAtOrDefault(0)).ToList();
                            ReturnObject.Consent.ExceptDepartmentCodes = exceptDepartment.Select(s => s.Text.ElementAtOrDefault(0)).ToArray();
                        }
                    }

                    var relationship = section.text.Items.OfType<StrucDocParagraph>().Where(w => w.ID == "relationship");
                    if (relationship != null && relationship.Any() && relationship.Any(s => s.Text != null) && relationship.Any(w => w.Text.Any()))
                    {
                        string relationshipValue = relationship.FirstOrDefault().Text.ElementAtOrDefault(0).Split(':').Last().Trim();
                        ReturnObject.Consent.Relationship = CommonExtension.GetValueFromDescription<RelationshipType>(relationshipValue);
                    }
                    break;

                case OID.BPPC_WITHDRAWAL:
                    ParsingNarrtiveBlock(section);
                    var table2 = section.text.Items.OfType<StrucDocTable>().LastOrDefault();
                    if (table2 != null && table2.tbody != null && table2.tbody.Any())
                    {
                        List<string> exceptOrganizationNames = new List<string>();
                        List<string> exceptOrganizationOIDs = new List<string>();

                        if (table2.tbody[0].tr.ElementAtOrDefault(3) != null &&
                            table2.tbody[0].tr.ElementAtOrDefault(3).Items != null &&
                            table2.tbody[0].tr.ElementAtOrDefault(3).Items.OfType<StrucDocTd>().Any())
                        {
                            var exceptOrganization = table2.tbody[0].tr[3].Items.OfType<StrucDocTd>().FirstOrDefault().Items.OfType<StrucDocParagraph>();
                            exceptOrganizationNames = exceptOrganization.Select(s => s.Text.ElementAtOrDefault(0)).ToList();
                            //ReturnObject.Withdrawal.WithdrawalOrganizationList = new List<WithdrawalObject.ExceptOrganizationObject>();
                            List<WithdrawalObject.ExceptOrganizationObject> WithdrawalOrganizationList = new List<WithdrawalObject.ExceptOrganizationObject>();

                            for (int i = 0; i < exceptOrganizationNames.Count; i++)
                            {
                                WithdrawalOrganizationList.Add(new WithdrawalObject.ExceptOrganizationObject() { OrganizationName = exceptOrganizationNames.ElementAtOrDefault(i) });
                            }
                            ReturnObject.Withdrawal.WithdrawalOrganizations = WithdrawalOrganizationList.ToArray();
                        }

                        if (table2.tbody[0].tr.ElementAtOrDefault(4) != null &&
                            table2.tbody[0].tr.ElementAtOrDefault(4).Items != null &&
                            table2.tbody[0].tr.ElementAtOrDefault(4).Items.OfType<StrucDocTd>().Any())
                        {
                            var exceptOrganizationOID = table2.tbody[0].tr[4].Items.OfType<StrucDocTd>().FirstOrDefault().Items.OfType<StrucDocParagraph>();
                            exceptOrganizationOIDs = exceptOrganizationOID.Select(s => s.Text.ElementAtOrDefault(0)).ToList();

                            for (int i = 0; i < exceptOrganizationNames.Count; i++)
                            {
                                ReturnObject.Withdrawal.WithdrawalOrganizations[i].OID = exceptOrganizationOIDs[i];
                            }
                        }

                        if (table2.tbody[0].tr.ElementAtOrDefault(7) != null &&
                            table2.tbody[0].tr.ElementAtOrDefault(7).Items != null &&
                            table2.tbody[0].tr.ElementAtOrDefault(7).Items.OfType<StrucDocTd>().Any())
                        {
                            var exceptDepartment = table2.tbody[0].tr[7].Items.OfType<StrucDocTd>().FirstOrDefault().Items.OfType<StrucDocParagraph>();

                            if (exceptDepartment != null && exceptDepartment.Any())
                            {
                                //ReturnObject.Withdrawal.WithdrawalDepartmentCodeList = exceptDepartment.Select(s => s.Text.ElementAtOrDefault(0)).ToList();
                                ReturnObject.Withdrawal.WithdrawalDepartmentCodes = exceptDepartment.Select(s => s.Text.ElementAtOrDefault(0)).ToList().ToArray();
                            }
                        }

                        var relationship2 = section.text.Items.OfType<StrucDocParagraph>().Where(w => w.ID == "relationship");
                        if (relationship2 != null && relationship2.Any() && relationship2.Any(s => s.Text != null) && relationship2.Any(w => w.Text.Any()))
                        {
                            string relationshipValue = relationship2.FirstOrDefault().Text.ElementAtOrDefault(0).Split(':').Last().Trim();
                            ReturnObject.Withdrawal.Relationship = CommonExtension.GetValueFromDescription<RelationshipType>(relationshipValue);
                        }

                        ReturnObject.Withdrawal.WithdrawalOrganizationReason = tableBody.ElementAtOrDefault(5);
                        ReturnObject.Withdrawal.WithdrawalDepartmentReason = tableBody.ElementAtOrDefault(8);
                        ReturnObject.Withdrawal.WholeWithdrawalReason = tableBody.ElementAtOrDefault(9);
                    }
                    break;
                default:
                    break;
            }
        }

        internal TransferObject ExtractTransfer(POCD_MT000040Section section)
        {
            if (section != null && section.text != null)
            {
                textValue = new List<string>();
                ParsingNarrtiveBlock(section);

                if (tableBody != null)
                {
                    var returnobj = new TransferObject();
                    textValue = tableBody[0].Split('|').ToList();
                    if (!string.IsNullOrEmpty(textValue.ElementAtOrDefault(0)))
                    {
                        returnobj.ArrivalTime = Regex.Replace(textValue.ElementAtOrDefault(0), @"\D", "");
                    }
                    if (!string.IsNullOrEmpty(textValue.ElementAtOrDefault(1)))
                    {
                        returnobj.TransferDate = Regex.Replace(textValue.ElementAtOrDefault(1), @"\D", "");
                    }
                    returnobj.Transportaion = textValue.ElementAtOrDefault(2);
                    returnobj.CarNumber = textValue.ElementAtOrDefault(3);
                    returnobj.Practitioner = textValue.ElementAtOrDefault(4);
                    returnobj.ReasonForTransfer = textValue.ElementAtOrDefault(5);

                    return returnobj;
                }
                else
                {
                    return null;
                }
            }
            else return null;
        }

        internal SignatureObject ExtractSignature(POCD_MT000040Section section)
        {
            if (section != null && section.entry != null)
            {
                SignatureObject returnobj = new SignatureObject();
                foreach (var observationMedia in section.entry.Select(s => s.Item).OfType<POCD_MT000040ObservationMedia>())
                {
                    foreach (var observation in observationMedia.entryRelationship.Select(s => s.Item).OfType<POCD_MT000040Observation>())
                    {
                        if (observation.value != null && observation.value.OfType<ED>().Any() && observation.value.OfType<ED>().FirstOrDefault().Text != null)
                        {
                            ED ed = observation.value.OfType<ED>().FirstOrDefault();
                            returnobj.ImageData = ed.Text.ElementAtOrDefault(0);
                            returnobj.MediaType = ed.mediaType;
                            return returnobj;
                        }
                        else return null;
                    }
                }
                return returnobj;
            }
            else return null;
        }

        internal ImageReadingObject ExtractImageReading(POCD_MT000040Section section)
        {
            if (section != null && section.text != null && section.text.Items.OfType<StrucDocList>() != null)
            {
                var strucDocList = section.text.Items.OfType<StrucDocList>().FirstOrDefault();
                var strucDocItems = strucDocList != null ? strucDocList.item : null;
                if (strucDocItems != null && strucDocItems.Any())
                {
                    ImageReadingObject returnObj = new ImageReadingObject();
                    returnObj.PerformDate = strucDocItems.ElementAtOrDefault(0) != null && strucDocItems.ElementAtOrDefault(0).Text != null ?
                        Regex.Replace(strucDocItems.ElementAtOrDefault(0).Text[0].Replace(returnObj.PerformDateValue, "").Trim(), @"\D", "") : null;
                    returnObj.ReadingDate = strucDocItems.ElementAtOrDefault(1) != null && strucDocItems.ElementAtOrDefault(1).Text != null ?
                        Regex.Replace(strucDocItems.ElementAtOrDefault(1).Text[0].Replace(returnObj.ReadingDateValue, "").Trim(), @"\D", "") : null;
                    returnObj.DoctorName = strucDocItems.ElementAtOrDefault(2) != null && strucDocItems.ElementAtOrDefault(2).Text != null ?
                        strucDocItems.ElementAtOrDefault(2).Text[0].Replace(returnObj.DoctorNameValue, "").Trim() : null;
                    returnObj.ImageURL = strucDocItems.ElementAtOrDefault(3) != null && strucDocItems.ElementAtOrDefault(3).Text != null ?
                        strucDocItems.ElementAtOrDefault(3).Text[0].Replace(returnObj.ImageURLValue, "").Trim() : null;
                    returnObj.StudyUID = strucDocItems.ElementAtOrDefault(4) != null && strucDocItems.ElementAtOrDefault(4).Text != null ?
                        strucDocItems.ElementAtOrDefault(4).Text[0].Replace(returnObj.StudyUIDValue, "").Trim() : null;
                    returnObj.SeriesUID = strucDocItems.ElementAtOrDefault(5) != null && strucDocItems.ElementAtOrDefault(5).Text != null ?
                        strucDocItems.ElementAtOrDefault(5).Text[0].Replace(returnObj.SeriesUIDValue, "").Trim() : null;
                    returnObj.SopUID = strucDocItems.ElementAtOrDefault(6) != null && strucDocItems.ElementAtOrDefault(6).Text != null ?
                        strucDocItems.ElementAtOrDefault(6).Text[0].Replace(returnObj.SopUIDValue, "").Trim() : null;
                    returnObj.Conclusion = strucDocItems.ElementAtOrDefault(7) != null && strucDocItems.ElementAtOrDefault(7).Text != null ?
                        strucDocItems.ElementAtOrDefault(7).Text[0].Replace(returnObj.ConclusionValue, "").Trim() : null; ;
                    returnObj.TestCode = strucDocItems.ElementAtOrDefault(8) != null && strucDocItems.ElementAtOrDefault(8).Text != null ?
                        strucDocItems.ElementAtOrDefault(8).Text[0].Replace(returnObj.TestCodeValue, "").Trim() : null;
                    returnObj.TestName = strucDocItems.ElementAtOrDefault(9) != null && strucDocItems.ElementAtOrDefault(9).Text != null ?
                        strucDocItems.ElementAtOrDefault(9).Text[0].Replace(returnObj.TestNameValue, "").Trim() : null;
                    return returnObj;
                }
                else return null;
            }
            else return null;
        }
    }
}
