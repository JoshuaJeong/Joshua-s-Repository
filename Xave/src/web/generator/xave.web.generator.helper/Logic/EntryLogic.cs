using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using xave.com.generator.cus;
using xave.com.generator.cus.StructureSetModel;
using xave.com.generator.cus.Voca;
using xave.web.generator.helper.Util;
//using System.Threading.Tasks;

namespace xave.web.generator.helper.Logic
{
    [Serializable]
    internal class EntryLogic : DataTypeLogic
    {
        #region ::  Properties
        private static string ErrorLogDirectory = System.Configuration.ConfigurationManager.AppSettings["ErrorLogDirectory"];
        internal POCD_MT000040Entry Entry { get; set; }
        internal List<POCD_MT000040Entry> EntryList { get; set; }
        internal POCD_MT000040EntryRelationship EntryRelationship { get; set; }
        internal List<POCD_MT000040EntryRelationship> EntryRelationshipList { get; set; }
        internal POCD_MT000040RegionOfInterest RegionOfInterest { get; set; }
        internal POCD_MT000040ObservationMedia ObservationMedia { get; set; }
        internal POCD_MT000040Supply Supply { get; set; }
        internal POCD_MT000040Procedure Procedure { get; set; }
        internal POCD_MT000040Encounter Encounter { get; set; }
        internal POCD_MT000040Observation Observation { get; set; }
        internal POCD_MT000040Organizer Organizer { get; set; }
        internal POCD_MT000040Act Act { get; set; }
        internal POCD_MT000040SubstanceAdministration SubstanceAdministration { get; set; }
        internal POCD_MT000040Author Author { get; set; }
        internal POCD_MT000040Informant12 Informant { get; set; }
        internal POCD_MT000040Subject Subject { get; set; }
        internal POCD_MT000040Performer2 Performer { get; set; }
        internal POCD_MT000040Consumable Consumable { get; set; }
        internal POCD_MT000040Specimen Specimen { get; set; }
        internal POCD_MT000040Product Product { get; set; }
        internal POCD_MT000040Participant2 Participant { get; set; }
        //Role(Entry) properties
        internal POCD_MT000040AssignedEntity AssignedEntity { get; set; }
        internal POCD_MT000040SpecimenRole SpecimenRole { get; set; }
        internal POCD_MT000040ManufacturedProduct ManufacturedProduct { get; set; }
        internal POCD_MT000040ParticipantRole ParticipantRole { get; set; }
        //Entity(Entry) properties
        internal POCD_MT000040Organization Organization { get; set; }
        internal POCD_MT000040LabeledDrug LabeledDrug { get; set; }
        internal POCD_MT000040Material Material { get; set; }
        internal POCD_MT000040Device Device { get; set; }
        internal POCD_MT000040PlayingEntity PlayingEntity { get; set; }
        internal POCD_MT000040Entity Entity { get; set; }
        internal POCD_MT000040Component4 Component4 { get; set; }
        internal List<POCD_MT000040Component4> Component4List { get; set; }
        #endregion

        #region ::  Methods

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="_cdaObject"></param>
        /// <param name="_section"></param>
        /// <param name="obj">
        ///     obj : cdaObject.Problems 와 같은 Entry를 만들기 위한 Instances
        ///     - 사용자들로 부터 받는 CDAObject의 Instances (Entry를 만들기 위한 Instances)
        /// </param>
        /// <returns></returns>

        // 하나의 Section 안에 만들어지는 여러개의 Entry들...
        internal POCD_MT000040Entry[] CreateSectionEntries<T>(CDAObject cdaObject, Section _section, T obj)
        {
            EntryList = new List<POCD_MT000040Entry>(_section.EntryList.Count());

            #region input Validation
            if (obj == null) throw new Exception("Object is null!");
            if (_section == null) throw new Exception("Section is null!");
            #endregion

            try
            {
                _section.EntryList.ToList().ForEach(d => { EntryList.AddRange(CreateEntries<T>(cdaObject, _section, d, obj)); });

                //Parallel.ForEach(_section.EntryList, d => { EntryList.AddRange(CreateEntries<T>(cdaObject, _section, d, obj)); });
                //_section.EntryList.AsParallel().ForAll(d => { EntryList.AddRange(CreateEntries<T>(cdaObject, _section, d, obj)); });
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                throw new Exception("CreateEntry Exception: " + MessageHandler.GetErrorMessage(e));
            }

            return EntryList.ToArray(); // 하나의 Section 안에 만들어지는 여러개의 Entry들...
        }

        internal POCD_MT000040Entry[] CreateSectionEntries<T, U>(CDAObject cdaObject, Section _section, U obj)
        {
            EntryList = new List<POCD_MT000040Entry>();

            try
            {
                _section.EntryList.All(d => { EntryList.AddRange(CreateEntries<T, U>(cdaObject, _section, d, obj)); return true; });
                //Parallel.ForEach(_section.EntryList, d => { EntryList.AddRange(CreateEntries<T, U>(cdaObject, _section, d, obj)); });
                //_section.EntryList.AsParallel().ForAll(d => { EntryList.AddRange(CreateEntries<T, U>(cdaObject, _section, d, obj)); });
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                throw new Exception("CreateEntry Exception: " + MessageHandler.GetErrorMessage(e));
            }

            return EntryList.ToArray(); // 하나의 Section 안에 만들어지는 여러개의 Entry들...
        }

        // 하나의 Section 안에 만들어지는 여러개의 Entry들(<act..., <organizer..., 등)을 만드는 Method
        private List<POCD_MT000040Entry> CreateEntries<T, U>(CDAObject cdaObject, Section _section, SectionPart entry, U obj)
        {
            List<POCD_MT000040Entry> entryList = new List<POCD_MT000040Entry>();

            try
            {
                IEnumerable<T> list = (IEnumerable<T>)obj;

                if (list != null && list.Count() > 0)
                {
                    list.All(t =>
                    {
                        entryList.Add(new POCD_MT000040Entry()
                        {
                            Item = CreateEntryItem<T>(cdaObject, _section, entry, t)
                        });
                        return true;
                    });
                    //Parallel.For(0, list.Count(), (i) =>
                    //{
                    //    entryList.Add(new POCD_MT000040Entry()
                    //    {
                    //        Item = CreateEntryItem<T>(cdaObject, _section, entry, list[i])
                    //    });
                    //});
                }
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                throw new Exception("CreateEntries Exception: " + MessageHandler.GetErrorMessage(e));
            }
            return entryList;
        }

        private void PerformanceTestMethod(SectionPart entry)
        {

            #region Performance Test Code
            DateTime now = DateTime.Now;
            for (int i = 0; i < 4590; i++)
            {
                //BodyStructure bodyStructure = CommonQuery.GetBodyStructure(new string[] { "@classCode" }, entry);
            }
            TimespanLog("BodyStructure btest Timespan", now);
            #endregion

        }

        private List<POCD_MT000040Entry> CreateEntries<T>(CDAObject cdaObject, Section _section, SectionPart entry, T obj)
        {
            List<POCD_MT000040Entry> entryList = new List<POCD_MT000040Entry>();
            POCD_MT000040Entry entryItem = new POCD_MT000040Entry() { Item = CreateEntryItem<T>(cdaObject, _section, entry, obj) };
            entryList.Add(entryItem);
            return entryList;
        }

        //static string DynamicVariale<T>(T data, string param)
        //{
        //    if (string.IsNullOrEmpty(param)) return null;

        //    PropertyInfo[] p = typeof(T).GetProperties();
        //    for (int i = 0; i < p.Count(); i++)
        //    {
        //        strTemp = GetValue<T>(data, param, p[i]);
        //        if (!string.IsNullOrEmpty(strTemp)) break;
        //    }
        //    return strTemp;
        //}

        static ParameterExpression pe;
        private static string DynamicVariale<T>(T obj, string param)
        {
            if (string.IsNullOrEmpty(param) || obj == null) return null;
            try
            {
                pe = Expression.Parameter(obj.GetType(), "x");
                var propertyResolver = Expression.Lambda<Func<T, string>>(Expression.Property(pe, param), pe).Compile();
                return (string)propertyResolver(obj);
            }
            catch
            {
                return null;
            }
        }

        //static string DynamicVariale<T>(T data, string param)
        //{
        //    if (string.IsNullOrEmpty(param)) return null;
        //    PropertyInfo[] p = typeof(T).GetProperties();
        //    for (int i = 0; i < p.Count(); i++)
        //    {
        //        strTemp = GetValue<T>(data, param, p[i]);
        //        if (!string.IsNullOrEmpty(strTemp)) break;
        //    }
        //    return strTemp;
        //}

        //static string DynamicVariale<T>(T data, string param)
        //{
        //    if (string.IsNullOrEmpty(param)) return null;

        //    return typeof(T).GetProperties().Select(p => GetValue<T>(data, param, p)).TakeWhile(t => !string.IsNullOrEmpty(t)).FirstOrDefault(t => !string.IsNullOrEmpty(t));
        //}

        //static string DynamicVariale<T>(T data, string param)
        //{
        //    if (string.IsNullOrEmpty(param)) return null;

        //    return typeof(T).GetProperties().Select(p => GetValue<T>(data, param, p)).FirstOrDefault(t => !string.IsNullOrEmpty(t));
        //}

        private object CreateEntryRelationItem<T>(CDAObject cdaObject, Section _section, SectionPart entry, T obj, string Extension)
        {
            try
            {
                #region Instance Create
                Type type = FindType(entry.BindType);
                object item = CreateInstance(type);
                #endregion

                SetEntryItem<T>(entry, obj, item, type, Extension);
                SetValue<T>(entry, obj, item, type);
                SetParticipant<T>(entry, obj, item, type);
                CreateEntryRelationship<T>(cdaObject, _section, entry, obj, type, item, Extension);

                //                Parallel.Invoke(
                //                #region Entry ID, CODE, TEMPLATEID 등...
                //() => { SetEntryItem<T>(entry, obj, item, type); },
                //                #endregion
                //                #region value
                // () => { SetValue<T>(entry, obj, item, type); },
                //                #endregion
                //                #region observation
                // () => { SetParticipant<T>(entry, obj, item, type); },
                //                #endregion
                //                #region entryRelationship
                // () => { CreateEntryRelationship<T>(cdaObject, _section, entry, obj, type, item); }
                //                #endregion
                //);

                return item;
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                throw new Exception("CreateEntryRelationItem Exception: " + MessageHandler.GetErrorMessage(e));
            }
        }

        private void CreateEntryRelationship<T>(CDAObject cdaObject, Section _section, SectionPart entry, T obj, Type type, object item, string Extension)
        {
            if (entry != null && entry.Children != null)
            {
                //List<POCD_MT000040EntryRelationship> createRelationships = new List<POCD_MT000040EntryRelationship>();
                //createRelationships.AddRange(entry.Children.Select(d => CreateEntryRelationship<T>(cdaObject, _section, d, obj)));
                //entry.Children.AsParallel().ForAll(d => createRelationships.AddRange(CreateEntryRelationship<T>(cdaObject, _section, d, obj)));
                //entry.Children.All(d => { createRelationships.AddRange(CreateEntryRelationship<T>(cdaObject, _section, d, obj)); return true; });
                #region Backup
                //foreach (SectionPart ent in entry.Children)
                //{
                //    relationshipList = CreateEntryRelationship<T>(_cdaObject, _section, ent, obj) as List<POCD_MT000040EntryRelationship>;
                //}
                #endregion
                SetValue("entryRelationship", item, entry.Children.Select(d => CreateEntryRelationship<T>(cdaObject, _section, d, obj, Extension)).ToArray(), type);
            }
        }

        private object CreateEntryRelationItem<T, U>(CDAObject cdaObject, Section _section, SectionPart entry, T obj, string Extension)
        {
            try
            {
                #region Instance Create
                Type type = FindType(entry.BindType);
                object item = CreateInstance(type);
                #endregion

                SetEntryItem<T>(entry, obj, item, type, Extension);
                SetValue<T>(entry, obj, item, type);
                SetParticipant<T>(entry, obj, item, type);
                CreateEntryRelationship<T, U>(cdaObject, _section, entry, obj, type, item, Extension);

                //                Parallel.Invoke(
                //                #region Entry ID, CODE, TEMPLATEID 등...
                //() => { SetEntryItem<T>(entry, obj, item, type, Extension); },
                //                #endregion
                //                #region value
                // () => { SetValue<T>(entry, obj, item, type); },
                //                #endregion
                //                #region observation
                // () => { SetParticipant<T>(entry, obj, item, type); },
                //                #endregion
                //                #region entryRelationship
                // () => { CreateEntryRelationship<T, U>(cdaObject, _section, entry, obj, type, item, Extension); }
                //                #endregion
                //);

                return item;
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                throw new Exception("CreateEntryRelationItem Exception: " + MessageHandler.GetErrorMessage(e));
            }
        }

        List<POCD_MT000040EntryRelationship> relations2 = new List<POCD_MT000040EntryRelationship>();
        private void CreateEntryRelationship<T, U>(CDAObject cdaObject, Section _section, SectionPart entry, T obj, Type type, object item, string Extension)
        {
            if (entry != null && entry.Children != null)
            {
                relations2.Clear();
                entry.Children.ToList().ForEach(d => { relations2.AddRange(CreateEntryRelationship<T, U>(cdaObject, _section, d, obj, Extension)); });
                SetValue("entryRelationship", item, relations2, type);
            }
        }

        private void SetParticipant<T>(SectionPart entry, T obj, object item, Type type)
        {
            BodyStructure participant = CommonQuery.GetBodyStructure("participant", entry);
            if (participant != null && participant.UseYN == "TRUE")
            {
                PlayingEntity = new POCD_MT000040PlayingEntity() { classCode = CommonQuery.GetBodyValue("participant/participantRole/playingEntity/@classCode", entry) };

                BodyStructure structure = CommonQuery.GetBodyStructure("participant/participantRole/playingEntity/code", entry);
                if (structure != null && structure.UseYN == "TRUE")
                    PlayingEntity.code = GetCE(obj.GetVariable(structure), obj.GetVariable(CommonQuery.GetBodyStructure("participant/participantRole/playingEntity/code/@displayName", entry)), null, null);

                structure = CommonQuery.GetBodyStructure("participant/participantRole/playingEntity/name", entry);
                if (structure != null && structure.UseYN == "TRUE")
                {
                    PlayingEntity.name = new PN[] { new PN() { Text = new string[] { obj.GetVariable(structure) } } };
                }

                ParticipantRole = new POCD_MT000040ParticipantRole() { classCode = CommonQuery.GetBodyValue("participant/participantRole/@classCode", entry), Item = PlayingEntity };
                POCD_MT000040Participant2 participant2 = new POCD_MT000040Participant2() { typeCode = CommonQuery.GetBodyValue("participant/@typeCode", entry), participantRole = ParticipantRole };
                POCD_MT000040Participant2[] participants = new POCD_MT000040Participant2[] { participant2 };

                SetValue("participant", item, participants, type);
            }
        }

        private object CreateEntryItem<T>(CDAObject cdaObject, Section _section, SectionPart entry, T obj)
        {
            try
            {
                #region Instance Create
                Type type = FindType(entry.BindType);
                object item = CreateInstance(type);
                #endregion

                string Extension = obj.GetValue("Extension");

                //from = DateTime.Now; SetEntryItem<T>(entry, obj, item, type, Extension); TimespanLog("SetEntryItem Timespan", from);
                //from = DateTime.Now; SetValue<T>(entry, obj, item, type); TimespanLog("SetValue Timespan", from);
                //from = DateTime.Now; SetObservations<T>(entry, obj, item, type, Extension); TimespanLog("SetObservations Timespan", from);
                //from = DateTime.Now; SetConsumableElement<T>(entry, obj, item, type); TimespanLog("SetConsumableElement Timespan", from);
                //from = DateTime.Now; SetRelationships<T>(cdaObject, _section, entry, obj, item, type, Extension); TimespanLog("SetRelationships Timespan", from);

                SetEntryItem<T>(entry, obj, item, type, Extension);
                SetValue<T>(entry, obj, item, type);
                SetObservations<T>(entry, obj, item, type, Extension);
                SetConsumableElement<T>(entry, obj, item, type);
                SetRelationships<T>(cdaObject, _section, entry, obj, item, type, Extension);

                //                Parallel.Invoke(
                //                #region Entry ID, CODE, TEMPLATEID 등...
                //() => { SetEntryItem<T>(entry, obj, item, type); },
                //                #endregion
                //                #region value
                // () => { SetValue<T>(entry, obj, item, type); },
                //                #endregion
                //                #region observation
                // () => { SetObservations<T>(entry, obj, item, type); },
                //                #endregion
                //                #region repeatNumber / doseQuantity / consumable
                // () => { SetConsumableElement<T>(entry, obj, item, type); },
                //                #endregion
                //                #region entryRelationship
                // () => { SetRelationships<T, U>(cdaObject, _section, entry, obj, item, type); }
                //                #endregion
                //);

                return item;
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                throw new Exception("CreateEntryItem Exception: " + MessageHandler.GetErrorMessage(e));
            }
        }

        private void TimespanLog(string str, DateTime f)
        {
            //if (Timespan.IsDebugEnabled)
            //{
            //    Timespan.Debug(string.Format("{0} - {1}", str, (DateTime.Now - f).ToString()));
            //}
        }

        private void SetRelationships<T>(CDAObject cdaObject, Section _section, SectionPart entry, T obj, object item, Type type, string Extension)
        {
            if (entry != null && entry.Children != null)
            {
                entry.Children.ToList().ForEach(ent =>
                {
                    SetValue("entryRelationship", item, new POCD_MT000040EntryRelationship[] { CreateEntryRelationship<T>(cdaObject, _section, ent, obj, Extension) }, type);
                });
                //Parallel.ForEach(entry.Children, ent =>
                //{
                //    SetValue("entryRelationship", item, new POCD_MT000040EntryRelationship[] { CreateEntryRelationship<T>(cdaObject, _section, ent, obj, Extension) }, type);
                //});
            }
        }

        private static void SetConsumableElement<T>(SectionPart entry, T obj, object item, Type type)
        {
            SetUsage<T>(obj, item, type);
            SetDoseQuantity<T>(entry, obj, item, type);
            SetRepeatNumber<T>(entry, obj, item, type);
            SetConsumable<T>(entry, obj, item, type);

            //Parallel.Invoke(
            //    () => { SetUsage<T>(obj, item, type); },
            //    () => { SetDoseQuantity<T>(entry, obj, item, type); },
            //    () => { SetRepeatNumber<T>(entry, obj, item, type); },
            //    () => { SetConsumable<T>(entry, obj, item, type); }
            //    );
        }

        private static void SetUsage<T>(T obj, object item, Type type)
        {
            string Usage = obj.GetValue("Usage");
            if (!string.IsNullOrEmpty(Usage))
                SetValue("text", item, new ED() { Text = new string[] { Usage } }, type);
        }

        private static void SetConsumable<T>(SectionPart entry, T obj, object item, Type type)
        {
            string strProperty = "consumable";
            BodyStructure bodyValue = CommonQuery.GetBodyStructure(strProperty, entry);
            if (bodyValue != null && bodyValue.UseYN == "TRUE")
            {
                POCD_MT000040ManufacturedProduct manufacturedProduct = new POCD_MT000040ManufacturedProduct();
                Type manufacturedProductType = typeof(POCD_MT000040ManufacturedProduct);
                POCD_MT000040Material material = new POCD_MT000040Material();
                Type materialType = typeof(POCD_MT000040Material);

                manufacturedProduct.classCode = RoleClassManufacturedProduct.MANU;
                manufacturedProduct.templateId = GetTemplateId(CommonQuery.GetBodyValue("consumable/manufacturedProduct/templateId/@root", entry), CommonQuery.GetBodyValue("consumable/manufacturedProduct/templateId/@extension", entry));

                BodyStructure codeStructure = CommonQuery.GetBodyStructure("consumable/manufacturedProduct/manufacturedMaterial/code", entry);
                if (codeStructure != null && codeStructure.UseYN == "TRUE")
                {
                    string codeSystemName = CommonQuery.GetBodyValue("consumable/manufacturedProduct/manufacturedMaterial/code/@codeSystemName", entry);
                    string codeSystem = CommonQuery.GetBodyValue("consumable/manufacturedProduct/manufacturedMaterial/code/@codeSystem", entry);
                    string MajorComponentCode = obj.GetValue("MajorComponentCode");
                    CE ce = GetCE(obj.GetValue("MedicationCode"), obj.GetValue("MedicationName"), codeSystemName, codeSystem);
                    ce.translation = !string.IsNullOrEmpty(MajorComponentCode) ? new CD[] { GetCD(MajorComponentCode, obj.GetValue("MajorComponent"), obj.GetValue("ConsumableCodeSystemName"), obj.GetValue("ConsumableCodeSystem")) } : null;
                    material.code = ce;
                    manufacturedProduct.Item = material;
                }

                BodyStructure nameStructure = CommonQuery.GetBodyStructure("consumable/manufacturedProduct/manufacturedMaterial/name", entry);
                if (nameStructure != null && nameStructure.UseYN == "TRUE")
                {
                    if (nameStructure.ValueType == "OBJECT")
                    {
                        material.name = GetEN(obj.GetValue(nameStructure.Property));
                        manufacturedProduct.Item = material;
                    }
                }

                POCD_MT000040Consumable consumable = new POCD_MT000040Consumable() { manufacturedProduct = manufacturedProduct };
                SetValue(strProperty, item, consumable, type);
            }
        }

        static Double s;
        static Regex rg = new Regex(@"[^0-9.]", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static void SetDoseQuantity<T>(SectionPart entry, T obj, object item, Type type)
        {
            BodyStructure bodyValue = CommonQuery.GetBodyStructure("doseQuantity", entry);
            if (bodyValue != null && bodyValue.UseYN == "TRUE")
            {
                string doseQuantity = obj.GetValue("DoseQuantity");
                string DoseQuantityUnit = obj.GetValue("DoseQuantityUnit");
                //IVL_PQ ivl_pq = (!string.IsNullOrEmpty(doseQuantity) && Double.TryParse(rg.Replace(doseQuantity.Trim(), ""), out s)) ?
                IVL_PQ ivl_pq = (!string.IsNullOrEmpty(doseQuantity) && Double.TryParse(doseQuantity.Trim(), out s)) ?
                    //new IVL_PQ() { value = doseQuantity, unit = DoseQuantityUnit } :
                    new IVL_PQ() { value = doseQuantity.Trim(), unit = DoseQuantityUnit } :
                    ivl_pq = new IVL_PQ() { nullFlavor = "NI", unit = DoseQuantityUnit };
                SetValue("doseQuantity", item, ivl_pq, type);
            }
        }

        static Regex rg2 = new Regex(@"\D", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static void SetRepeatNumber<T>(SectionPart entry, T obj, object item, Type type)
        {
            BodyStructure bodyValue = CommonQuery.GetBodyStructure("repeatNumber", entry);
            if (bodyValue != null && bodyValue.UseYN == "TRUE")
            {
                string repeatNumber = obj.GetValue("RepeatNumber");
                IVL_INT ivl_int = !string.IsNullOrEmpty(repeatNumber) ? new IVL_INT() { value = rg2.Replace(repeatNumber, "") } : null;
                SetValue("repeatNumber", item, ivl_int, type);
            }
        }

        private void SetEntryItem<T>(SectionPart entry, T obj, object item, Type type, string Extension)
        {
            string ClassCode = null;
            string MoodCode = null;
            BodyStructure bodyStructure = CommonQuery.GetBodyStructure("@classCode", entry);
            if (bodyStructure != null && bodyStructure.UseYN == "TRUE")
                ClassCode = bodyStructure.ValueType == "OBJECT" ? obj.GetValue("ClassCode") : bodyStructure.Value;
            SetValue(entry, item, type, "@classCode", "classCode", obj.GetValue("ClassCodeType"), ClassCode, bodyStructure);

            bodyStructure = CommonQuery.GetBodyStructure("@moodCode", entry);
            if (bodyStructure != null && bodyStructure.UseYN == "TRUE")
                MoodCode = bodyStructure.ValueType == "OBJECT" ? obj.GetValue("MoodCode") : bodyStructure.Value;
            SetValue(entry, item, type, "@moodCode", "moodCode", obj.GetValue("MoodCodeType"), MoodCode, bodyStructure);

            SetValue("templateId", item, GetTemplateId(entry.TemplateIdRoot, entry.TemplateIdExtension), type);
            //SetValue("id", item, GetIIArray(Extension, Guid.NewGuid().ToString()), type);
            SetValue("id", item, GetIIArray(Extension, Guid.NewGuid().ToString().ToUpper()), type);
            SetCode<T>(entry, obj, item, type, Extension);
            SetStatusCode<T>(entry, obj, item, type);
            SetEffectiveTime<T>(entry, obj, item, type);

            //Parallel.Invoke(
            //    () => { SetValue(entry, item, type, new string[] { "@classCode" }, "classCode", DynamicVariale<T>(obj, "ClassCodeType"), ClassCode); },
            //    () => { SetValue(entry, item, type, new string[] { "@moodCode" }, "moodCode", DynamicVariale<T>(obj, "MoodCodeType"), MoodCode); },

            //    () => { SetValue("templateId", item, GetTemplateId(entry.TemplateIdRoot, entry.TemplateIdExtension), type); },
            //    () => { SetValue("id", item, GetIIArray(DynamicVariale<T>(obj, "Extension"), Guid.NewGuid().ToString()), type); },
            //    () => { SetCode<T>(entry, obj, item, type); },
            //    () => { SetStatusCode<T>(entry, obj, item, type); },
            //    () => { SetEffectiveTime<T>(entry, obj, item, type); }
            //    );
        }

        static To DynamicObject<Ti, To>(Ti data, string param)
        {
            if (string.IsNullOrEmpty(param)) return default(To);

            PropertyInfo p = typeof(Ti).GetProperty(param);
            return p == null ? default(To) : data.GetValue<Ti, To>(p);
        }

        private static void SetValue<T>(SectionPart entry, T obj, object item, Type type)
        {
            BodyStructure valueStructure = CommonQuery.GetBodyStructure("value", entry);
            if (valueStructure != null && valueStructure.UseYN == "TRUE")
            {
                string ValueClassType = obj.GetValue("ValueClassType");
                List<CD> kostomList = new List<CD>();
                KostomObject[] k = DynamicObject<T, KostomObject[]>(obj, "KostomCodes");
                if (k != null && k.Count() > 0)
                    k.ToList().ForEach(t => { kostomList.Add(new CD() { code = t.Code, displayName = t.DisplayName, codeSystem = t.CodeSystem, codeSystemName = t.CodeSystemName }); });
                object value = null;

                if (ValueClassType == "ANY[]")
                    value = GetANY(new CD()
                    {
                        code = obj.GetValue("Value"),
                        displayName = obj.GetValue("Name"),
                        codeSystem = OID.KCD,
                        codeSystemName = CodeSystemName.KCD,
                        translation = kostomList != null && kostomList.Any() ? kostomList.ToArray() : null,
                    });
                else if (ValueClassType == "CD" && entry.TemplateIdRoot != EntryOID.SOCIAL_HISTORY_OBSERVATION)
                    value = GetANY(new CD()
                    {
                        code = obj.GetValue("Value"),
                        displayName = obj.GetValue("Name"),
                        codeSystem = OID.KOSTOM,
                        codeSystemName = CodeSystemName.KOSTOM,
                        translation = kostomList != null && kostomList.Any() ? kostomList.ToArray() : null,
                    });

                #region 17.04.21 추가 - HSH  (리팩토링 필요)
                if (obj is SignatureObject)
                {
                    if (item.GetType() == typeof(POCD_MT000040ObservationMedia))
                    {
                        value = new ED() { nullFlavor = "NI" };
                    }
                    else
                    {
                        value = GetANY(new ED()
                        {
                            Text = new string[] { DynamicVariale<T>(obj, "ImageData") },
                            representation = BinaryDataEncoding.B64,
                            mediaType = DynamicVariale<T>(obj, "MediaType")
                        });
                    }
                }
                #endregion
                if (value != null)
                    SetValue("value", item, value, type);
            }
        }

        private static void SetStatusCode<T>(SectionPart entry, T obj, object item, Type type)
        {
            SetValue("statusCode", item, new CS() { code = CommonQuery.GetBodyValue("statusCode/@code", entry) }, type);
        }

        private static void SetCode<T>(SectionPart entry, T obj, object item, Type type, string Extension)
        {
            BodyStructure c = CommonQuery.GetBodyStructure("code/@code", entry);
            BodyStructure s = CommonQuery.GetBodyStructure("code/@codeSystem", entry);
            BodyStructure n = CommonQuery.GetBodyStructure("code/@codeSystemName", entry);
            BodyStructure d = CommonQuery.GetBodyStructure("code/@displayName", entry);
            BodyStructure nf = CommonQuery.GetBodyStructure("code/@nullFlavor", entry);

            if ((c == null || c.UseYN == "FALSE") &&
                (s == null || s.UseYN == "FALSE") &&
                (n == null || n.UseYN == "FALSE") &&
                (d == null || d.UseYN == "FALSE") &&
                (nf == null || nf.UseYN == "FALSE"))
                return;

            if (obj is LaboratoryTestObject && !string.IsNullOrEmpty(Extension))
            {
                if (Extension == "Specimen")
                {
                    c.Property = "EntryCode";
                    d.Property = "EntryName";
                }
                else
                {
                    c.Property = "TestCode";
                    d.Property = "TestName";
                }
            }

            object code = null;
            string codeSystemName = null;
            string cd = obj.GetVariable(c);
            if (n == null)
            {
                codeSystemName = CommonQuery.GetCodeValue(cd, "CLASSIFICATION");
            }
            else
            {
                codeSystemName = obj.GetVariable(n);
                if (string.IsNullOrEmpty(codeSystemName))
                    codeSystemName = CommonQuery.GetCodeValue(cd, "CLASSIFICATION");
            }

            if (entry.CodeType == "CD")
                code = GetCD(cd, obj.GetVariable(d), codeSystemName, obj.GetVariable(s));
            else if (entry.CodeType == "CE")
                code = GetCE(cd, obj.GetVariable(d), codeSystemName, obj.GetVariable(s));

            BodyStructure translation = CommonQuery.GetBodyStructure("code/translation", entry);
            if (translation != null && translation.UseYN == "TRUE")
            {
                if (obj is SocialHistoryObject)
                {
                    string CodeSystem = obj.GetValue("CodeSystem");
                    string CodeSystemName = obj.GetValue("CodeSystemName");

                    CD[] translations = new CD[]
                    {
                        new CD() { code = obj.GetValue("FrequencyCode"), displayName = obj.GetValue("Frequency"), codeSystem = CodeSystem, codeSystemName = CodeSystemName },
                        new CD() { code = obj.GetValue("AlcoholConsumptionCode"), displayName = obj.GetValue("AlcoholConsumption"), codeSystem = CodeSystem, codeSystemName = CodeSystemName },
                        new CD() { code = obj.GetValue("OverdrinkingCode"), displayName = obj.GetValue("Overdrinking"), codeSystem = CodeSystem, codeSystemName = CodeSystemName }
                    };

                    if (code != null && translations != null && translations.Count() > 0)
                        SetValue("translation", code, translations, code.GetType());
                }
                else if (translation != null && translation.ValueType == "OBJECT" && translation.Property == "KostomCodes")
                {
                    KostomObject[] k = DynamicObject<T, KostomObject[]>(obj, "KostomCodes");
                    List<CD> kostomList = new List<CD>();
                    if (k != null)
                        //k.ForEach(t => { kostomList.Add(new CD() { code = t.Code, displayName = t.DisplayName, codeSystem = t.CodeSystem, codeSystemName = t.CodeSystemName }); });
                        kostomList.AddRange(k.Select(t => new CD() { code = t.Code, displayName = t.DisplayName, codeSystem = t.CodeSystem, codeSystemName = t.CodeSystemName }));

                    if (code != null && kostomList.Count() > 0)
                        SetValue("translation", code, kostomList.ToArray(), code.GetType());
                }
                else
                {
                    translation = CommonQuery.GetBodyStructure("code/translation/@code", entry);
                    if (translation != null && translation.UseYN == "TRUE")
                    {
                        CD translationCode = new CD();
                        SetValue<T>("code", translationCode, obj, entry, "code/translation/@code", translation);
                        SetValue<T>("displayName", translationCode, obj, entry, "code/translation/@displayName");
                        SetValue<T>("codeSystem", translationCode, obj, entry, "code/translation/@codeSystem");
                        SetValue<T>("codeSystemName", translationCode, obj, entry, "code/translation/@codeSystemName");
                        if ((!string.IsNullOrEmpty(translationCode.code) || !string.IsNullOrEmpty(translationCode.displayName)) && code != null && translationCode != null)
                            SetValue("translation", code, new CD[] { translationCode }, code.GetType());
                    }
                }
            }

            if (code != null)
                SetValue("code", item, code, type);
        }

        private static void SetValue<T>(string propertyName, object target, T source, SectionPart part, string variable, BodyStructure structure = null)
        {
            structure = structure == null ? CommonQuery.GetBodyStructure(variable, part) : structure;
            if (structure != null && structure.UseYN == "TRUE")
            {
                if (structure.ValueType == "STATIC")
                    SetValue(propertyName, target, structure.Value, target.GetType());
                else if (structure.ValueType == "OBJECT")
                    SetValue(propertyName, target, source.GetValue(structure.Property), target.GetType());
            }
        }

        //private static void SetValue<T>(string propertyName, object target, T source, SectionPart part, string[] variable, BodyStructure structure = null)
        //{
        //    structure = structure == null ? CommonQuery.GetBodyStructure(variable, part) : structure;
        //    if (structure != null && structure.UseYN == "TRUE")
        //    {
        //        if (structure.ValueType == "STATIC")
        //            SetValue(propertyName, target, structure.Value, target.GetType());
        //        else if (structure.ValueType == "OBJECT")
        //            SetValue(propertyName, target, source.GetValue(structure.Property), target.GetType());
        //    }
        //}

        private void SetEffectiveTime<T>(SectionPart entry, T obj, object item, Type type)
        {
            BodyStructure bodyValue = CommonQuery.GetBodyStructure("effectiveTime", entry);
            if (bodyValue != null && bodyValue.UseYN == "TRUE")
            {
                //string effectiveTimeType = obj.GetValue("EffectiveTimeType");
                object effectiveTimeObj = null;
                string StartDate = obj.GetValue("StartDate");

                if (bodyValue.BindType == "IVL_TS")
                    effectiveTimeObj = !string.IsNullOrEmpty(StartDate) ? GetIVL_TS(StartDate, obj.GetValue("EndDate")) : GetIVL_TS(obj.GetValue("Date"));
                else if (bodyValue.BindType == "SXCM_TS[]")
                {
                    string Period = obj.GetValue("Period");
                    Period = string.IsNullOrEmpty(Period) ? null : Period;
                    effectiveTimeObj = !string.IsNullOrEmpty(StartDate) ? GetSXCM_TS(StartDate, null, Period, !string.IsNullOrEmpty(Period) ? "d" : string.Empty) : GetSXCM_TS(obj.GetValue("Date"), Period, !string.IsNullOrEmpty(Period) ? "d" : string.Empty);
                }
                else if (bodyValue.BindType == "System.String")
                    effectiveTimeObj = StartDate;

                if (effectiveTimeObj != null)
                    SetValue("effectiveTime", item, effectiveTimeObj, type);
            }
        }

        private void SetObservations<T>(SectionPart entry, T obj, object item, Type type, string Extension)
        {
            BodyStructure bodyValue = CommonQuery.GetBodyStructure("component", entry);
            if (bodyValue != null && bodyValue.UseYN == "TRUE")
            {
                List<BodyStructure> bodyValueList = CommonQuery.GetBodyStructures("component/observation", entry);
                if (bodyValueList != null && bodyValueList.Count() > 0)
                {
                    List<POCD_MT000040Component4> componentList = new List<POCD_MT000040Component4>();

                    componentList.AddRange(
                        bodyValueList.Where(t => t.UseYN == "TRUE").Select(b =>
                        new POCD_MT000040Component4()
                        {
                            typeCode = ActRelationshipHasComponent.COMP,
                            Item = GetObservation<T>(entry, obj, bodyValue, b, Extension)
                        }));

                    SetValue("component", item, componentList.ToArray(), type);
                }
            }
        }

        private POCD_MT000040Observation GetObservation<T>(SectionPart entry, T obj, BodyStructure bodyValue, BodyStructure b, string Extension)
        {
            POCD_MT000040Observation observation = new POCD_MT000040Observation()
            {
                classCode = "OBS",
                moodCode = x_ActMoodDocumentObservation.EVN,
                templateId = GetTemplateId(CommonQuery.GetBodyValue("component/observation/templateId/@root", entry), CommonQuery.GetBodyValue("component/observation/templateId/@extension", entry)),
                id = GetIIArray(),
                statusCode = new CS() { code = CommonQuery.GetBodyValue("statusCode/@code", entry) },
                effectiveTime = GetIVL_TS(obj.GetValue("Date")),
            };

            string valueProperty = !string.IsNullOrEmpty(b.Property) ? b.Property : "ResultValue";
            string ResultUnit = b.ValueType == "STATIC" && !string.IsNullOrEmpty(b.Value) ? b.Value : obj.GetValue("ResultUnit");
            string _ResultValue = obj.GetValue(valueProperty);
            Double d;
            //Regex rg = new Regex(@"[^0-9.]", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            //if (!string.IsNullOrEmpty(_ResultValue) && Double.TryParse(rg.Replace(_ResultValue.Trim(),""), out d)) // 수치가능             
            if (!string.IsNullOrEmpty(_ResultValue) && Double.TryParse(_ResultValue.Trim(), out d)) // 수치가능 
                //observation.value = GetANY(GetPq(_ResultValue, ResultUnit));
                //observation.value = GetANY(GetPq(rg.Replace(_ResultValue.Trim(), ""), ResultUnit));
                observation.value = GetANY(GetPq(_ResultValue.Trim(), ResultUnit));
            else
                observation.value = !string.IsNullOrEmpty(_ResultValue) ? GetANY(GetST(_ResultValue)) : null;

            if (obj is LaboratoryTestObject && Extension == "Specimen")
            {
                string TestName = obj.GetValue("TestName");
                if (!string.IsNullOrEmpty(TestName))
                    observation.text = new ED() { Text = new string[] { TestName } };
            }

            bodyValue = CommonQuery.GetBodyStructure("component/observation/code", entry);
            if (bodyValue != null && bodyValue.UseYN == "TRUE")
            {
                string _code = GetValue<T>(entry, obj, "component/observation/code/@code", b.Group);
                string _name = GetValue<T>(entry, obj, "component/observation/code/@displayName", b.Group);
                string _codeSystem = GetValue<T>(entry, obj, "component/observation/code/@codeSystem", b.Group);
                string _codeSystemName = GetValue<T>(entry, obj, "component/observation/code/@codeSystemName", b.Group);

                observation.code = GetCD(_code, _name, _codeSystemName, _codeSystem);
            }

            bodyValue = CommonQuery.GetBodyStructure("component/observation/referenceRange", entry);
            if (bodyValue != null && bodyValue.UseYN == "TRUE")
            {
                string Reference = obj.GetValue("Reference");
                if (!string.IsNullOrEmpty(Reference))
                {
                    POCD_MT000040ObservationRange observ = new POCD_MT000040ObservationRange() { value = new ST() { Text = new string[] { Reference } } };
                    observation.referenceRange = new POCD_MT000040ReferenceRange[] { new POCD_MT000040ReferenceRange() { observationRange = observ } };
                }
            }
            return observation;
        }

        //private static string GetValue<T>(SectionPart entry, T obj, string[] strs, int group)
        //{
        //    string retVal = string.Empty;
        //    BodyStructure bodyValue = CommonQuery.GetBodyPartByGroup(strs, entry, group);
        //    if (bodyValue != null && bodyValue.UseYN == "TRUE")
        //        retVal = obj.GetVariable(bodyValue);
        //    return retVal;
        //}

        private static string GetValue<T>(SectionPart entry, T obj, string strs, int group)
        {
            string retVal = string.Empty;
            BodyStructure bodyValue = CommonQuery.GetBodyPartByGroup(strs, entry, group);
            if (bodyValue != null && bodyValue.UseYN == "TRUE")
                retVal = obj.GetVariable(bodyValue);
            return retVal;
        }

        //private static void SetValue(SectionPart entry, object item, Type type, string[] xPath, string propertyName, string codeType = null, string Value = null)
        //{
        //    BodyStructure bodyValue = CommonQuery.GetBodyStructure(xPath, entry);
        //    if (string.IsNullOrEmpty(Value) || bodyValue == null || bodyValue.UseYN == "FALSE") return;

        //    SetValue(entry, item, type, propertyName, xPath, bodyValue, Value);
        //}

        //private static void SetValue(SectionPart entry, object item, Type type, string[] xPath, string propertyName, string codeType = null, string Value = null, BodyStructure bodyValue = null)
        //{
        //    bodyValue = bodyValue == null ? CommonQuery.GetBodyStructure(xPath, entry) : bodyValue;
        //    if (string.IsNullOrEmpty(Value) || bodyValue == null || bodyValue.UseYN == "FALSE") return;

        //    SetValue(entry, item, type, propertyName, xPath, bodyValue, Value);
        //}

        private static void SetValue(SectionPart entry, object item, Type type, string xPath, string propertyName, string codeType = null, string Value = null, BodyStructure bodyValue = null)
        {
            bodyValue = bodyValue == null ? CommonQuery.GetBodyStructure(xPath, entry) : bodyValue;
            if (string.IsNullOrEmpty(Value) || bodyValue == null || bodyValue.UseYN == "FALSE") return;

            SetValue(entry, item, type, propertyName, xPath, bodyValue, Value);
        }

        List<POCD_MT000040EntryRelationship> relationshipsList = new List<POCD_MT000040EntryRelationship>();
        private List<POCD_MT000040EntryRelationship> CreateEntryRelationship<T, U>(CDAObject cdaObject, Section _section, SectionPart entry, T obj, string Extension)
        {
            try
            {
                lock (relationshipsList)
                {
                    relationshipsList.Clear();
                    relationshipsList.Add(new POCD_MT000040EntryRelationship()
                    {
                        typeCode = x_ActRelationshipEntryRelationship.SUBJ,
                        inversionInd = false,
                        Item = CreateEntryRelationItem<T, U>(cdaObject, _section, entry, obj, Extension)
                    });
                }
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                throw new Exception("CreateEntryRelationship Exception: " + MessageHandler.GetErrorMessage(e));
            }
            return relationshipsList;
        }

        private POCD_MT000040EntryRelationship CreateEntryRelationship<T>(CDAObject cdaObject, Section _section, SectionPart entry, T obj, string Extension)
        {
            return
                new POCD_MT000040EntryRelationship()
                {
                    typeCode = x_ActRelationshipEntryRelationship.SUBJ,
                    inversionInd = false,
                    Item = CreateEntryRelationItem<T>(cdaObject, _section, entry, obj, Extension)
                };
        }

        private static void SetValue(string propertyName, object item, object value, Type type)
        {
            if (value == null) return;
            item.SetValue(propertyName, value, type);

            //PropertyInfo prop1 = type.GetProperty(propertyName);
            //prop1.SetValue(item, value, null);
        }

        private static void SetValue<T>(string propertyName, object item, T value, Type type)
        {
            if (value == null) return;
            item.SetValue(propertyName, value, type);

            //PropertyInfo prop1 = type.GetProperty(propertyName);
            //prop1.SetValue(item, value, null);
        }

        //private static void SetValue(SectionPart entry, object item, Type type, string propertyName, string[] xPath, BodyStructure bodyValue, string Value)
        //{
        //    try
        //    {
        //        if (bodyValue.BindType == null || bodyValue.BindType == "string")
        //            item.SetValue(propertyName, Value, type);
        //        else
        //        {
        //            Type bindType = FindType(bodyValue.BindType);
        //            item.SetValue(propertyName, Enum.Parse(bindType, Value, true), type);
        //        }
        //    }
        //    catch
        //    {

        //    }

        //    #region Backup
        //    //Type bindType = FindType(bodyValue.BindType);

        //    //if (bindType == null || bindType == typeof(string))
        //    //    item.SetValue(propertyName, Value, type);
        //    //else
        //    //    item.SetValue(propertyName, Enum.Parse(bindType, Value, true), type);
        //    #endregion
        //}

        private static void SetValue(SectionPart entry, object item, Type type, string propertyName, string xPath, BodyStructure bodyValue, string Value)
        {
            try
            {
                if (bodyValue.BindType == null || bodyValue.BindType.ToLower().Contains("string"))
                    item.SetValue(propertyName, Value, type);
                else
                {
                    Type bindType = FindType(bodyValue.BindType);
                    item.SetValue(propertyName, Enum.Parse(bindType, Value, true), type);
                }
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
            }

            #region Backup
            //Type bindType = FindType(bodyValue.BindType);

            //if (bindType == null || bindType == typeof(string))
            //    item.SetValue(propertyName, Value, type);
            //else
            //    item.SetValue(propertyName, Enum.Parse(bindType, Value, true), type);
            #endregion
        }

        //private static Type FindType(string fullName)
        //{
        //    if (string.IsNullOrEmpty(fullName)) return typeof(string);
        //    return
        //        (from t in AppDomain.CurrentDomain.GetAssemblies()
        //         where t.GetType(fullName) != null
        //         select t.GetType(fullName)).FirstOrDefault();
        //}

        static Assembly[] assemblys;

        public static void FindAssembly()
        {
            assemblys = AppDomain.CurrentDomain.GetAssemblies().Where(t => t.FullName.Contains("xave.com.generator.std") || t.FullName.Contains("xave.com.generator.cus") || t.FullName.Contains("mscorlib")).ToArray();
        }

        public static Type FindType(string typeName)
        {
            if (string.IsNullOrEmpty(typeName)) return null;
            if (assemblys == null) FindAssembly();
            return
                (from assembly in assemblys
                 select assembly.GetType(typeName)).FirstOrDefault(t => t != null);
        }

        public object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        #region  :  Problems
        internal POCD_MT000040Entry[] SetProblemConcernEnrty(List<ProblemObject> items)
        {
            if (items != null && items.Count() > 0)
            {
                EntryList = new List<POCD_MT000040Entry>();
                foreach (var item in items)
                {
                    Entry = new POCD_MT000040Entry();
                    Act = new POCD_MT000040Act() { classCode = x_ActClassDocumentEntryAct.ACT, moodCode = x_DocumentActMood.EVN };
                    Act.templateId = GetTemplateId(EntryOID.PROBLEM_CONCERN_ACT, "2015-08-01");
                    Act.id = GetIIArray();
                    //Act.code = GetCD(ActCode.CONC, "Concern", null, "2.16.840.1.113883.5.6");
                    //Act.code = GetCD(ActClass.CONC, ActClassDisplayName.CONC, null, OID.HL7_ACT_CLASS);
                    //수정부분
                    //Act.statusCode = GetCS(item.Status.ToString(), null, null, null);
                    //Act.effectiveTime = new IVL_TS() { value = item.StartDate };

                    Act.effectiveTime = GetIVL_TS(item.StartDate, string.Empty);
                    Act.entryRelationship = GetProblemObservation(item);

                    Entry.Item = Act;
                    EntryList.Add(Entry);
                }
                return EntryList.ToArray();
            }
            else
            {
                return null;
            }
        }

        private POCD_MT000040EntryRelationship[] GetProblemObservation(ProblemObject item)
        {
            EntryRelationshipList = new List<POCD_MT000040EntryRelationship>();
            EntryRelationship = new POCD_MT000040EntryRelationship() { typeCode = x_ActRelationshipEntryRelationship.SUBJ, inversionInd = false };
            Observation = new POCD_MT000040Observation() { classCode = "OBS", moodCode = x_ActMoodDocumentObservation.EVN };
            Observation.templateId = GetTemplateId(EntryOID.PROBLEM_OBSERVATION, "2015-08-01");
            Observation.id = GetIIArray();
            Observation.code = GetCD(SNOMED_CT.Problem, SNOMED_CT_DisplayName.Problem, null, OID.SNOMED_CT);
            //Observation.statusCode = new CS() { code = ActStatus.COMPLETED };
            Observation.effectiveTime = GetIVL_TS(item.StartDate, string.Empty);
            Observation.value = GetANY(GetCD(item.ProblemCode, item.ProblemName, CodeSystemName.KCD, OID.KCD));

            // CONF:1098-14349            
            EntryRelationship.Item = Observation;
            EntryRelationshipList.Add(EntryRelationship);

            return EntryRelationshipList.ToArray();
        }
        #endregion

        #region :  Allergies
        internal POCD_MT000040Entry[] SetAllergiesEntry(List<AllergyObject> allergies)
        {
            EntryList = new List<POCD_MT000040Entry>();

            foreach (var item in allergies)
            {
                Entry = new POCD_MT000040Entry();
                Act = new POCD_MT000040Act() { classCode = x_ActClassDocumentEntryAct.ACT, moodCode = x_DocumentActMood.EVN };
                Act.templateId = GetTemplateId(EntryOID.ALLERGY_PROBLEM_ACT, "2015-08-01");
                //Act.templateId = GetTemplateId(EntryOID.Allergy_Problem_Act, "2014-06-09");
                Act.id = GetIIArray();
                Act.code = new CD() { code = SNOMED_CT.Problem, codeSystem = OID.HL7_ACT_CLASS };
                //Act.statusCode = GetCS(item.Status, null, null, null);
                Act.effectiveTime = GetIVL_TS(null, null);
                Act.entryRelationship = GetAllergiesEntryRelationship(item);

                Entry.Item = Act;

                EntryList.Add(Entry);
            }
            return EntryList.ToArray();
        }
        private POCD_MT000040EntryRelationship[] GetAllergiesEntryRelationship(AllergyObject item)
        {
            EntryRelationshipList = new List<POCD_MT000040EntryRelationship>();
            EntryRelationship = new POCD_MT000040EntryRelationship() { typeCode = x_ActRelationshipEntryRelationship.SUBJ, inversionInd = false };
            Observation = new POCD_MT000040Observation() { classCode = "OBS", moodCode = x_ActMoodDocumentObservation.EVN };
            Participant = new POCD_MT000040Participant2() { typeCode = "CSM" };
            List<POCD_MT000040Participant2> participants = new List<POCD_MT000040Participant2>();
            PlayingEntity = new POCD_MT000040PlayingEntity() { classCode = "MMAT" };
            ParticipantRole = new POCD_MT000040ParticipantRole() { classCode = "MANU" };

            //PlayingEntity.code = GetCE(item.AllergyCode, item.AllergyDisplayName, item.AllergyCodeSystemName, item.AllergyCodeSystemOID);

            ParticipantRole.Item = PlayingEntity;
            Participant.participantRole = ParticipantRole;
            participants.Add(Participant);

            Observation.templateId = GetTemplateId(EntryOID.ALLERGY_INTOLERANCE_OBSERVATION, "2014-06-09");
            Observation.id = GetIIArray();
            //Observation.code = GetCD(ActCode.Assertion, null, null, OID.ACT_CODE);
            //Allergy Observation SHALL contain exactly one [1..1] statusCode/@code="completed" (CodeSystem: 2.16.840.1.113883.5.14 ActStatus) (CONF:7386)
            Observation.statusCode = GetCS("completed", null, null, null);
            //Observation.effectiveTime = GetIVL_TS(item.LowTime, item.HighTime);
            //This value SHALL contain exactly one [1..1] @code, which SHALL be selected from ValueSet Allergy/Adverse Event Type 2.16.840.1.113883.3.88.12.3221.6.2 DYNAMIC (CONF:9139)
            //Observation.value = GetANY(GetCD(item.AllergyTypeCode, item.AllergyTypeDisplayName, CodeSystemName.SNOMED_CT, OID.SNOMED_CT));
            Observation.participant = participants.ToArray();
            EntryRelationship.Item = Observation;

            EntryRelationshipList.Add(EntryRelationship);
            return EntryRelationshipList.ToArray();
        }
        #endregion

        #region :  Medication
        /// <summary>
        /// Medication Entry를 정의
        /// </summary>        
        internal POCD_MT000040Entry[] SetMedicationActivity(List<MedicationObject> items)
        {
            if (items != null && items.Count() > 0)
            {
                EntryList = new List<POCD_MT000040Entry>();
                foreach (var item in items)
                {
                    Entry = new POCD_MT000040Entry();
                    Entry.Item = SetMedicationActivity(item);
                    EntryList.Add(Entry);
                }
                return EntryList.ToArray();
            }
            else
            {
                return null;
            }

        }
        #endregion

        #region :  Discharge Medication(완료)
        internal POCD_MT000040Entry[] SetDischargeMedication(List<MedicationObject> items)
        {
            if (items != null && items.Count() > 0)
            {
                EntryList = new List<POCD_MT000040Entry>();
                foreach (var item in items)
                {
                    Entry = new POCD_MT000040Entry();
                    Act = new POCD_MT000040Act() { classCode = x_ActClassDocumentEntryAct.ACT, moodCode = x_DocumentActMood.EVN };
                    Entry.Item = Act;
                    Act.id = GetIIArray();
                    Act.templateId = GetTemplateId(EntryOID.DISCHARGE_MEDICATION, "2014-06-09");
                    Act.code = GetCD("75311-1", "Discharge medication", CodeSystemName.LOINC, OID.LOINC);
                    //Act.statusCode = new CS() { code = ActStatus.COMPLETED };

                    EntryRelationship = new POCD_MT000040EntryRelationship() { typeCode = x_ActRelationshipEntryRelationship.SUBJ };
                    EntryRelationship.Item = SetMedicationActivity(item);

                    Act.entryRelationship = new POCD_MT000040EntryRelationship[] { EntryRelationship };

                    EntryList.Add(Entry);
                }
                return EntryList.ToArray();
            }
            else
                return null;
        }

        private POCD_MT000040SubstanceAdministration SetMedicationActivity(MedicationObject item)
        {
            POCD_MT000040SubstanceAdministration substanceAdministration = new POCD_MT000040SubstanceAdministration() { classCode = "SBADM", moodCode = x_DocumentSubstanceMood.EVN };
            substanceAdministration.id = GetIIArray();
            substanceAdministration.templateId = GetTemplateId(EntryOID.MEDICATION_ACTIVITY, "2014-06-09");
            //substanceAdministration.statusCode = new CS() { code = ActStatus.COMPLETED };
            //substanceAdministration.effectiveTime = GetSXCM_TS(item.StartDate, item.EndDate, item.Period, "d");
            substanceAdministration.effectiveTime = GetSXCM_TS(item.StartDate, string.Empty, item.Period, "d");
            substanceAdministration.text = !string.IsNullOrEmpty(item.Usage) ? new ED() { Text = new string[] { item.Usage } } : null;
            substanceAdministration.doseQuantity = !string.IsNullOrEmpty(item.DoseQuantity) ? new IVL_PQ() { value = item.DoseQuantity, unit = item.DoseQuantityUnit } : null;
            substanceAdministration.repeatNumber = !string.IsNullOrEmpty(item.RepeatNumber) ? new IVL_INT() { value = item.RepeatNumber } : null;

            //Medication Information
            POCD_MT000040Consumable consumable = new POCD_MT000040Consumable();
            consumable.manufacturedProduct = new POCD_MT000040ManufacturedProduct();
            consumable.manufacturedProduct.classCode = RoleClassManufacturedProduct.MANU;
            consumable.manufacturedProduct.templateId = GetTemplateId(EntryOID.MEDICATION_INFORMATION, "2014-06-09"); //완료
            POCD_MT000040Material material = new POCD_MT000040Material();

            material.code = GetCE(item.MedicationCode, item.MedicationName, CodeSystemName.KD, OID.KD);

            List<CD> codeList = new List<CD>();
            CD atcCode = null;

            if (!string.IsNullOrEmpty(item.MajorComponentCode))
            {
                atcCode = new CD() { code = item.MajorComponentCode, displayName = item.MajorComponent, codeSystemName = CodeSystemName.ATC, codeSystem = OID.ATC };
                codeList.Add(atcCode);
            }
            material.code.translation = codeList.Count() > 0 ? codeList.ToArray() : null;

            consumable.manufacturedProduct.Item = material;
            substanceAdministration.consumable = consumable;
            return substanceAdministration;
        }
        #endregion

        #region :  Procedure
        /// <summary>
        /// Procedure Entry
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        //internal POCD_MT000040Entry[] SetProcedureActivityProcedure(List<ProcedureObject> items)
        //{
        //    if (items != null && items.Count() > 0)
        //    {
        //        EntryList = new List<POCD_MT000040Entry>();

        //        foreach (var item in items)
        //        {
        //            Entry = new POCD_MT000040Entry();
        //            Procedure = new POCD_MT000040Procedure() { classCode = "PROC", moodCode = x_DocumentProcedureMood.EVN };
        //            Procedure.templateId = GetTemplateId(EntryOID.PROCEDURE_ACTIVITY_PROCEDURE, "2014-06-09"); //완료
        //            Procedure.id = GetIIArray();
        //            //(CONF:1098-19207)
        //            Procedure.code = GetCD(item.ProcedureCode, item.ProcedureName, CodeSystemName.ICD_9_CM, OID.ICD_9_CM);
        //            Procedure.statusCode = new CS() { code = ActStatus.COMPLETED };
        //            //Procedure.statusCode = GetCS(item.Status, null, null, null);
        //            Procedure.effectiveTime = new IVL_TS() { value = item.StartDate };
        //            //(CONF:1098-32479)
        //            Procedure.author = new POCD_MT000040Author[] 
        //            {
        //                new POCD_MT000040Author()
        //                {
        //                    templateId = GetTemplateId(EntryOID.AUTHOR_PARTICIPATION),
        //                    time = new TS(){ value = DateTime.Now.ToString("yyyyMMdd") },
        //                    assignedAuthor = new POCD_MT000040AssignedAuthor(){ id = GetIIArray(null, null) }
        //                }
        //            };
        //            Entry.Item = Procedure;
        //            EntryList.Add(Entry);

        //        }
        //        return EntryList.ToArray();
        //    }
        //    else
        //    {
        //        EntryList = new List<POCD_MT000040Entry>();

        //        Entry = new POCD_MT000040Entry();
        //        Procedure = new POCD_MT000040Procedure() { classCode = "PROC", moodCode = x_DocumentProcedureMood.EVN };
        //        Procedure.templateId = GetTemplateId(EntryOID.PROCEDURE_ACTIVITY_PROCEDURE, "2014-06-09");
        //        Procedure.id = GetIIArray();
        //        Procedure.code = new CD() { nullFlavor = "UNK" };
        //        Procedure.statusCode = new CS() { nullFlavor = "UNK" };
        //        Procedure.effectiveTime = new IVL_TS() { nullFlavor = "UNK" };

        //        Entry.Item = Procedure;
        //        EntryList.Add(Entry);
        //        return EntryList.ToArray();
        //    }

        //}
        #endregion

        #region :  Vital Signs
        internal POCD_MT000040Entry[] SetVitalSignOrganizer(List<VitalSignsObject> items)
        {
            if (items != null && items.Count > 0)
            {
                EntryList = new List<POCD_MT000040Entry>();
                Entry = new POCD_MT000040Entry();
                Organizer = new POCD_MT000040Organizer() { classCode = x_ActClassDocumentEntryOrganizer.CLUSTER, moodCode = "EVN" };
                Entry.Item = Organizer;
                Organizer.id = GetIIArray();
                Organizer.templateId = GetTemplateId(EntryOID.VITAL_SIGNS_ORGANIZER, "2015-08-01");
                //Organizer.templateId = GetTemplateId(EntryOID.Vital_Signs_Organizer, "2014-06-09"); // CCDA Version 2.0
                Organizer.code = GetCD(SNOMED_CT.Vital_Signs, SNOMED_CT_DisplayName.Vital_Signs, null, OID.SNOMED_CT);
                //Organizer.code = GetCD("74728-7", "Vital signs, weight, height, head circumference, oximetry, BMI, and BSA panel", CodeSystemName.LOINC, OID.LOINC); // CCDA Version 2.0
                Organizer.code.translation = new CD[] { GetCD(LOINC.Vital_Signs_Code, LoincDisplayName.Vital_Signs_Code, CodeSystemName.LOINC, OID.LOINC) };
                //Organizer.statusCode = new CS() { code = ActStatus.COMPLETED };
                //Organizer.effectiveTime = new IVL_TS() { value = items.Count > 0 ? items[0].Time : DateTime.Now.ToString("yyyyMMdd") };
                //결과 카운트가 0 보다 클때, Vital sign Observation 생성

                List<POCD_MT000040Component4> componentList = new List<POCD_MT000040Component4>();
                foreach (var item in items)
                {
                    if (!string.IsNullOrEmpty(item.Height))
                    {
                        componentList.Add(SetVitalSignObservation(item.Date, item.Height, "cm", LOINC.Height, LoincDisplayName.Height));
                    }
                    if (!string.IsNullOrEmpty(item.Weight))
                    {
                        componentList.Add(SetVitalSignObservation(item.Date, item.Weight, "kg", LOINC.Weight, LoincDisplayName.Weight));
                    }
                    if (!string.IsNullOrEmpty(item.BP_Diastolic))
                    {
                        componentList.Add(SetVitalSignObservation(item.Date, item.BP_Diastolic, "mm[Hg]", LOINC.BP_Diastolic, LoincDisplayName.BP_Diastolic));
                    }
                    if (!string.IsNullOrEmpty(item.BP_Systolic))
                    {
                        componentList.Add(SetVitalSignObservation(item.Date, item.BP_Systolic, "mm[Hg]", LOINC.BP_Systolic, LoincDisplayName.BP_Systolic));
                    }
                    if (!string.IsNullOrEmpty(item.BodyTemperature))
                    {
                        componentList.Add(SetVitalSignObservation(item.Date, item.BodyTemperature, "°C", LOINC.BodyTemperature, LoincDisplayName.BodyTemperature));
                    }
                }
                Organizer.component = componentList != null && componentList.Any() ? componentList.ToArray() : null;


                EntryList.Add(Entry);

                return EntryList.ToArray();
            }
            else
            {
                return null;
            }
        }

        internal POCD_MT000040Component4 SetVitalSignObservation(string time, string result, string unit, string code, string codeDescription)
        {
            POCD_MT000040Component4 obj = new POCD_MT000040Component4();
            Observation = new POCD_MT000040Observation() { classCode = "OBS", moodCode = x_ActMoodDocumentObservation.EVN };
            Observation.templateId = GetTemplateId(EntryOID.VITAL_SIGN_OBSERVATION, "2014-06-09");
            Observation.id = GetIIArray();
            Observation.code = GetCD(code, codeDescription, CodeSystemName.LOINC, OID.LOINC);
            //Observation.statusCode = new CS() { code = ActStatus.COMPLETED };
            Observation.effectiveTime = new IVL_TS() { value = time };
            Observation.value = GetANY(GetPq(result, unit));
            obj.Item = Observation;
            return obj;
        }

        //public POCD_MT000040Component4 SetVitalSignObservation(VitalSignResultObject item)
        //{
        //    POCD_MT000040Component4 obj = new POCD_MT000040Component4();
        //    Observation = new POCD_MT000040Observation() { classCode = "OBS", moodCode = x_ActMoodDocumentObservation.EVN };
        //    Observation.templateId = GetTemplateId(EntryOID.VITAL_SIGN_OBSERVATION, "2014-06-09");
        //    Observation.id = GetIIArray();
        //    Observation.code = GetCD(item.VitalSignCode, item.VitalSignCodeName, CodeSystemName.LOINC, OID.LOINC);
        //    Observation.statusCode = new CS() { code = ActStatus.COMPLETED };
        //    Observation.effectiveTime = new IVL_TS() { value = item.Time };
        //    Observation.value = GetANY(GetPq(item.ResultValue, item.ResultUnit));
        //    obj.Item = Observation;
        //    return obj;
        //}
        #endregion

        #region :  Laboratory Test
        internal POCD_MT000040Entry[] SetResultOrganizer(List<LaboratoryTestObject> items)
        {
            if (items != null && items.Count() > 0)
            {
                EntryList = new List<POCD_MT000040Entry>();
                foreach (var item in items)
                {
                    Entry = new POCD_MT000040Entry() { typeCode = x_ActRelationshipEntry.DRIV };
                    Organizer = new POCD_MT000040Organizer() { classCode = x_ActClassDocumentEntryOrganizer.BATTERY, moodCode = "EVN" };
                    Entry.Item = Organizer;

                    Organizer.templateId = GetTemplateId(EntryOID.RESULT_ORGANIZER, "2015-08-01"); //Result Organizer                    
                    Organizer.id = GetIIArray();
                    //Organizer.code = GetCD(item.TestCode, item.TestName, CodeSystemName.LOINC, OID.LOINC);
                    Organizer.code = GetCD(item.TestCode, item.TestName, CodeSystemName.EDI, OID.EDI);
                    //Organizer.statusCode = new CS() { code = ActStatus.COMPLETED };
                    Organizer.component = SetResultObservation(item);

                    EntryList.Add(Entry);
                }
                return EntryList.ToArray();
            }
            else
            {
                return null;
            }
        }

        internal POCD_MT000040Entry[] SetResultOrganizerByCode(List<LaboratoryTestObject> items)
        {
            if (items != null)
            {
                EntryList = new List<POCD_MT000040Entry>();
                foreach (IGrouping<LaboratoryType, LaboratoryTestObject> group in items.GroupBy(w => w.LabType))
                {
                    foreach (var item in group)
                    {
                        Entry = new POCD_MT000040Entry() { typeCode = x_ActRelationshipEntry.DRIV };
                        Organizer = new POCD_MT000040Organizer() { classCode = x_ActClassDocumentEntryOrganizer.BATTERY, moodCode = "EVN" };
                        Entry.Item = Organizer;
                        Organizer.templateId = GetTemplateId(EntryOID.RESULT_ORGANIZER, "2015-08-01"); //Result Organizer
                        switch (group.Key)
                        {
                            case LaboratoryType.Specimen:
                                Organizer.id = GetIIArray(LaboratoryType.Specimen.ToString(), null);
                                break;
                            case LaboratoryType.Functional:
                                Organizer.id = GetIIArray(LaboratoryType.Functional.ToString(), null);
                                break;
                            case LaboratoryType.Pathology:
                                Organizer.id = GetIIArray(LaboratoryType.Pathology.ToString(), null);
                                break;
                            case LaboratoryType.Radiology:
                                Organizer.id = GetIIArray(LaboratoryType.Radiology.ToString(), null);
                                break;
                            case LaboratoryType.None:
                                Organizer.id = GetIIArray();
                                break;
                            default:
                                Organizer.id = GetIIArray();
                                break;
                        }

                        Organizer.code = GetCD(item.TestCode, item.TestName, CodeSystemName.EDI, OID.EDI);
                        //Organizer.code = GetCD(item.Select(s => s.TestCode).FirstOrDefault(), item.Select(s => s.TestName).FirstOrDefault(), CodeSystemName.LOINC, OID.LOINC);
                        //Organizer.statusCode = new CS() { code = ActStatus.COMPLETED };
                        Component4List = new List<POCD_MT000040Component4>();
                        Component4 = new POCD_MT000040Component4();
                        Observation = new POCD_MT000040Observation() { classCode = "OBS", moodCode = x_ActMoodDocumentObservation.EVN };
                        Observation.templateId = GetTemplateId(EntryOID.RESULT_OBSERVATION, "2015-08-01");
                        Observation.id = GetIIArray();
                        //검사명 Code Mapping
                        //Observation.code = GetCD(comp.EntryCode, comp.EntryName, CodeSystemName.LOINC, OID.LOINC);
                        //Observation.statusCode = new CS() { code = ActStatus.COMPLETED };
                        //Observation.effectiveTime = !string.IsNullOrEmpty(item.OrderDate) ? new IVL_TS() { value = item.OrderDate } : null;
                        //Observation.effectiveTime = !string.IsNullOrEmpty(item.PerformDate) ? new IVL_TS() { value = item.PerformDate } : null;
                        //Result Text
                        //Result Code, Result Code System, Result Code DisplayName
                        Double d;
                        if (Double.TryParse(item.ResultValue.Trim(), out d)) // 수치가능 
                        {
                            //Observation.value = GetANY(GetPq(item.ResultValue, item.ResultUnit));
                            Observation.value = GetANY(GetPq(item.ResultValue, ""));
                        }
                        else
                        {
                            Observation.value = !string.IsNullOrEmpty(item.ResultValue) ? GetANY(new ST() { Text = new string[] { item.ResultValue } }) : null;
                        }
                        //if (!string.IsNullOrEmpty(item.ReferenceLowValue) || !string.IsNullOrEmpty(item.ReferenceHighValue))
                        //{
                        //    POCD_MT000040ObservationRange observ = new POCD_MT000040ObservationRange();
                        //    if (Double.TryParse(item.ReferenceLowValue, out d) && Double.TryParse(item.ReferenceHighValue, out d))
                        //    {
                        //        observ.value = GetIVL_PQ(item.ReferenceLowValue, item.ReferenceHighValue, item.ReferenceUnit);
                        //    }
                        //    else
                        //    {
                        //        observ.value = new ST() { Text = new string[] { item.ReferenceLowValue + " " + item.ReferenceHighValue } };
                        //    }
                        //    POCD_MT000040ReferenceRange reference = new POCD_MT000040ReferenceRange() { observationRange = observ };
                        //    Observation.referenceRange = new POCD_MT000040ReferenceRange[] { reference };
                        //}
                        Component4.Item = Observation;
                        Component4List.Add(Component4);
                        Organizer.component = Component4List.Count() > 0 ? Component4List.ToArray() : null;
                        EntryList.Add(Entry);
                    }
                }
                return EntryList.ToArray();
            }
            else
            {
                return null;
            }
        }

        private POCD_MT000040Component4[] SetResultObservation(List<LaboratoryTestObject> items)
        {
            if (items != null)
            {
                Component4List = new List<POCD_MT000040Component4>();
                foreach (var item in items)
                {
                    POCD_MT000040Component4 component = new POCD_MT000040Component4() { typeCode = ActRelationshipHasComponent.COMP };
                    Observation = new POCD_MT000040Observation() { classCode = "OBS", moodCode = x_ActMoodDocumentObservation.EVN };
                    Observation.templateId = GetTemplateId(EntryOID.RESULT_OBSERVATION, "2015-08-01");
                    Observation.id = GetIIArray();
                    //Observation.code = GetCD(item.EntryCode, item.EntryName, CodeSystemName.LOINC, OID.LOINC);
                    //Observation.statusCode = new CS() { code = ActStatus.COMPLETED };
                    //Observation.effectiveTime = new IVL_TS() { value = item.Date };
                    //Result Text
                    //Result Code, Result Code System, Result Code DisplayName
                    //Observation.value = GetANY(GetPq(item.ResultValue, null));
                    component.Item = Observation;
                    Component4List.Add(component);
                }
                return Component4List.ToArray();
            }
            else return null;
        }


        private POCD_MT000040Component4[] SetResultObservation(LaboratoryTestObject item)
        {
            List<POCD_MT000040Component4> componentList = new List<POCD_MT000040Component4>();

            POCD_MT000040Component4 component = new POCD_MT000040Component4() { typeCode = ActRelationshipHasComponent.COMP };
            Observation = new POCD_MT000040Observation() { classCode = "OBS", moodCode = x_ActMoodDocumentObservation.EVN };
            Observation.templateId = GetTemplateId(EntryOID.RESULT_OBSERVATION, "2015-08-01");
            //Observation.templateId = GetTemplateId("2.16.840.1.113883.10.20.22.4.2", "2014-06-09");
            Observation.id = GetIIArray();
            //검사명 Code Mapping
            //Observation.code = GetCD(item.EntryCode, item.EntryName, CodeSystemName.LOINC, OID.LOINC);
            //Observation.statusCode = new CS() { code = ActStatus.COMPLETED };
            //Observation.effectiveTime = new IVL_TS() { value = item.Date };
            //Result Text
            //Result Code, Result Code System, Result Code DisplayName
            //Observation.value = GetANY(GetPq(item.ResultValue, null));
            component.Item = Observation;
            componentList.Add(component);

            return componentList.ToArray();
        }
        #endregion

        #region :  DIR
        //internal POCD_MT000040Entry[] SetStudyAct(List<StudyObject> items)
        //{
        //    if (items != null)
        //    {
        //        EntryList = new List<POCD_MT000040Entry>();
        //        foreach (StudyObject item in items)
        //        {
        //            Entry = new POCD_MT000040Entry();
        //            Act = new POCD_MT000040Act() { classCode = x_ActClassDocumentEntryAct.ACT, moodCode = x_DocumentActMood.EVN };
        //            Entry.Item = Act;

        //            Act.id = new II[] { new II() { root = item.InstanceUID } };
        //            Act.templateId = GetTemplateId("2.16.840.1.113883.10.20.6.2.6");
        //            Act.code = GetCD("113014", "Study", "DCM", "1.2.840.10008.2.16.4");
        //            if (item.Series != null && item.Series.Count() > 0)
        //            {
        //                EntryRelationshipList = new List<POCD_MT000040EntryRelationship>();
        //                foreach (var series in item.Series)
        //                {
        //                    EntryRelationship = new POCD_MT000040EntryRelationship() { typeCode = x_ActRelationshipEntryRelationship.COMP };
        //                    POCD_MT000040Act act = new POCD_MT000040Act() { classCode = x_ActClassDocumentEntryAct.ACT, moodCode = x_DocumentActMood.EVN };
        //                    act.id = new II[] { new II() { root = series.InstanceUID } };
        //                    act.code = GetCD("113015", "Series", "DCM", "1.2.840.10008.2.16.4");
        //                    //CONF-DIR-121: The qualifier element SHALL contain a name element where @code is 121139 (Modality) and @codeSystem is 1.2.840.10008.2.16.4.
        //                    //CONF-DIR-122: The qualifier element SHALL also contain a value element where @code contains a modality code and @codeSystem is 1.2.840.10008.2.16.4.
        //                    CR qualifier = new CR();
        //                    qualifier.name = new CV() { code = "121139", codeSystem = "1.2.840.10008.2.16.4", codeSystemName = "DCM", displayName = "Modality" };
        //                    qualifier.value = new CD() { code = "CR", codeSystem = "1.2.840.10008.2.16.4", codeSystemName = "DCM", displayName = "Computed Radiography" };

        //                    act.code.qualifier = new CR[] { qualifier };
        //                    if (series.SopInstance != null && series.SopInstance.Count() > 0)
        //                    {
        //                        List<POCD_MT000040EntryRelationship> list = new List<POCD_MT000040EntryRelationship>();
        //                        foreach (var sop in series.SopInstance)
        //                        {
        //                            POCD_MT000040EntryRelationship ent = new POCD_MT000040EntryRelationship() { typeCode = x_ActRelationshipEntryRelationship.COMP };
        //                            Observation = new POCD_MT000040Observation() { classCode = "DGIMG", moodCode = x_ActMoodDocumentObservation.EVN };
        //                            Observation.templateId = GetTemplateId("2.16.840.1.113883.10.20.6.2.8");
        //                            Observation.id = new II[] { new II() { root = sop.InstanceUID } };
        //                            Observation.code = GetCD(sop.UIDCode, null, "DCMUID", "1.2.840.10008.2.6.1");
        //                            ent.Item = Observation;
        //                            list.Add(ent);
        //                        }
        //                        act.entryRelationship = list.ToArray();
        //                    }
        //                    EntryRelationship.Item = act;
        //                    EntryRelationshipList.Add(EntryRelationship);
        //                }
        //            }
        //            Act.entryRelationship = EntryRelationshipList.ToArray();
        //            EntryList.Add(Entry);
        //        }
        //        return EntryList.ToArray();
        //    }
        //    else
        //        return null;
        //}
        #endregion

        //16.5.11
        #region : Signature
        internal POCD_MT000040Entry[] SetSignatureImage(string imageData, string mediaType)
        {
            Entry = new POCD_MT000040Entry();
            ObservationMedia = new POCD_MT000040ObservationMedia();
            Observation = new POCD_MT000040Observation() { classCode = "OBS", moodCode = x_ActMoodDocumentObservation.EVN };
            EntryRelationship = new POCD_MT000040EntryRelationship() { Item = Observation };
            Entry.Item = ObservationMedia;

            ObservationMedia.templateId = GetTemplateId(EntryOID.SIGNATURE_IMAGE);
            Observation.templateId = GetTemplateId(EntryOID.SIGNATURE_IMAGE_OBSERVATION);
            Observation.id = GetIIArray();
            Observation.value = GetANY(new ED() { mediaType = mediaType, representation = BinaryDataEncoding.B64, Text = new string[] { imageData } });
            ObservationMedia.entryRelationship = new POCD_MT000040EntryRelationship[] { EntryRelationship };

            return new POCD_MT000040Entry[] { Entry };
        }
        #endregion

        //16.5.26
        #region :  Immunizations
        internal POCD_MT000040Entry[] SetImmunizationActivity(List<ImmunizationObject> items)
        {
            if (items != null)
            {
                EntryList = new List<POCD_MT000040Entry>();
                foreach (var item in items)
                {
                    Entry = new POCD_MT000040Entry();
                    SubstanceAdministration = new POCD_MT000040SubstanceAdministration() { classCode = "SBADM", moodCode = x_DocumentSubstanceMood.EVN };
                    Entry.Item = SubstanceAdministration;
                    SubstanceAdministration.id = GetIIArray();
                    SubstanceAdministration.templateId = GetTemplateId(EntryOID.IMMUNIZATION_ACTIVITY, "2015-08-01");
                    //SubstanceAdministration.statusCode = new CS() { code = ActStatus.COMPLETED };
                    SubstanceAdministration.consumable = new POCD_MT000040Consumable();
                    SubstanceAdministration.consumable.manufacturedProduct = new POCD_MT000040ManufacturedProduct() { classCode = RoleClassManufacturedProduct.MANU };
                    SubstanceAdministration.consumable.manufacturedProduct.templateId = GetTemplateId(EntryOID.IMMUNIZATION_MEDICATION_INFORMATION, "2014-06-09");
                    SubstanceAdministration.consumable.manufacturedProduct.id = GetIIArray();
                    //SubstanceAdministration.consumable.manufacturedProduct.Item
                    Material = new POCD_MT000040Material();

                    Material.code = GetCE(string.Empty, item.VaccineName, CodeSystemName.EDI, OID.EDI);
                    Material.lotNumberText = new ST() { nullFlavor = "UNK" };
                    SubstanceAdministration.consumable.manufacturedProduct.Item = Material;

                    EntryList.Add(Entry);
                }
            }
            return null;
        }
        #endregion

        //17. 02. 07
        #region : Social history
        internal POCD_MT000040Entry[] SetSocialHistory(CDAObject item)
        {
            POCD_MT000040Entry smoking = new POCD_MT000040Entry();
            Observation = new POCD_MT000040Observation() { classCode = "OBS", moodCode = x_ActMoodDocumentObservation.EVN };
            Observation.templateId = GetTemplateId(EntryOID.SMOKING_STATUS, "2014-06-09");
            Observation.id = GetIIArray();
            Observation.code = GetCE(LOINC.Tobacco_Smoking_Status, LoincDisplayName.Tobacco_Smoking_Status, CodeSystemName.LOINC, OID.LOINC);
            //Observation.statusCode = new CS() { code = ActStatus.COMPLETED };
            Observation.effectiveTime = new IVL_TS() { nullFlavor = "NI" };
            Observation.value = GetANY(GetCD(item.SocialHistory.SmokingStatusCode, item.SocialHistory.SmokingStatus, CodeSystemName.KOSTOM, OID.KOSTOM));
            smoking.Item = Observation;

            POCD_MT000040Entry drinking = new POCD_MT000040Entry();
            Observation = new POCD_MT000040Observation() { classCode = "OBS", moodCode = x_ActMoodDocumentObservation.EVN };
            Observation.templateId = GetTemplateId(EntryOID.SOCIAL_HISTORY_OBSERVATION, "2015-08-01");
            Observation.id = GetIIArray();
            //Observation.statusCode = new CS() { code = ActStatus.COMPLETED };
            Observation.effectiveTime = new IVL_TS() { nullFlavor = "NI" };
            Observation.code = GetCD(SNOMED_CT.Alcohol_Intake, null, null, OID.SNOMED_CT);
            Observation.code.translation = new CD[] 
            {
                GetCD(item.SocialHistory.FrequencyCode,item.SocialHistory.Frequency,"",OID.LOINC),
                GetCD(item.SocialHistory.AlcoholConsumptionCode,item.SocialHistory.AlcoholConsumption,"",OID.LOINC),
                GetCD(item.SocialHistory.OverdrinkingCode,item.SocialHistory.Overdrinking,"",OID.LOINC),
            };
            drinking.Item = Observation;

            return new POCD_MT000040Entry[] { smoking, drinking };
        }
        #endregion
        #endregion
    }
}
