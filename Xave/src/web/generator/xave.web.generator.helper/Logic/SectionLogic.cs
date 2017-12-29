using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using xave.com.generator.cus;
using xave.com.generator.cus.StructureSetModel;
using xave.com.generator.cus.Voca;
using xave.web.generator.helper.Util;

namespace xave.web.generator.helper.Logic
{
    [Serializable]
    public class SectionLogic : DataTypeLogic
    {
        #region :: Privates Memeber
        private static EntryLogic entryLogic = new EntryLogic();
        private static CDAObject cdaObject;
        private static string ErrorLogDirectory = System.Configuration.ConfigurationManager.AppSettings["ErrorLogDirectory"];
        //private static DateTime from;
        //private static DateTime duration;
        #endregion

        #region :: Internal Property
        //internal Dictionary<SectionType, List<string>> Narrative { get; set; }
        internal static StrucDocText DocumentText { get; set; }
        internal static List<StrucDocTable> Tables { get; set; }
        internal static StrucDocTable Table { get; set; }
        internal static StrucDocThead Thead { get; set; }
        internal static List<StrucDocTr> TheadTrList { get; set; }
        internal static List<StrucDocTbody> TbodyList { get; set; }
        internal static StrucDocTbody Tbody { get; set; }
        internal static StrucDocTr Tr { get; set; }
        internal static List<StrucDocTr> TrList { get; set; }
        internal static StrucDocTh[] ThArray { get; set; }
        internal static POCD_MT000040Section Section { get; set; }
        internal static StrucDocTd[] TdArray { get; set; }
        internal static StrucDocList strucDocList { get; set; }
        internal static POCD_MT000040Component3 Component3 { get; set; }
        internal static List<object> SectionTextItems { get; set; }
        internal static StrucDocTr[] TbodyTrArray { get; set; }
        internal static StrucDocParagraph Paragraph { get; set; }
        internal static StrucDocContent Content { get; set; }
        internal static StrucDocTh Th { get; set; }
        internal static StrucDocTd Td { get; set; }
        internal static List<StrucDocTd> StrucdocTdList { get; set; }
        internal static List<object> textItems { get; set; }
        internal static StrucDocItem strucDocItem { get; set; }
        internal static List<StrucDocItem> strucDocItems { get; set; }
        #endregion

        #region :: Constructors
        #endregion

        #region ::  Method
        #region : Section<T>
        public static POCD_MT000040Component3 CreateSection_Entry<T>(CDAObject cdaObject, Section _section, T obj)
        {
            #region input value validation
            if (cdaObject == null) throw new Exception("Not proper CDA Object info!");
            if (_section == null) throw new Exception("Not proper Section info!");
            #endregion

            try
            {
                CreateSectionHead(_section);
                if (obj == null)
                {
                    Section.nullFlavor = "NI";
                    Section.text = new StrucDocText() { Text = new string[] { "정보 없음" } };
                }
                else
                {
                    SectionTextItems = new List<object>();

                    //duration = DateTime.Now;
                    CreateNarrative<T>(_section, obj, cdaObject);
                    //Logger.TimespanLog("CreateNarrative - (" + _section.Code + "), " + obj.ToString() + " - ", duration);

                    //duration = DateTime.Now;
                    CreateEntry<T>(cdaObject, _section, obj);
                    //Logger.TimespanLog("CreateEntry - (" + _section.Code + "), " + obj.ToString() + " - ", duration);

                    Section.text.Items = SectionTextItems.ToArray();
                }
                Component3.section = Section;
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                throw;
            }

            return Component3;
        }

        private static void CreateEntry<T>(CDAObject cdaObject, Section _section, T obj)
        {
            Section.entry = entryLogic.CreateSectionEntries<T>(cdaObject, _section, obj);
        }

        private static void CreateNarrative<T>(Section _section, T obj, CDAObject cdaObject)
        {
            SectionPart[] narratives = _section.NarrativeList;
            if (narratives == null) return;

            foreach (SectionPart n in narratives.Where(w => w.SectionType == "NARRATIVE" && w.Parent < 1)) // 부모에 해당하는 객체만 검색
            {
                CreateSectionTextItems<T>(n, obj, cdaObject);
            }
        }

        private static void CreateSectionTextItems<T>(SectionPart narrative, T obj, CDAObject cdaObject)
        {
            switch (narrative.Detail.ToLower())
            {
                case "table":
                    //from = DateTime.Now;
                    SectionTextItems.Add(CreateTable<T>(narrative, obj, cdaObject));
                    //Logger.TimespanLog("CreateTable<T> - " + obj.ToString() + " - ", from);
                    break;
                case "table2":
                    //from = DateTime.Now;
                    SectionTextItems.Add(CreateTable2<T>(narrative, obj, cdaObject));
                    //Logger.TimespanLog("CreateTable2<T> - " + obj.ToString() + " - ", from);
                    break;
                case "list":
                    //from = DateTime.Now;
                    SectionTextItems.Add(CreateList<T>(narrative, obj, cdaObject));
                    //Logger.TimespanLog("CreateList<T> - " + obj.ToString() + " - ", from);
                    break;
                case "paragraph":
                case "content":
                case "footnote":
                case "footnoteRef":
                case "sub":
                case "sup":
                    //from = DateTime.Now;
                    SectionTextItems.AddRange(CreateGenericSectionText<T>(narrative, obj, cdaObject, narrative.Detail));
                    //Logger.TimespanLog("CreateSectionText<T> - " + obj.ToString() + " - ", from);
                    break;
                case "br":
                    //from = DateTime.Now;
                    SectionTextItems.AddRange(CreateBr(narrative));
                    //Logger.TimespanLog("CreateBr<T> - " + obj.ToString() + " - ", from);
                    break;
                case "renderMultiMedia":
                    //from = DateTime.Now;
                    SectionTextItems.AddRange(CreateRenderMultiMedia<T>(narrative, obj, cdaObject));
                    //Logger.TimespanLog("CreateRenderMultiMedia<T> - " + obj.ToString() + " - ", from);
                    break;
                case "bppc":  //동의서
                    //from = DateTime.Now;    
                    CreateConsentSection(obj as CDAObject);
                    //Logger.TimespanLog("CreateConsentSection<T> - " + obj.ToString() + " - ", from);
                    break;
                case "withdrawal": //철회서
                    //from = DateTime.Now;
                    CreateWithdrawalSection(obj as CDAObject);
                    //Logger.TimespanLog("CreateWithdrawalSection<T> - " + obj.ToString() + " - ", from);
                    break;
                case "text":
                    //from = DateTime.Now;
                    Section.text.Text = new string[] { narrative.BodyStructureList.Where(w => w.ValueType == "STATIC").FirstOrDefault().Value };
                    //Logger.TimespanLog("text<T> - " + obj.ToString() + " - ", from);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region : Section<T, U>
        public static POCD_MT000040Component3 CreateSection_EntryList<T, U>(CDAObject cdaObject, Section _section, U obj)
        {
            if (_section == null) throw new Exception("Not proper Section info!");

            Component3 = new POCD_MT000040Component3();

            try
            {
                CreateSectionHead(_section);
                //IList<T> objectList = obj != null ? obj as IList<T> : null;
                T[] objectList = obj != null ? obj as T[] : null;
                if (obj == null || objectList == null || objectList.Count() == 0)
                {
                    Section.nullFlavor = "NI";
                    Section.text = new StrucDocText() { Text = new string[] { "정보 없음" } };
                }
                else
                {
                    SectionTextItems = new List<object>();

                    //duration = DateTime.Now;
                    CreateNarrative<T, U>(_section, obj, cdaObject);
                    //Logger.TimespanLog("CreateNarrative - (" + _section.Code + "), " + obj.ToString() + " - ", duration);

                    //duration = DateTime.Now;
                    CreateEntry<T, U>(cdaObject, _section, obj);
                    //Logger.TimespanLog("CreateEntry -     (" + _section.Code + "), " + obj.ToString() + " - ", duration);

                    Section.text.Items = SectionTextItems.ToArray();
                }

                Component3.section = Section;
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                throw e;
            }

            return Component3;
        }

        private static void CreateEntry<T, U>(CDAObject cdaObject, Section _section, U obj)
        {
            Section.entry = entryLogic.CreateSectionEntries<T, U>(cdaObject, _section, obj);
        }

        private static void CreateNarrative<T, U>(Section _section, U obj, CDAObject cdaObject)
        {
            SectionPart[] narratives = _section.NarrativeList;
            if (narratives == null) return;

            //narratives.Where(w => w.SectionType == "NARRATIVE" && w.Parent < 1).AsParallel().ForAll(nr => CreateNarrative<T, U>(obj, cdaObject, nr));
            foreach (SectionPart narrative in narratives.Where(w => w.SectionType == "NARRATIVE" && w.Parent < 1)) // 부모객체만 선택
            {
                CreateSectionTextItems<T, U>(obj, cdaObject, narrative);
            }
        }

        private static void CreateSectionTextItems<T, U>(U obj, CDAObject cdaObject, SectionPart narrative)
        {
            //Logger.TimespanLog(narrative.Detail, DateTime.Now);

            switch (narrative.Detail.ToLower())
            {
                case "table":
                    SectionTextItems.Add(CreateTable<T>(narrative, obj, cdaObject));
                    break;
                case "table2":
                    SectionTextItems.Add(CreateTable2<T>(narrative, obj, cdaObject));
                    break;
                case "list":
                    SectionTextItems.Add(CreateList<T>(narrative, obj, cdaObject));
                    break;
                case "paragraph":
                case "content":
                case "footnote":
                case "footnoteRef":
                case "sub":
                case "sup":
                    SectionTextItems.AddRange(CreateGenericSectionText<T>(narrative, obj, cdaObject, narrative.Detail));
                    break;
                case "br":
                    SectionTextItems.AddRange(CreateBr(narrative));
                    break;
                case "renderMultiMedia":
                    SectionTextItems.AddRange(CreateRenderMultiMedia<T>(narrative, obj, cdaObject));
                    break;
                case "text":
                    Section.text.Text = new string[] { narrative.BodyStructureList.Where(w => w.Path.Contains("text") && w.ValueType.Equals("STATIC")).FirstOrDefault().Value };
                    break;
                default:
                    break;
            }
        }
        #endregion

        private static void CreateSectionHead(Section _section)
        {
            Component3 = new POCD_MT000040Component3();
            Section = new POCD_MT000040Section() { id = GetII(), title = GetST(_section.Title) };
            Section.templateId = GetTemplateId(_section.TemplateIdRoot, _section.TemplateIdExtension);
            Section.code = GetCE(_section.Code, _section.DisplayName, _section.CodeSystemName, _section.CodeSystem);
            Section.text = new StrucDocText();
            Component3.section = Section;
        }

        #region :: Narrative Logic

        #region : Table
        private static object CreateTable<T>(SectionPart narrative, object obj, CDAObject cdaObject)
        {
            Table = new StrucDocTable();

            CreateTableHead(narrative);

            Table.thead = Thead;

            CreateTableBody<T>(narrative, obj);

            Table.tbody = new StrucDocTbody[] { new StrucDocTbody() { tr = TbodyTrArray } };

            return Table;
        }

        private static void CreateTableHead(SectionPart narrative)
        {
            Thead = new StrucDocThead();
            ThArray = CommonQuery.GetBodyValues("table/thead/tr/th", narrative).Select(s => new StrucDocTh() { Text = new string[] { s } }).ToArray();
            Thead.tr = new StrucDocTr[] { new StrucDocTr() { Items = ThArray } };
            List<string> widths = CommonQuery.GetBodyValues("table/colgroup/col/@width", narrative);
            if (widths != null && widths.Any())
            {
                Table.Items = new object[] { new StrucDocColgroup() { col = widths.Select(s => new StrucDocCol() { width = s }).ToArray() } };
            }
        }

        private static void CreateTableBody<TSource>(SectionPart narrative, object obj)
        {
            TbodyTrArray = null;
            TrList = new List<StrucDocTr>();

            IEnumerable<BodyStructure> bodyStructures = CommonQuery.GetBodyStructures("table/tbody/tr/td", narrative).Where(w => w.ValueType == "OBJECT");
            IEnumerable<StrucDocTdAlign> tdAligns = CommonQuery.GetBodyValues("table/tbody/tr/td/@align", narrative).
                Select(s => (StrucDocTdAlign)Enum.Parse(typeof(StrucDocTdAlign), s, ignoreCase: true));

            if (bodyStructures != null & bodyStructures.Any())
            {
                //if (obj.GetType().IsGenericType)
                if (obj.GetType().IsArray)
                {
                    //IList<TSource> list = (IList<TSource>)obj;
                    TSource[] array = (TSource[])obj;
                    int trIndex = 0;
                    //TrList.AddRange(list.Select(t => CreateStrucDocTr(t, bodyStructures, trIndex++, tdAligns)));
                    TrList.AddRange(array.Select(t => CreateStrucDocTr(t, bodyStructures, trIndex++, tdAligns)));
                    TbodyTrArray = TrList.ToArray();
                }
                else
                {
                    int _trIndex = 0;
                    TrList.AddRange(new List<StrucDocTr>() { CreateStrucDocTr((TSource)obj, bodyStructures, _trIndex++, tdAligns) });
                    TbodyTrArray = TrList.ToArray();
                }
            }
            else return;
        }

        private static StrucDocTr CreateStrucDocTr<TSource>(
            TSource t,
            IEnumerable<BodyStructure> bodyStructures,
            int trIndex,
            IEnumerable<StrucDocTdAlign> tdAligns)
        {
            Tr = new StrucDocTr();
            StrucdocTdList = new List<StrucDocTd>();

            int tdIndex = 0;

            ModelBase model = t as ModelBase;
            model.TableBodyArray = DynamicVariales<TSource>(t, bodyStructures.Select(s => s.Property).ToArray());

            StrucdocTdList.AddRange(model.TableBodyArray.Select(d => CreateStrucDocTd(d, tdIndex++, trIndex, tdAligns, t)));
            Tr.Items = StrucdocTdList.ToArray();
            return Tr;
        }

        private static StrucDocTd CreateStrucDocTd(string value, int tdIndex, int trIndex, IEnumerable<StrucDocTdAlign> tdAligns, object TSource = null)
        {
            StrucDocParagraph content;
            List<object> obj = new List<object>();
            if (!string.IsNullOrEmpty(value) && (value.Contains("\r\n") || value.Contains("\n")))
            {
                System.Text.RegularExpressions.Regex.Replace(value, "(?<!\r)\n", "\r\n");
                System.IO.StringReader sr = new System.IO.StringReader(value);
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        obj.Add(string.Empty);
                    }
                    else
                    {
                        content = new StrucDocParagraph();
                        content.Text = new string[] { line };
                        obj.Add(content);
                    }
                }
                return Td = new StrucDocTd()
                {
                    align = tdAligns.ElementAtOrDefault(tdIndex),
                    Items = obj.ToArray()
                };
            }
            else
            {
                return CreateTd(tdAligns.ElementAtOrDefault(tdIndex), trIndex, value, TSource);
            }
        }

        private static StrucDocTd CreateTd(StrucDocTdAlign alignType, int index, string value, object TSouce = null)
        {
            if (value.IsURL())
            {
                string accessionNumber = null; // 170920 추가
                string kosuid = null; // 171121
                if (TSouce != null &&
                    TSouce.GetType().GetProperty("AccessionNumber") != null &&
                    TSouce.GetType().GetProperty("KosUid") != null &&
                    TSouce.GetType().GetProperty("AccessionNumber").GetValue(TSouce, null) != null &&
                    TSouce.GetType().GetProperty("KosUid").GetValue(TSouce, null) != null)
                {
                    accessionNumber = TSouce.GetType().GetProperty("AccessionNumber").GetValue(TSouce, null).ToString();
                    kosuid = TSouce.GetType().GetProperty("KosUid").GetValue(TSouce, null).ToString();
                    return Td = new StrucDocTd() { align = alignType, Items = new object[] { new StrucDocLinkHtml() { ID = string.Format("PACS{0}", index), href = value, rel = accessionNumber, rev = kosuid } } };
                }
                else
                {
                    return Td = new StrucDocTd() { align = alignType, Text = new string[] { value } };
                }
                //return Td = new StrucDocTd() { align = alignType, Items = new object[] { new StrucDocLinkHtml() { ID = string.Format("PACS{0}", index), href = value } } };
                //return Td = new StrucDocTd() { align = alignType, Items = new object[] { new StrucDocLinkHtml() { ID = string.Format("PACS{0}", index), href = value, rel = accessionNumber } } };
            }
            else
            {
                return Td = new StrucDocTd() { align = alignType, Text = new string[] { value } };
            }
        }

        #endregion

        #region : List
        private static object CreateList<TSource>(SectionPart narrative, object obj, CDAObject cdaObject) // 수정중
        {
            IEnumerable<BodyStructure> bodyStructures = CommonQuery.GetBodyStructures("list/item", narrative).Where(w => w.ValueType == "OBJECT");
            if (bodyStructures == null || bodyStructures.Count() < 1) return null;

            string[] variales = DynamicVariales(cdaObject, bodyStructures.Select(s => s.Property).ToArray());

            //if (obj.GetType().IsGenericType)
            if (obj.GetType().IsArray)
            {
                //IList<TSource> list = (IList<TSource>)obj;
                TSource[] array = (TSource[])obj;
                strucDocItems = new List<StrucDocItem>();

                if (narrative.Children != null)
                {
                    int itemIndex = 0;
                    strucDocItems.AddRange(narrative.Children.Select(s => CreateStrucdocItem<TSource>(s, array, variales, itemIndex++)));
                    return strucDocList = new StrucDocList() { item = strucDocItems.ToArray() };
                }
                else return null;
            }
            else
            {
                TSource temp = (TSource)obj;
                if (temp.GetType() == typeof(string))
                {
                    return strucDocList = new StrucDocList() { item = new StrucDocItem[] { new StrucDocItem() { Text = new string[] { temp.ToString() } } } };
                }
                else // model 인 경우
                {
                    ModelBase model = temp as ModelBase;
                    model.TableBodyArray = DynamicVariales<TSource>(temp, bodyStructures.Select(t => t.Property).ToArray());
                    strucDocItems = new List<StrucDocItem>();
                    strucDocItems.AddRange(model.TableBodyArray.Select(d => new StrucDocItem() { Text = new string[] { d } }));
                    return strucDocList = new StrucDocList() { item = strucDocItems.ToArray() };
                }
            }
        }

        private static StrucDocItem CreateStrucdocItem<TSource>(SectionPart children, TSource[] sourceArray, string[] variales, int itemIndex)
        {
            strucDocItem = new StrucDocItem();
            List<TSource> newlist = new List<TSource>();
            TSource[] newArray = new TSource[] { };
            //SetNewCdaObjectList(children, sourceList, newlist);
            SetNewCdaObjectList(children, sourceArray, ref newArray);

            //if (newlist.Any())
            if (newArray.Any())
            {
                strucDocItem.Text = variales != null ? new string[] { variales.ElementAtOrDefault(itemIndex) } : new string[] { string.Empty };
                switch (children.Detail.ToLower())
                {
                    case "table":
                        //strucDocItem.Items = new object[] { CreateTable<TSource>(children, newlist.ToArray(), cdaObject), string.Empty };
                        strucDocItem.Items = new object[] { CreateTable<TSource>(children, newArray, cdaObject), string.Empty };
                        break;
                    case "paragraph":
                        //strucDocItem.Items = new object[] { CreateGenericSectionText<TSource>(children, newlist, cdaObject), string.Empty };
                        strucDocItem.Items = new object[] { CreateGenericSectionText<TSource>(children, newArray, cdaObject), string.Empty };
                        break;
                    default:
                        break;
                }
            }
            else
            {
                strucDocItem = null;
            }
            return strucDocItem;
        }

        private static void SetNewCdaObjectList<TSource>(SectionPart children, TSource[] sourceArray, ref TSource[] newArray)
        {
            List<TSource> newList = new List<TSource>();
            foreach (var item in sourceArray)
            {
                if (!string.IsNullOrEmpty(children.Value) && !string.IsNullOrEmpty(children.ValueType))
                {
                    PropertyInfo prop = item.GetType().GetProperty(children.ValueType);
                    if (prop != null && prop.GetValue(item, null).ToString() == children.Value)
                    {
                        //newList.Add(item);
                        newArray = ArrayHandler.Add<TSource>(newArray, item);
                    }
                }
            }
            //newArray = newList.ToArray();
        }

        #endregion

        #region : RenderMultiMedia
        private static List<object> CreateRenderMultiMedia<TSource>(SectionPart narrative, object obj, CDAObject cdaObject)
        {
            //List<string> values = new List<string>();
            IEnumerable<BodyStructure> bodyStructures = CommonQuery.GetBodyStructures("renderMultiMedia/caption", narrative).Where(w => w.ValueType == "OBJECT");
            List<object> textItems = new List<object>();
            //if (obj.GetType().IsGenericType && bodyStructures != null && bodyStructures.Any())
            if (obj.GetType().IsArray && bodyStructures != null && bodyStructures.Any())
            {
                //IList<TSource> list = (IList<TSource>)obj;
                TSource[] list = (TSource[])obj;
                //values.AddRange(bodyStructures.Select(s => s.Property));
                foreach (TSource item in list)
                {
                    ModelBase model = item as ModelBase;
                    model.TableBodyArray = DynamicVariales<TSource>(item, bodyStructures.Select(s => s.Property).ToArray());
                    textItems.AddRange(model.TableBodyArray.Select(s => (object)new StrucDocRenderMultiMedia() { caption = new StrucDocCaption() { Text = new string[] { s } } }));
                }
                return textItems;
            }
            else
            {
                TSource temp = (TSource)obj;
                if (temp.GetType() == typeof(string))
                {
                    textItems.Add(new StrucDocRenderMultiMedia() { caption = new StrucDocCaption() { Text = new string[] { temp.ToString() } } });
                }
                else
                {
                    ModelBase model = temp as ModelBase;
                    model.TableBodyArray = DynamicVariales<TSource>(temp, bodyStructures.Select(s => s.Property).ToArray());
                    textItems.AddRange(model.TableBodyArray.Select(s => (object)new StrucDocRenderMultiMedia() { caption = new StrucDocCaption() { Text = new string[] { s } } }));
                }
                //textItems.Add(new StrucDocRenderMultiMedia() { caption = new StrucDocCaption() { Text = new string[] { obj.ToString() } } });
                return textItems;
            }
        }
        #endregion

        #region : Br
        private static List<object> CreateBr(SectionPart narrative)
        {
            IEnumerable<BodyStructure> bodyStructures = CommonQuery.GetBodyStructures("br", narrative).Where(w => w.ValueType == "OBJECT");

            if (bodyStructures != null && bodyStructures.Any())
            {
                return bodyStructures.Select(d => string.Empty).Cast<object>().ToList();
            }
            else return null;
        }
        #endregion

        #region : Table 2
        private static object CreateTable2<TSource>(SectionPart narrative, object obj, CDAObject cdaObject) // 법정 감염성 전염병
        {
            Table = new StrucDocTable();
            IEnumerable<IGrouping<int, BodyStructure>> bodystructures = narrative.BodyStructureList.Where(w => w.Group > 0).GroupBy(g => g.Group);
            TrList = new List<StrucDocTr>();

            foreach (IGrouping<int, BodyStructure> bodystructure in bodystructures)
            {
                Tr = new StrucDocTr();
                List<object> items = new List<object>();
                foreach (var item in bodystructure)
                {
                    if (item.Path.EndsWith("th"))
                    {
                        StrucDocTh th = new StrucDocTh() { Text = new string[] { item.Value } };
                        items.Add(th);
                    }
                    if (item.Path.EndsWith("td"))
                    {
                        PropertyInfo prop = obj.GetType().GetProperty(item.Property);
                        StrucDocTd td = new StrucDocTd() { Text = new string[] { prop != null ? prop.GetValue(obj, null).ToString().StringToDatetime() : null } };
                        items.Add(td);
                    }
                    if (item.Path.EndsWith("@align"))
                    {
                        PropertyInfo prop = items.Last().GetType().GetProperty("align");
                        prop.SetValue(items.Last(), Enum.Parse(typeof(StrucDocTdAlign), item.Value, true), null);
                    }
                    if (item.Path.EndsWith("@colspan"))
                    {
                        items.Last().GetType().GetProperty("colspan").SetValue(items.Last(), item.Value, null);
                    }
                }
                Tr.Items = items.ToArray();
                TrList.Add(Tr);
            }

            Table.tbody = new StrucDocTbody[] { new StrucDocTbody() { tr = TrList.ToArray() } };
            return Table;
        }
        #endregion

        #region : Narrative Generic
        private static List<object> CreateGenericSectionText<TSource>(SectionPart narrative, object obj, CDAObject cdaObejct, string narraviteType = null)
        {
            textItems = new List<object>();
            IEnumerable<BodyStructure> bodyStructures = CommonQuery.GetBodyStructures(narraviteType.ToLower(), narrative).Where(w => w.ValueType == "OBJECT");

            if (bodyStructures != null && bodyStructures.Any())
            {
                //if (obj.GetType().IsGenericType)
                if (obj.GetType().IsArray)
                {
                    //IList<TSource> list = (IList<TSource>)obj;
                    TSource[] list = (TSource[])obj;
                    list.ToList().ForEach(t => CreateGenericSectionTextItem(t, bodyStructures, narraviteType));
                }
                else
                {
                    TSource t = (TSource)obj;
                    CreateGenericSectionTextItem(t, bodyStructures, narraviteType);
                }
                return textItems;
            }
            else return null;
        }

        private static void CreateGenericSectionTextItem<TSource>(TSource t, IEnumerable<BodyStructure> bodyStructures, string narrativeType = null)
        {
            if (t.GetType() == typeof(string))
            {
                //if (!string.IsNullOrEmpty(value) && ( value.Contains("\r\n") || value.Contains("\n") ))
                //{
                //    System.Text.RegularExpressions.Regex.Replace(value, "(?<!\r)\n", "\r\n");
                //StrucDocParagraph content;
                List<string> lines = new List<string>();
                if (!string.IsNullOrEmpty(t.ToString()) && (t.ToString().Contains("\r\n") || t.ToString().Contains("\n")))
                {
                    System.Text.RegularExpressions.Regex.Replace(t.ToString(), "(?<!\r)\n", "\r\n");
                    System.IO.StringReader sr = new System.IO.StringReader(t.ToString());
                    string line = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines.Add(string.IsNullOrEmpty(line) ? string.Empty : line);
                    }
                    textItems.AddRange(lines.Select(s => CreateSchemaTextItem(narrativeType, s)).ToList());
                }
                else
                {
                    textItems.Add(CreateSchemaTextItem(narrativeType, t.ToString()));
                }

                //if (!string.IsNullOrEmpty(t.ToString()) && t.ToString().GetLinesCount() > 0)
                //{
                //    IEnumerable<string> lineArray = t.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                //    textItems.AddRange(lineArray.Select(s => CreateSchemaTextItem(narrativeType, s)).ToList());
                //}
                //else
                //{
                //    textItems.Add(CreateSchemaTextItem(narrativeType, t.ToString()));
                //}
            }
            else
            {
                ModelBase model = t as ModelBase;
                model.TableBodyArray = DynamicVariales<TSource>(t, bodyStructures.Select(s => s.Property).ToArray());
                model.TableBodyArray.ToList().All(a =>
                {
                    List<string> lines = new List<string>();
                    //if (!string.IsNullOrEmpty(a) && a.Contains("\r\n"))
                    //if (!string.IsNullOrEmpty(a) && a.GetLinesCount() > 0)
                    //{
                    if (!string.IsNullOrEmpty(a.ToString()) && (a.ToString().Contains("\r\n") || a.ToString().Contains("\n")))
                    {
                        System.Text.RegularExpressions.Regex.Replace(a.ToString(), "(?<!\r)\n", "\r\n");
                        System.IO.StringReader sr = new System.IO.StringReader(a);
                        string line = null;
                        while ((line = sr.ReadLine()) != null)
                        {
                            lines.Add(string.IsNullOrEmpty(line) ? string.Empty : line);
                        }
                        textItems.AddRange(lines.Select(s => CreateSchemaTextItem(narrativeType, s)).ToList());
                        //IEnumerable<string> lineArray = a.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                        //textItems.AddRange(lineArray.Select(s => CreateSchemaTextItem(narrativeType, s)).ToList());
                    }
                    else
                    {
                        textItems.Add(CreateSchemaTextItem(narrativeType, a));
                    }
                    return true;
                });
            }
        }

        private static object CreateSchemaTextItem(string narraviteType, object value)
        {
            object obj = null;
            obj = Activator.CreateInstance("ezHIE.GLOBAL.Model.CDA.STD", string.Format("StrucDoc{0}", narraviteType.First().ToString().ToUpper() + narraviteType.Substring(1))).Unwrap();
            PropertyInfo prop = obj.GetType().GetProperty("Text");

            //if (!string.IsNullOrEmpty(value.ToString()) && (value.ToString().Contains("\r\n") || value.ToString().Contains("\n")))
            //{
            //    List<string> lines = new List<string>();
            //    System.Text.RegularExpressions.Regex.Replace(value.ToString(), "(?<!\r)\n", "\r\n");
            //    System.IO.StringReader sr = new System.IO.StringReader(value.ToString());
            //    string line = null;
            //    while ((line = sr.ReadLine()) != null)
            //    {
            //        lines.Add(string.IsNullOrEmpty(line) ? string.Empty : line);
            //    }
            //    //textItems.AddRange(lines.Select(s => CreateSchemaTextItem(narrativeType, s)).ToList());                
            //}
            //else
            //{
            //    textItems.Add(CreateSchemaTextItem(narrativeType, a));
            //}

            if (prop != null)
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    prop.SetValue(obj, new string[] { value.ToString() }, null);
                    return obj;
                }
                else return string.Empty;
            }
            else return null;
        }
        #endregion

        #endregion

        #region :  Get Variables
        static string[] DynamicVariales<T>(T data, string[] param)
        {
            try
            {
                return param.Select(p => data.GetValue(p).StringToDatetime()).ToArray();
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                return null;
            }
        }
        #endregion

        #region :  BPPC
        internal static void CreateConsentSection(CDAObject item) //동의서 섹션추가
        {
            #region [  Header  ]

            SectionTextItems.Add(string.Empty);
            Paragraph = new StrucDocParagraph()
            {
                Text = new string[] { 
                "본인은 원활한 진료서비스 제공을 위하여 의료기관에서 작성한 본인의 개인정보 및 의무기록 등 진료정보를 " +
                "타 의료기관 내원 진료 시 활용(이하 “진료정보교류서비스”라 한다)하고, 진료정보교류서비스 평가 및 분석 " +
                "자료로 활용될 수 있다는 사실에 대해 충분한 설명을 듣고 이해하였으며, 이에 아래와 같이 동의합니다. " +
                "※ 동의서 보관방법 : 서면으로 제출한 본 동의서는 개별 의료기관에서 보관하지 않고, 전자적으로 변환하여 진료정보교류시스템(보건복지부)에서 보관합니다." }
            };
            SectionTextItems.Add(Paragraph);
            #endregion

            #region [  Table  ]

            Table = new StrucDocTable() { border = "1" };
            TrList = new List<StrucDocTr>();
            Tbody = new StrucDocTbody();
            //StrucDocTbody tbody1 = new StrucDocTbody();
            //16.11.08
            #region [ 동의주체 ]
            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {
                    Th = new StrucDocTh() { Text = new string[] { "동의주체" }, rowspan = "3" },
                    Th = new StrucDocTh() { Text = new string[] { "성 명" }  },
                    Td = new StrucDocTd() { Text = new string[] { item.Patient.PatientName  } },
                    Th = new StrucDocTh() { Text = new string[] { "전화번호" }  },
                    Td = new StrucDocTd() { Text = new string[] { item.Patient.TelecomNumber  } }
                }
            };
            TrList.Add(Tr);

            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {
                    Th = new StrucDocTh() { Text = new string[] { "생년월일(주민등록번호)" } },                    
                    Td = new StrucDocTd() { Text = new string[] { item.Patient.DateOfBirth  }, colspan = "3" }                    
                }
            };
            TrList.Add(Tr);

            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {
                    new StrucDocTh() { Text = new string[] { "주 소" } },                    
                    new StrucDocTd() { Text = new string[] { string.Format("{0} {1} {2}", item.Patient.AdditionalLocator, item.Patient.StreetAddress, item.Patient.PostalCode) }, colspan = "3" }                    
                }
            };
            TrList.Add(Tr);

            Tbody.tr = TrList.ToArray();
            Table.tbody = new StrucDocTbody[] { Tbody };

            SectionTextItems.Add(Table);
            #endregion

            Table = new StrucDocTable() { border = "1" };
            TrList = new List<StrucDocTr>();
            Tbody = new StrucDocTbody();
            #region [ 제공하는 정보항목 ]
            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {                    
                    Th = new StrucDocTh() { Text = new string[] { "제공하는 정보 항목" }, rowspan = "3" },
                    Th = new StrucDocTh() { Text = new string[] { "[1] 개인정보" } },
                    Td = new StrucDocTd() { Text = new string[] { "성명, 연락처(법정대리인의 성명, 연락처), 주소" } }
                }
            };
            TrList.Add(Tr);

            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {
                    Th = new StrucDocTh() { Text = new string[] { "[2] 고유식별정보" } },
                    Td = new StrucDocTd() { Text = new string[] { "주민등록번호, 외국인등록번호" } }
                }
            };
            TrList.Add(Tr);


            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {
                    Th = new StrucDocTh() { Text = new string[] { "[3] 민감정보" } },
                    Td = new StrucDocTd()
                    {                        
                        Items = new object[] 
                        {
                            Content = new StrucDocContent(){ Text = new string[] { "① 수진일별 처방 내용(약 처방, 검사 내역)" }  },                            
                            string.Empty,
                            Content = new StrucDocContent(){ Text = new string[]{ "② 진단내용" } },
                            string.Empty,
                            Content = new StrucDocContent(){ Text = new string[]{ "③ 수술을 시행하는 경우 수술에 관한 내용(수술일, 수술명 등 진료기록지 등에 기재되는 일체의 수술정보)" } },
                            string.Empty,
                            Content = new StrucDocContent(){ Text = new string[]{ "④ 외래접수 정보(진료과, 진료의․주치의)" } },
                            string.Empty,
                            Content = new StrucDocContent(){ Text = new string[]{ "⑤ 입퇴원정보(입원일, 퇴원일 등 일체의 입퇴원 정보)" } },
                            string.Empty,
                            Content = new StrucDocContent(){ Text = new string[]{ "⑥ 예약정보(일시, 내용 등 일체의 예약정보)" } },
                            string.Empty,
                            Content = new StrucDocContent(){ Text = new string[]{ "⑦ 예방접종 내역 ⑧ 알레르기 및 부작용 정보" } },
                            string.Empty,
                            Content = new StrucDocContent(){ Text = new string[]{ "⑨ 생체신호 정보 및 흡연상태, ⑩ 신고된 법정 전염성 감염병 정보" } },
                            string.Empty,
                            Content = new StrucDocContent(){ Text = new string[]{ "⑪ 환자상태 및 회송사유" } }
                        }
                    }
                }
            };
            TrList.Add(Tr);
            #endregion
            #region [ 제공․이용의 목적 ]
            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {
                    Th = new StrucDocTh() { Text = new string[] { "제공․이용의 목적" } },
                    Td = new StrucDocTd()
                    { 
                        colspan = "2", 
                        Items = new object[]
                        {
                            Content = new StrucDocContent() { Text = new string[]{ "① 진료정보교류시스템을 활용한 의료인간 진료정보 전달로 진료서비스의 원활한 제공" } },
                            string.Empty,
                            Content = new StrucDocContent() { Text = new string[]{ "② 경제적·의학적 편익분석을 위하여 행해지는 진료정보교류서비스에 대한 평가·분석" } }                            
                        }
                    }
                }
            };
            TrList.Add(Tr);
            #endregion
            #region [ 제공받는 자 ]
            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {
                    Th = new StrucDocTh() { Text = new string[] { "제공받는 자" } },
                    Td = new StrucDocTd()
                    { 
                        colspan = "2", 
                        Items = new object[]
                        {
                            Content = new StrucDocContent() { Text = new string[]{ "① 본인이 내원하여 진료서비스를 제공받는 의료기관 " } },
                            string.Empty,
                            Content = new StrucDocContent() { Text = new string[]{ "(다만, 진료정보교류시스템을 사용하는 의료기관에 한정)" } },
                            string.Empty,
                            Content = new StrucDocContent() { Text = new string[]{ "② 보건복지부 (동의서관리, 진료정보교류서비스 평가자료로 한정)" } }
                        }
                    }
                }
            };
            TrList.Add(Tr);
            #endregion
            #region [ 보유 및 이용기간 ]
            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {
                    Th = new StrucDocTh() { Text = new string[] { "보유 및 이용기간" } },
                    Td = new StrucDocTd() 
                    { 
                        colspan = "2", 
                        Items = new object[]
                        {
                            Content = new StrucDocContent() { Text = new string[]{ "① 본 동의서의 유효기간은 “진료정보교류에 관한 개인정보 제공동의(전체) 철회서” 제출 전까지입니다." } },
                            string.Empty,
                            Content = new StrucDocContent() { Text = new string[]{ "② 본인(법정대리인)이 진료정보교류에 관한 개인정보 제공동의를 철회하는 경우 개인정보보호법제36조 및 제37조에 의하여 즉시 삭제 및 처리를 정지합니다." } },
                            string.Empty,
                            Content = new StrucDocContent() { Text = new string[]{ "③ 다만 위 제①,②항에도 불구하고 법령에 의하여 의무적으로 보존이 필요한 기간 동안에는 해당 정보가 보유됩니다." } }
                        }
                    }
                }
            };
            TrList.Add(Tr);
            #endregion
            #region [ 동의거부 권리 및 동의 거부 따른 불이익 또는 제한사항 ]
            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {
                    Th = new StrucDocTh() { Text = new string[] { "동의 거부 권리 및 동의 거부 따른 불이익 또는 제한사항" } },
                    Td = new StrucDocTd() 
                    { 
                        colspan = "2", 
                        Items = new object[]
                        {
                            Content = new StrucDocContent(){ Text = new string[]{ "① 귀하는 개인정보 제공 동의를 거부할 권리가 있습니다." } },
                            string.Empty,
                            Content = new StrucDocContent(){ Text = new string[]{ "② 동의를 거부할 경우 별도의 불이익은 없으며, 다만 진료정보교류시스템을 활용하지 못함으로써 진료를 의뢰하는 경우 진료접수 대기시간이 소요될 수 있으며 별도의 의무기록송부절차가 필요함을 알려드립니다." } }                            
                        }
                    }
                }
            };
            #endregion

            #region [ 동의 제외사항 ]
            List<StrucDocParagraph> departmentNames = new List<StrucDocParagraph>();
            List<StrucDocParagraph> departmentCodes = new List<StrucDocParagraph>();
            if (item.Consent.ExceptDepartmentCodes != null && item.Consent.ExceptDepartmentCodes.Any())
            {
                foreach (var code in item.Consent.ExceptDepartmentCodes)
                {
                    string codeName = null;
                    codeName = CommonQuery.GetCodeValue(code, "NAME");
                    departmentNames.Add(new StrucDocParagraph() { Text = new string[] { codeName } });
                    departmentCodes.Add(new StrucDocParagraph() { Text = new string[] { code } });
                }
            }

            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {                    
                    Th = new StrucDocTh() { Text = new string[] { "동의 제외 사항" }, rowspan = "2" },
                    Th = new StrucDocTh() { Text = new string[] { "진료과명" } },
                    Td = new StrucDocTd() { Items = departmentNames.ToArray() }                    
                }
            };
            TrList.Add(Tr);

            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {
                    Th = new StrucDocTh() { Text = new string[] { "진료과 코드" } },
                    Td = new StrucDocTd() { Items = departmentCodes.ToArray() },
                }
            };
            TrList.Add(Tr);
            #endregion

            Tbody.tr = TrList.ToArray();
            Table.tbody = new StrucDocTbody[] { Tbody };
            SectionTextItems.Add(Table);
            #endregion

            #region [  Bottom  ]
            SectionTextItems.Add(string.Empty);
            SectionTextItems.Add(Paragraph = new StrucDocParagraph() { Text = new string[] { "본인(법정대리인)은 [1]개인정보의 제공․이용에 동의합니다." } });
            SectionTextItems.Add(Paragraph = new StrucDocParagraph() { Text = new string[] { "본인(법정대리인)은 [2]고유식별정보(주민등록번호)의 제공․이용에 동의합니다." } });
            SectionTextItems.Add(Paragraph = new StrucDocParagraph() { Text = new string[] { "본인(법정대리인)은 [3]민감(진료)정보의 제공․이용에 동의합니다." } });
            SectionTextItems.Add(string.Empty);
            SectionTextItems.Add(Paragraph = new StrucDocParagraph() { Text = new string[] { item.Consent.ConsentTime.StringToDatetime("yyyy년 MM월 dd일") } });
            SectionTextItems.Add(string.Empty);

            #region [ 이름 / 연락처 / 관계 ]
            string personName = null;
            string telecomNumber = null;
            string relationship = null;

            if (item.Guardian != null)
            {
                personName = item.Guardian.GuardianName;
                telecomNumber = item.Guardian.TelecomNumber;
                switch (item.Guardian.GType)
                {
                    case GuardianType.Father:
                        relationship = "부";
                        break;
                    case GuardianType.Mother:
                        relationship = "모";
                        break;
                    case GuardianType.GrandMother:
                        relationship = "조모";
                        break;
                    case GuardianType.GrandFather:
                        relationship = "조부";
                        break;
                    case GuardianType.Wife:
                        relationship = "부인";
                        break;
                    case GuardianType.Husband:
                        relationship = "남편";
                        break;
                    case GuardianType.Family:
                        relationship = "가족";
                        break;
                    case GuardianType.Son:
                        relationship = "아들";
                        break;
                    case GuardianType.Daughther:
                        relationship = "딸";
                        break;
                    case GuardianType.GrandDaughter:
                        relationship = "손녀";
                        break;
                    case GuardianType.GrandSon:
                        relationship = "손자";
                        break;
                    case GuardianType.Neighbor:
                        relationship = "이웃";
                        break;
                    case GuardianType.Roommate:
                        relationship = "동거인";
                        break;
                    case GuardianType.Self:
                        relationship = "본인";
                        break;
                    default:
                        break;
                }
            }
            else if (item.Consent.Relationship != RelationshipType.Myself)
            {
                personName = item.Consent.ConsentSubjectName;
                telecomNumber = item.Consent.ConsentSubjectContact;
                relationship = item.Consent.Relationship.GetDescription();
            }
            else
            {
                personName = item.Patient.PatientName;
                telecomNumber = item.Patient.TelecomNumber;
                relationship = "본인";
            }

            SectionTextItems.Add(Paragraph = new StrucDocParagraph() { Text = new string[] { string.Format("환자(대리인 또는 보호자) 이름: {0}", personName) }, ID = "personname" });
            SectionTextItems.Add(Paragraph = new StrucDocParagraph() { Text = new string[] { string.Format("연락처: {0}", telecomNumber) }, ID = "persontelecom" });
            SectionTextItems.Add(Paragraph = new StrucDocParagraph() { Text = new string[] { string.Format("환자와의 관계: {0}", relationship) }, ID = "relationship" });
            SectionTextItems.Add(string.Empty);
            #endregion
            #endregion
        }
        #endregion

        #region :  Withdrawal
        internal static void CreateWithdrawalSection(CDAObject item)
        {
            //Section.text = new StrucDocText();         
            #region [  Header-Table  ]
            Table = new StrucDocTable() { border = "1" };
            TrList = new List<StrucDocTr>();
            Tbody = new StrucDocTbody();
            Tr = new StrucDocTr()
            {
                Items = new object[]
                {                    
                    Th = new StrucDocTh()
                    { 
                        Items = new object[]
                        {
                            Content = new StrucDocContent() { Text = new string[]{ "접수일" }},
                            string.Empty,
                            Content = new StrucDocContent() { Text = new string[]{ DateTime.Now.ToString("yyyy-MM-dd") }}
                        }
                    },
                    Th = new StrucDocTh()
                    { 
                        Items = new object[]
                        {
                            Content = new StrucDocContent() { Text = new string[]{ "접수기관" }},
                            string.Empty,
                            Content = new StrucDocContent() { Text = new string[]{ item.Custodian.CustodianName }}
                        }
                    },                    
                    Th = new StrucDocTh() { Text = new string[] { "처리기간 10일 이내"} },
                }
            };
            TrList.Add(Tr);
            Tbody.tr = TrList.ToArray();
            Table.tbody = new StrucDocTbody[] { Tbody };

            SectionTextItems.Add(Table);
            #endregion

            #region [  Table  ]
            //161107 추가 부분            
            Table = new StrucDocTable() { border = "1" };
            Tbody = new StrucDocTbody();
            TrList = new List<StrucDocTr>();
            #region [ 동의주체 ]
            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {
                    Th = new StrucDocTh() { Text = new string[] { "동의주체" }, rowspan = "3" },
                    Th = new StrucDocTh() { Text = new string[] { "성 명" } },
                    Td = new StrucDocTd() { Text = new string[] { item.Patient.PatientName } },
                    Th = new StrucDocTh() { Text = new string[] { "전화번호" } },
                    Td = new StrucDocTd() { Text = new string[] { item.Patient.TelecomNumber } }
                }
            };
            TrList.Add(Tr);
            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {
                    Th = new StrucDocTh() { Text = new string[] { "생년월일(주민등록번호)" } },                    
                    Td = new StrucDocTd() { Text = new string[] { item.Patient.DateOfBirth }, colspan = "4" }
                }
            };
            TrList.Add(Tr);

            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {
                    Th = new StrucDocTh() { Text = new string[] { "주 소" } },                    
                    Td = new StrucDocTd() { Text = new string[] { string.Format("{0} {1}", item.Patient.AdditionalLocator, item.Patient.StreetAddress)  }, colspan = "4" }
                }
            };
            TrList.Add(Tr);
            #endregion

            #region [ 철회내용 ]
            List<StrucDocParagraph> organizationNames = new List<StrucDocParagraph>();
            List<StrucDocParagraph> organizationOIDs = new List<StrucDocParagraph>();
            List<StrucDocParagraph> departmentNames = new List<StrucDocParagraph>();
            List<StrucDocParagraph> departmentCodes = new List<StrucDocParagraph>();
            if (item.Withdrawal.WithdrawalOrganizations != null && item.Withdrawal.WithdrawalOrganizations.Any())
            {
                item.Withdrawal.WithdrawalOrganizations.All(a =>
                {
                    organizationNames.Add(new StrucDocParagraph() { Text = new string[] { a.OrganizationName } });
                    organizationOIDs.Add(new StrucDocParagraph() { Text = new string[] { a.OID } });
                    return true;
                });

                //organizationNames.AddRange(item.Withdrawal.WithdrawalOrganizations.
                //    Select(s => Paragraph = new StrucDocParagraph() { Text = new string[] { s.Name }, ID = string.Format("o{0}", s.OID) }).ToList());
            }
            if (item.Withdrawal.WithdrawalDepartmentCodes != null && item.Withdrawal.WithdrawalDepartmentCodes.Any())
            {
                item.Withdrawal.WithdrawalDepartmentCodes.All(d =>
                {
                    departmentNames.Add(new StrucDocParagraph() { Text = new string[] { CommonQuery.GetCodeValue(d, "NAME") } });
                    departmentCodes.Add(new StrucDocParagraph() { Text = new string[] { d } });
                    return true;
                });

                //departmentNames.AddRange(item.Withdrawal.WithdrawalDepartmentCodes.
                //    Select(s => Paragraph = new StrucDocParagraph() { Text = new string[] { CommonQuery.GetCode<string>(new string[] { s }, "NAME") }, ID = string.Format("d{0}", s) }).ToList());
            }
            //organization
            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {
                    Th = new StrucDocTh() { Text = new string[] { "철회내용" }, rowspan = "7" },
                    Th = new StrucDocTh() { Text = new string[] { "부분 의료기관" }, rowspan = "3" },
                    Th = new StrucDocTh() { Text = new string[] { "의료기관명(동의 철회 요구)" } },
                    Td = new StrucDocTd() { Items = organizationNames.Any()? organizationNames.ToArray() : null, colspan = "2"  }
                }
            };
            TrList.Add(Tr);

            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {
                    Th = new StrucDocTh() { Text = new string[] { "의료기관 OID" } },
                    Td = new StrucDocTd() { Items = organizationOIDs.Any()? organizationOIDs.ToArray() : null, colspan = "2"  }
                }
            };
            TrList.Add(Tr);

            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {                    
                    Td = new StrucDocTd() { ID = "WithdrawalReason_Organization", Text = new string[]{ item.Withdrawal.WithdrawalOrganizationReason }, colspan = "3" }
                }
            };
            TrList.Add(Tr);

            //department
            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {                    
                    Th = new StrucDocTh() { Text = new string[] { "부분 진료과" }, rowspan = "3" },
                    Th = new StrucDocTh() { Text = new string[] { "진료과명(동의 철회 요구)" } },
                    Td = new StrucDocTd() { Items = departmentNames.Any()? departmentNames.ToArray() : null, colspan = "2"  }
                }
            };
            TrList.Add(Tr);

            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {
                    Th = new StrucDocTh() { Text = new string[] { "진료과 코드" } },
                    Td = new StrucDocTd() { Items = departmentCodes.Any()? departmentCodes.ToArray() : null, colspan = "2"  }
                }
            };
            TrList.Add(Tr);

            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {                    
                    Td = new StrucDocTd() { ID = "WithdrawalReason_Department", Text = new string[]{ item.Withdrawal.WithdrawalDepartmentReason }, colspan = "3" }
                }
            };
            TrList.Add(Tr);

            //Whole
            Tr = new StrucDocTr()
            {
                Items = new object[] 
                {                    
                    Th = new StrucDocTh() { Text = new string[] { "전체" } },                    
                    Td = new StrucDocTd() { ID = "WithdrawalReason_Whole", Text = new string[]{ item.Withdrawal.PolicyType == PrivacyPolicyType.ENTIRE_CONSENT ? item.Withdrawal.WholeWithdrawalReason : null }, colspan = "3" }
                }
            };
            TrList.Add(Tr);
            #endregion
            Tbody.tr = TrList.ToArray();
            Table.tbody = new StrucDocTbody[] { Tbody };
            SectionTextItems.Add(Table);
            SectionTextItems.Add(string.Empty);
            #endregion

            #region [  Bottom  ]
            SectionTextItems.Add(Paragraph = new StrucDocParagraph() { Text = new string[] { "개인정보보호법 등에 따라 위와 같이 요구합니다." } });
            SectionTextItems.Add(string.Empty);
            SectionTextItems.Add(Paragraph = new StrucDocParagraph() { Text = new string[] { item.Withdrawal.WithdrawalDate.StringToDatetime("yyyy년 MM월 dd일") } });
            SectionTextItems.Add(string.Empty);

            #region [ 이름 / 연락처 / 관계 ]
            string personName = null;
            string telecomNumber = null;
            string relationship = null;

            if (item.Guardian != null)
            {
                personName = item.Guardian.GuardianName;
                telecomNumber = item.Guardian.TelecomNumber;
                switch (item.Guardian.GType)
                {
                    case GuardianType.Father:
                        relationship = "부";
                        break;
                    case GuardianType.Mother:
                        relationship = "모";
                        break;
                    case GuardianType.GrandMother:
                        relationship = "조모";
                        break;
                    case GuardianType.GrandFather:
                        relationship = "조부";
                        break;
                    case GuardianType.Wife:
                        relationship = "부인";
                        break;
                    case GuardianType.Husband:
                        relationship = "남편";
                        break;
                    case GuardianType.Family:
                        relationship = "가족";
                        break;
                    case GuardianType.Son:
                        relationship = "아들";
                        break;
                    case GuardianType.Daughther:
                        relationship = "딸";
                        break;
                    case GuardianType.GrandDaughter:
                        relationship = "손녀";
                        break;
                    case GuardianType.GrandSon:
                        relationship = "손자";
                        break;
                    case GuardianType.Neighbor:
                        relationship = "이웃";
                        break;
                    case GuardianType.Roommate:
                        relationship = "동거인";
                        break;
                    case GuardianType.Self:
                        relationship = "본인";
                        break;
                    default:
                        break;
                }
            }
            else if (item.Withdrawal.Relationship != RelationshipType.Myself)
            {
                personName = item.Withdrawal.WithdrawalSubjectName;
                telecomNumber = item.Withdrawal.WithdrawalSubjectContact;
                relationship = item.Withdrawal.Relationship.GetDescription();
            }
            else
            {
                personName = item.Patient.PatientName;
                telecomNumber = item.Patient.TelecomNumber;
                relationship = "본인";
            }

            SectionTextItems.Add(Paragraph = new StrucDocParagraph() { Text = new string[] { string.Format("환자(대리인 또는 보호자) 이름: {0}", personName) }, ID = "personname" });
            SectionTextItems.Add(Paragraph = new StrucDocParagraph() { Text = new string[] { string.Format("연락처: {0}", telecomNumber) }, ID = "persontelecom" });
            SectionTextItems.Add(Paragraph = new StrucDocParagraph() { Text = new string[] { string.Format("환자와의 관계: {0}", relationship) }, ID = "relationship" });
            #endregion

            #endregion

            Section.text.Items = SectionTextItems.ToArray();
        }
        #endregion

        #region : AS - IS

        #region :  Narrative Block ( AS -IS )
        //private static StrucDocTr[] GetTbody<T>(object items)
        //{
        //    Type type = items.GetType();
        //    if (type.IsGenericType)
        //    {
        //        TrList = new List<StrucDocTr>();
        //        IList<T> list = (IList<T>)items;
        //        foreach (T item in list)
        //        {
        //            Tr = new StrucDocTr();
        //            PropertyInfo prop = item.GetType().GetProperty("TableBodyArray", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        //            List<StrucDocTd> strucDocTdList = new List<StrucDocTd>();
        //            if (prop != null)
        //            {
        //                string[] values = (string[])prop.GetValue(item, null);
        //                values.All(d =>
        //                {
        //                    if (!string.IsNullOrEmpty(d) && d.GetLinesCount() > 0)
        //                    {
        //                        List<object> obj = new List<object>();
        //                        System.IO.StringReader sr = new System.IO.StringReader(d);
        //                        string line = null;
        //                        while ((line = sr.ReadLine()) != null)
        //                        {
        //                            //추가
        //                            obj.Add(new StrucDocParagraph() { Text = new string[] { line } });
        //                        }
        //                        strucDocTdList.Add(new StrucDocTd() { Items = obj.ToArray() });
        //                    }
        //                    else
        //                    {
        //                        strucDocTdList.Add(new StrucDocTd() { Text = new string[] { d } });
        //                    }
        //                    return true;
        //                });

        //                TdArray = strucDocTdList.ToArray();
        //                Tr.Items = TdArray;
        //                TrList.Add(Tr);
        //            }
        //            else
        //            {
        //                return null;
        //            }
        //        }
        //        return TrList.ToArray();
        //    }
        //    else
        //    {
        //        TrList = new List<StrucDocTr>();
        //        Tr = new StrucDocTr();
        //        PropertyInfo prop = items.GetType().GetProperty("TableBodyArray", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        //        List<StrucDocTd> strucDocToList = new List<StrucDocTd>();
        //        if (prop != null)
        //        {
        //            string[] values = (string[])prop.GetValue(items, null);
        //            foreach (string val in values)
        //            {
        //                if (!string.IsNullOrEmpty(val) && val.GetLinesCount() > 0)
        //                {
        //                    List<object> obj = new List<object>();
        //                    System.IO.StringReader sr = new System.IO.StringReader(val);
        //                    string line = null;
        //                    while ((line = sr.ReadLine()) != null)
        //                    {
        //                        obj.Add(new StrucDocParagraph() { Text = new string[] { line } });
        //                    }
        //                    strucDocToList.Add(new StrucDocTd() { Items = obj.ToArray() });
        //                }
        //                else
        //                {
        //                    strucDocToList.Add(new StrucDocTd() { Text = new string[] { val } });
        //                }
        //            }
        //            TdArray = strucDocToList.ToArray();
        //            Tr.Items = TdArray;
        //            TrList.Add(Tr);
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //        return TrList.ToArray();
        //    }
        //}

        //private void CreateParagraph(List<string> item)
        //{
        //    Section.text = new StrucDocText() { };
        //    List<StrucDocParagraph> pList = new List<StrucDocParagraph>();
        //    foreach (string p in item)
        //    {
        //        StrucDocParagraph para = new StrucDocParagraph();
        //        para.Text = new string[] { p };
        //        pList.Add(para);
        //    }
        //    Section.text.Items = pList.ToArray();
        //}

        //private void CreateParagraphAndContent(List<string> item)
        //{
        //    Section.text = new StrucDocText() { };
        //    List<StrucDocParagraph> pList = new List<StrucDocParagraph>();
        //    foreach (string p in item)
        //    {
        //        StrucDocParagraph para = new StrucDocParagraph();
        //        para.Items = new object[] { new StrucDocContent() { Text = new string[] { p } } };
        //        pList.Add(para);
        //    }
        //    Section.text.Items = pList.ToArray();
        //}


        /// <summary>
        /// Table head 설정
        /// </summary>
        /// <param name="array"></param>
        //internal static void CreateTableHead(IEnumerable<string> array)
        //{
        //    ThArray = array.Select(s => new StrucDocTh() { Text = new string[] { s } }).ToArray();
        //}

        /// <summary>
        /// Narrative block 초기화 ( Table )
        /// </summary>
        //internal static void SetTextTable()
        //{
        //    DocumentText = new StrucDocText();
        //    Tables = new List<StrucDocTable>();
        //    Table = new StrucDocTable();
        //    Thead = new StrucDocThead();
        //    TheadTrList = new List<StrucDocTr>();
        //    TbodyList = new List<StrucDocTbody>();
        //    Tbody = new StrucDocTbody();
        //    Tr = new StrucDocTr();

        //    Tr.Items = ThArray;
        //    TheadTrList.Add(Tr);
        //    Thead.tr = TheadTrList.ToArray();

        //    TbodyList.Add(Tbody);
        //    Table.thead = Thead;
        //    Table.tbody = TbodyList.ToArray();
        //    Tables.Add(Table);
        //    DocumentText.Items = Tables.ToArray();
        //    Section.text = DocumentText;
        //}

        //internal void SetTextTable(IEnumerable<string> array)
        //{
        //    CreateTableHead(array);
        //    Table = new StrucDocTable();
        //    Thead = new StrucDocThead();
        //    TheadTrList = new List<StrucDocTr>();
        //    TbodyList = new List<StrucDocTbody>();
        //    Tbody = new StrucDocTbody();
        //    Tr = new StrucDocTr();
        //    Tr.Items = ThArray;
        //    TheadTrList.Add(Tr);
        //    Thead.tr = TheadTrList.ToArray();
        //    TbodyList.Add(Tbody);
        //    Table.thead = Thead;
        //    Table.tbody = TbodyList.ToArray();
        //    TrList = new List<StrucDocTr>();
        //}

        /// <summary>
        /// 날짜 포맷 변경
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //internal string ConvertToDateFormat(string value)
        //{
        //    System.Globalization.CultureInfo info = System.Globalization.CultureInfo.CurrentCulture;
        //    DateTime temp = new DateTime();

        //    if (DateTime.TryParseExact(value, "yyyyMMdd", info, System.Globalization.DateTimeStyles.None, out temp))
        //    {
        //        string rtn = temp.ToString("yyyy-MM-dd");
        //        return rtn;
        //    }
        //    else
        //        return null;

        //}
        #endregion

        #region:  Section Part ( AS - IS )
        #region :  Problems (진단내역)
        //internal POCD_MT000040Component3 CreateProblems(List<ProblemObject> items, string type)
        //{
        //    if (items != null && items.Any())
        //    {
        //        Component3 = new POCD_MT000040Component3();
        //        Section = new POCD_MT000040Section();
        //        Section.id = GetII();
        //        Section.templateId = GetTemplateId(SectionOID.Problems_Entry, "2015-08-01");
        //        Section.code = GetCE(LOINC.Problems, LoincDisplayName.Problems, CodeSystemName.LOINC, OID.LOINC);
        //        Section.title = GetST("진단내역");

        //        CreateTableHead(Narrative[SectionType.Problems_Clinic]);
        //        SetTextTable();
        //        items.ForEach(d => d.TableBodyArray = new string[] { ConvertToDateFormat(d.StartDate), d.ProblemName, d.ProblemCode });
        //        Tbody.tr = GetTbody<ProblemObject>(items);
        //        Component3.section = Section;
        //        if (items.Any(d => !string.IsNullOrEmpty(d.ProblemCode)))
        //        {
        //            Component3.section.entry = entryLogic.SetProblemConcernEnrty(items);
        //        }
        //        return Component3;
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //}
        #endregion
        #region :  Medications (투약내역)
        //internal POCD_MT000040Component3 CreateMedications(List<MedicationObject> items, string type)
        //{
        //    if (items != null && items.Any())
        //    {
        //        Component3 = new POCD_MT000040Component3();
        //        Section = new POCD_MT000040Section();
        //        Section.id = GetII();
        //        Section.templateId = GetTemplateId(SectionOID.Medications, "2014-06-09");
        //        Section.code = GetCE(LOINC.Medications, LoincDisplayName.Medications, CodeSystemName.LOINC, OID.LOINC);
        //        Section.title = GetST("투약내역");

        //        CreateTableHead(Narrative[SectionType.Medications_Clinic]);
        //        SetTextTable();
        //        // "투여일자", "약품명","주성분명" "복용량", "횟수", "일수", "용법"
        //        //items.ForEach(d => d.TableBodyArray = new string[] { ConvertToDateFormat(d.StartDate), d.MedicationName, d.DoseQuantity + " " + d.DoseQuantityUnit, d.RepeatNumber, d.Period, d.Usage });
        //        string[] thWidth = new string[] { "12%", "40%", "10%", "8", "8", "23" };
        //        Table.Items = new object[] 
        //        {
        //            new StrucDocColgroup() { col = thWidth.Select(s => new StrucDocCol(){ width = s }).ToArray() }
        //        };
        //        #region
        //        //switch (type)
        //        //{
        //        //    case DocumentType.REFERRAL_NOTE:                        
        //        //        break;
        //        //    case DocumentType.CONSULTATION_NOTE:
        //        //    case DocumentType.TRANSFER_NOTE:                        
        //        //        //Medications
        //        //        //{ "투여일자(최초)", "투약명", "복용량+단위", "횟수", "일수", "용법", "경로" });
        //        //        //"투여일자(최초)", "투여일자(최총)", "진행상태", "투약코드(EDI)", "투약명", "복용량", "복용단위", "횟수", "투여기간(일)", "투약코드 주성분", "용법", "경로" });
        //        //        //CreateTableHead(Narrative[SectionType.Medications]);
        //        //        //SetTextTable();
        //        //        //items.ForEach(d => d.TableBodyArray = new string[7] { ConvertToDateFormat(d.StartDate), d.MedicationName, d.DoseQuantity+" "+d.DoseQuantityUnit, d.RepeatNumber, d.Period,  d.Usage, d.Route });
        //        //        break;
        //        //    case DocumentType.CRS_AMBULATORY:
        //        //    case DocumentType.CRS_INPATIENT:                    
        //        //        // "투여일자(최초)", "투여일자(최종)", "투약코드(EDI)", "투약명", "용량", "복용단위", "횟수", "투여기간", "투약코드 주성분(ATC)", "주성분명", "용법" });
        //        //        //CreateTableHead(Narrative[SectionType.Medications_CRS]);
        //        //        //SetTextTable();
        //        //        //items.ForEach(d => d.TableBodyArray = new string[11] { ConvertToDateFormat(d.StartDate), d.EndDate, d.MedicationCode, d.MedicationName, d.DoseQuantity, d.DoseQuantityUnit, d.RepeatNumber, d.Period, d.MajorComponentCode, d.MajorComponent, d.Usage });
        //        //        break;
        //        //    case DocumentType.CRS_PHR:
        //        //        //{ "처방일", "약품 코드(EDI)","약품 코드명(EDI)","약품 코드(KD)","약품 코드명(KD)","약품 주성분 코드","약품 주성분명","용량", "일수", "용법" });
        //        //        //CreateTableHead(Narrative[SectionType.Medications_PHR]);
        //        //        //SetTextTable();
        //        //        //items.ForEach(d => d.TableBodyArray = new string[8] { ConvertToDateFormat(d.StartDate), d.MedicationCode, d.MedicationName, d.MajorComponentCode, d.MajorComponent, d.DoseQuantity, d.Period, d.Usage });
        //        //        break;
        //        //    default:
        //        //        break;
        //        //}
        //        #endregion
        //        Tbody.tr = GetTbody<MedicationObject>(items);
        //        Component3.section = Section;
        //        Component3.section.entry = entryLogic.SetMedicationActivity(items);
        //        return Component3;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        #endregion
        #region :  Procedures(수술내역)
        //internal POCD_MT000040Component3 CreateProcedure(List<ProcedureObject> items, string type)
        //{
        //    if (items != null && items.Count() > 0)
        //    {
        //        Component3 = new POCD_MT000040Component3();
        //        Section = new POCD_MT000040Section();
        //        Section.id = GetII();
        //        Section.templateId = GetTemplateId(SectionOID.Procedures, "2014-06-09");
        //        Section.code = GetCE(LOINC.Procedures, LoincDisplayName.Procedures, "LOINC", OID.LOINC);
        //        Section.title = GetST("수술내역");
        //        CreateTableHead(Narrative[SectionType.Procedures_Clinic]);
        //        SetTextTable();
        //        // "수술일자", "수술명", "수술 진단명", "마취종류"
        //        items.ForEach(d => d.TableBodyArray = new string[] { ConvertToDateFormat(d.StartDate), d.ProcedureName, d.PostDiagnosisName, d.Anesthesia });

        //        Tbody.tr = GetTbody<ProcedureObject>(items);
        //        Component3.section = Section;
        //        if (items.Any(w => !string.IsNullOrEmpty(w.ProcedureCode)))
        //        {
        //            Section.entry = entryLogic.SetProcedureActivityProcedure(items);
        //        }
        //        return Component3;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        #endregion
        #region :  Laboratory Test(검사내역)
        //internal POCD_MT000040Component3 CreateLaboratoryTest(List<LaboratoryTestObject> items, string type)
        //{
        //    if (items != null && items.Count() > 0)
        //    {
        //        Component3 = new POCD_MT000040Component3();
        //        Section = new POCD_MT000040Section();
        //        Section.id = GetII();
        //        Section.templateId = GetTemplateId(SectionOID.Results, "2015-08-01");
        //        Section.code = GetCE(LOINC.Results, LoincDisplayName.Results, CodeSystemName.LOINC, OID.LOINC);
        //        Section.title = GetST("검사내역");

        //        List<StrucDocItem> docitemList = new List<StrucDocItem>();
        //        StrucDocItem DocItem1 = new StrucDocItem();
        //        DocItem1.Text = new string[] { "검체검사" };
        //        StrucDocItem DocItem2 = new StrucDocItem();
        //        DocItem2.Text = new string[] { "영상검사" };
        //        StrucDocItem DocItem3 = new StrucDocItem();
        //        DocItem3.Text = new string[] { "병리검사" };
        //        StrucDocItem DocItem4 = new StrucDocItem();
        //        DocItem4.Text = new string[] { "기능검사" };
        //        StrucDocItem DocItem5 = new StrucDocItem();
        //        DocItem4.Text = new string[] { "1차 의원 검사내역" };
        //        switch (type)
        //        {
        //            #region :  1차 의원 적용부
        //            case "1.2.410.100110.40.2.1.1":
        //                CreateTableHead(Narrative[SectionType.Result_Clinic]);
        //                SetTextTable();
        //                //items.ForEach(d => d.TableBodyArray = new string[5] { ConvertToDateFormat(d.StartDate), d.EntryName, d.TestName, d.ResultValue, d.Reference });
        //                items.ForEach(d => d.TableBodyArray = new string[] { "", "", "", "", "" });
        //                Tbody.tr = GetTbody<LaboratoryTestObject>(items);
        //                Component3.section = Section;
        //                return Component3;
        //            #endregion
        //            #region :  3차 의원 적용부
        //            case "1.2.410.100110.40.2.1.3":
        //            case "1.2.410.100110.40.2.1.2":
        //                foreach (IGrouping<LaboratoryType, LaboratoryTestObject> group in items.GroupBy(w => w.LabType))
        //                {
        //                    switch (group.Key)
        //                    {
        //                        case LaboratoryType.Specimen:// 검체                                    
        //                            // { "검사처방일", "검사수행일", "결과보고일", "검사명", "검사항목", "검사결과", "참고치"  });
        //                            DocItem1 = new StrucDocItem();
        //                            DocItem1.Text = new string[] { "검체검사" + " ( " + group.Count() + " 건 )" };
        //                            SetTextTable(Narrative[SectionType.Result_Specimen]);
        //                            //group.ToList().ForEach(d => d.TableBodyArray = new string[7] { ConvertToDateFormat(d.OrderDate), ConvertToDateFormat(d.StartDate), ConvertToDateFormat(d.ResultDate), d.TestName, d.EntryName, d.ResultValue, d.Reference });
        //                            group.ToList().ForEach(d => d.TableBodyArray = new string[] { "", "", "", "", "", "", "" });
        //                            Tbody.tr = GetTbody<LaboratoryTestObject>(group);
        //                            //간격 추가
        //                            Table.Items = new object[]
        //                            {
        //                                new StrucDocColgroup() 
        //                                {
        //                                    col = new StrucDocCol[]
        //                                    {
        //                                        new StrucDocCol(){ width = "15%" },
        //                                        new StrucDocCol(){ width = "15%" },
        //                                        new StrucDocCol(){ width = "15%" },
        //                                        new StrucDocCol(){ width = "15%" },
        //                                        new StrucDocCol(){ width = "15%" },
        //                                        new StrucDocCol(){ width = "15%" },
        //                                        new StrucDocCol(){ width = "10%" }
        //                                    }
        //                                }
        //                            };

        //                            DocItem1.Items = new object[] { Table, string.Empty };
        //                            docitemList.Add(DocItem1);
        //                            break;
        //                        case LaboratoryType.Radiology:// 영상검사                                    
        //                            // { "검사일자", "검사명", "검사결과" });
        //                            DocItem2 = new StrucDocItem();
        //                            DocItem2.Text = new string[] { "영상검사" + " ( " + group.Count() + " 건 )" };
        //                            // { "검사처방일", "검사수행일", "결과보고일", "검사명", "검사결과" });
        //                            SetTextTable(Narrative[SectionType.Result]);
        //                            //group.ToList().ForEach(d => d.TableBodyArray = new string[5] { ConvertToDateFormat(d.OrderDate), ConvertToDateFormat(d.StartDate), ConvertToDateFormat(d.ResultDate), d.TestName, d.ResultValue });
        //                            group.ToList().ForEach(d => d.TableBodyArray = new string[] { "", "", "", "", "" });
        //                            Tbody.tr = GetTbody<LaboratoryTestObject>(group);
        //                            Table.Items = new object[]
        //                            {
        //                                new StrucDocColgroup() 
        //                                {
        //                                    col = new StrucDocCol[]
        //                                    {
        //                                        new StrucDocCol(){ width = "15%" },
        //                                        new StrucDocCol(){ width = "15%" },
        //                                        new StrucDocCol(){ width = "15%" },
        //                                        new StrucDocCol(){ width = "15%" },
        //                                        new StrucDocCol(){ width = "40%" },                                                
        //                                    }
        //                                }
        //                            };
        //                            DocItem2.Items = new object[] { Table, string.Empty };
        //                            docitemList.Add(DocItem2);
        //                            break;
        //                        case LaboratoryType.Pathology:// 병리검사                                    
        //                            // { "검사일자", "검사명", "검사결과" });
        //                            DocItem3 = new StrucDocItem();
        //                            DocItem3.Text = new string[] { "병리검사" + " ( " + group.Count() + " 건 )" };
        //                            SetTextTable(Narrative[SectionType.Result]);
        //                            //group.ToList().ForEach(d => d.TableBodyArray = new string[5] { ConvertToDateFormat(d.OrderDate), ConvertToDateFormat(d.StartDate), ConvertToDateFormat(d.ResultDate), d.TestName, d.ResultValue });
        //                            group.ToList().ForEach(d => d.TableBodyArray = new string[] { "", "", "", "", "" });
        //                            Tbody.tr = GetTbody<LaboratoryTestObject>(group);
        //                            Table.Items = new object[]
        //                            {
        //                                new StrucDocColgroup() 
        //                                {
        //                                    col = new StrucDocCol[]
        //                                    {
        //                                        new StrucDocCol(){ width = "15%" },
        //                                        new StrucDocCol(){ width = "15%" },
        //                                        new StrucDocCol(){ width = "15%" },
        //                                        new StrucDocCol(){ width = "15%" },
        //                                        new StrucDocCol(){ width = "40%" },                                                
        //                                    }
        //                                }
        //                            };
        //                            DocItem3.Items = new object[] { Table, string.Empty };
        //                            docitemList.Add(DocItem3);
        //                            break;
        //                        case LaboratoryType.Functional:// 기능검사                                    
        //                            // { "검사일자", "검사명", "검사결과" });
        //                            DocItem4 = new StrucDocItem();
        //                            DocItem4.Text = new string[] { "기능검사" + " ( " + group.Count() + " 건 )" };
        //                            SetTextTable(Narrative[SectionType.Result]);
        //                            //group.ToList().ForEach(d => d.TableBodyArray = new string[5] { ConvertToDateFormat(d.OrderDate), ConvertToDateFormat(d.StartDate), ConvertToDateFormat(d.ResultDate), d.TestName, d.ResultValue });
        //                            group.ToList().ForEach(d => d.TableBodyArray = new string[] { "", "", "", "", "" });
        //                            Tbody.tr = GetTbody<LaboratoryTestObject>(group);
        //                            Table.Items = new object[]
        //                            {
        //                                new StrucDocColgroup() 
        //                                {
        //                                    col = new StrucDocCol[]
        //                                    {
        //                                        new StrucDocCol(){ width = "15%" },
        //                                        new StrucDocCol(){ width = "15%" },
        //                                        new StrucDocCol(){ width = "15%" },
        //                                        new StrucDocCol(){ width = "15%" },
        //                                        new StrucDocCol(){ width = "40%" },                                                
        //                                    }
        //                                }
        //                            };
        //                            DocItem4.Items = new object[] { Table, string.Empty };
        //                            docitemList.Add(DocItem4);
        //                            break;
        //                        case LaboratoryType.None:
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                }

        //                DocList = new StrucDocList() { item = docitemList.ToArray() };
        //                Section.text = new StrucDocText() { Items = new object[] { DocList } };
        //                Component3.section = Section;
        //                return Component3;
        //            #endregion
        //            #region :  CRS
        //            case "1.2.410.100110.40.2.1.4":
        //                foreach (IGrouping<LaboratoryType, LaboratoryTestObject> group in items.GroupBy(w => w.LabType))
        //                {
        //                    switch (group.Key)
        //                    {
        //                        case LaboratoryType.Specimen:// 검체                                    
        //                            // { "검사일자", "검사코드", "검사명", "검사코드", "검사결과" });
        //                            DocItem1 = new StrucDocItem();
        //                            DocItem1.Text = new string[] { "검체검사" };
        //                            //if (type == DocumentType.CRS_PHR)
        //                            //{
        //                            //    SetTextTable(new List<string>() { "검사일자", "검사명", "검사결과", "참고치" });
        //                            //    group.ToList().ForEach(d => d.TableBodyArray = new string[4] { ConvertToDateFormat(d.StartDate), d.TestName, d.ResultValue, !string.IsNullOrEmpty(d.ReferenceLowValue) ? d.ReferenceLowValue + " ~ " + d.ReferenceHighValue + " " + d.ReferenceUnit : null });
        //                            //}
        //                            //else
        //                            {
        //                                SetTextTable(Narrative[SectionType.Result_Specimen_CRS]);
        //                                //group.ToList().ForEach(d => d.TableBodyArray = new string[5] { ConvertToDateFormat(d.Date), d.TestCode, d.TestName, d.ResultValue, d.Reference });
        //                            }
        //                            Tbody.tr = GetTbody<LaboratoryTestObject>(group);
        //                            DocItem1.Items = new object[] { Table, string.Empty };
        //                            docitemList.Add(DocItem1);
        //                            break;
        //                        case LaboratoryType.Radiology:// 영상검사                                    
        //                            // { "검사일자", "검사코드", "검사명", "검사결과" });
        //                            DocItem2 = new StrucDocItem();
        //                            DocItem2.Text = new string[] { "영상검사" };
        //                            //if (type == DocumentType.CRS_PHR)
        //                            //{
        //                            //    SetTextTable(new List<string>() { "검사시행일자", "검사명", "검사코드" });
        //                            //    group.ToList().ForEach(d => d.TableBodyArray = new string[3] { ConvertToDateFormat(d.StartDate), d.TestName, d.TestCode });
        //                            //}
        //                            //else
        //                            {
        //                                SetTextTable(Narrative[SectionType.Result_CRS]);
        //                                //group.ToList().ForEach(d => d.TableBodyArray = new string[3] { ConvertToDateFormat(d.Date), d.TestCode, d.TestName });
        //                            }
        //                            Tbody.tr = GetTbody<LaboratoryTestObject>(group);
        //                            DocItem2.Items = new object[] { Table, string.Empty };
        //                            docitemList.Add(DocItem2);
        //                            break;
        //                        case LaboratoryType.Pathology:// 병리검사                                    
        //                            // { "검사일자", "검사명", "검사결과" });
        //                            DocItem3 = new StrucDocItem();
        //                            DocItem3.Text = new string[] { "병리검사" };
        //                            //if (type == DocumentType.CRS_PHR)
        //                            //{
        //                            //    SetTextTable(new List<string>() { "검사시행일자", "검사명", "검사코드" });
        //                            //    group.ToList().ForEach(d => d.TableBodyArray = new string[3] { ConvertToDateFormat(d.StartDate), d.TestName, d.TestCode });
        //                            //}
        //                            //else
        //                            {
        //                                SetTextTable(Narrative[SectionType.Result_CRS]);
        //                                //group.ToList().ForEach(d => d.TableBodyArray = new string[3] { ConvertToDateFormat(d.Date), d.TestCode, d.TestName });
        //                            }
        //                            Tbody.tr = GetTbody<LaboratoryTestObject>(group);
        //                            DocItem3.Items = new object[] { Table, string.Empty };
        //                            docitemList.Add(DocItem3);
        //                            break;
        //                        case LaboratoryType.Functional:// 기능검사                                    
        //                            // { "검사일자", "검사명", "검사결과" });
        //                            DocItem4 = new StrucDocItem();
        //                            DocItem4.Text = new string[] { "기능검사" };
        //                            //if (type == DocumentType.CRS_PHR)
        //                            //{
        //                            //    SetTextTable(new List<string>() { "검사시행일자", "검사명", "검사코드" });
        //                            //    group.ToList().ForEach(d => d.TableBodyArray = new string[3] { ConvertToDateFormat(d.OrderDate), d.TestName, d.TestCode });
        //                            //}
        //                            //else
        //                            {
        //                                SetTextTable(Narrative[SectionType.Result_CRS]);
        //                                //group.ToList().ForEach(d => d.TableBodyArray = new string[3] { ConvertToDateFormat(d.Date), d.TestCode, d.TestName });
        //                            }
        //                            Tbody.tr = GetTbody<LaboratoryTestObject>(group);
        //                            DocItem4.Items = new object[] { Table, string.Empty };
        //                            docitemList.Add(DocItem4);
        //                            break;
        //                        case LaboratoryType.None:
        //                            break;
        //                        //case LaboratoryType.Clinic:
        //                        //    break;
        //                        default:
        //                            break;
        //                    }
        //                }
        //                DocList = new StrucDocList() { item = docitemList.ToArray() };
        //                Section.text = new StrucDocText() { Items = new object[] { DocList } };
        //                Component3.section = Section;
        //                return Component3;
        //            #endregion
        //            default:
        //                return null;
        //        }
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        #endregion
        #region :  Plan of Treatment (치료계획 / 예약관련 내역)
        //internal POCD_MT000040Component3 CreatePlanOfTreatment(PlanOfTreatmentObject item, string type)
        //{
        //    if (item != null)
        //    {
        //        Component3 = new POCD_MT000040Component3();
        //        Section = new POCD_MT000040Section();
        //        Section.id = GetII();
        //        Section.templateId = GetTemplateId(SectionOID.Plan_Of_Care, "2015-08-01");
        //        Section.code = GetCE(LOINC.Plan_of_care, LoincDisplayName.Plan_of_care, CodeSystemName.LOINC, OID.LOINC);
        //        Section.title = type == "1.2.410.100110.40.2.1.3" ? GetST("치료계획") : GetST("예약관련 정보");

        //        switch (type)
        //        {
        //            case "1.2.410.100110.40.2.1.4":
        //                CreateTableHead(Narrative[SectionType.Plan_of_treatment_CRS]);
        //                item.TableBodyArray = new string[5] { ConvertToDateFormat(item.PlannedDate), item.Text, string.Empty, string.Empty, string.Empty };
        //                break;
        //            case "1.2.410.100110.40.2.1.1":
        //            case "1.2.410.100110.40.2.1.2":
        //                CreateTableHead(Narrative[SectionType.Plan_of_treatment]);
        //                item.TableBodyArray = new string[2] { ConvertToDateFormat(item.PlannedDate), item.Text };
        //                break;
        //            case "1.2.410.100110.40.2.1.3":
        //                CreateTableHead(Narrative[SectionType.Plan_of_treatment_Consultation_note]);
        //                //item.TableBodyArray = new string[2] { item.CareProgress, item.PlannedCare };
        //                item.TableBodyArray = new string[2] { "치료경과", "향후 치료방침" };
        //                break;
        //            default:
        //                return null;
        //        }
        //        SetTextTable();
        //        Tbody.tr = GetTbody<PlanOfTreatmentObject>(item);
        //        Component3.section = Section;
        //        return Component3;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        #endregion
        #region :  Assessment (소견 및 주의사항)
        internal POCD_MT000040Component3 CreateAssessment(AssessmentObject item)
        {
            if (item != null)
            {
                Component3 = new POCD_MT000040Component3();
                Section = new POCD_MT000040Section();
                Section.id = GetII();
                Section.templateId = GetTemplateId("2.16.840.1.113883.10.20.22.2.8");
                Section.code = GetCE("51848-0", "ASSESSMENTS", CodeSystemName.LOINC, OID.LOINC);
                //Section.title = GetST("History of present illness");
                Section.title = GetST("소견 및 주의사항");

                List<StrucDocItem> docitemList = new List<StrucDocItem>();
                docitemList.Add(new StrucDocItem() { Text = new string[] { "이학적 검사결과 : " + item.PhysicalScienceLab } });
                docitemList.Add(new StrucDocItem() { Text = new string[] { "소견 및 주의사항 : " + item.Assessment } });


                StrucDocList strucDocList = new StrucDocList() { item = docitemList.ToArray() };
                Section.text = new StrucDocText() { Items = new object[] { strucDocList } };

                Component3.section = Section;
                //Link
                return Component3;
            }
            else
            {
                return null;
            }
        }
        #endregion
        #region :  Reason For Referral (의뢰사유 / 회송사유)
        //internal POCD_MT000040Component3 CreateReasonForReferral(string param, string type)
        //{
        //    if (!string.IsNullOrEmpty(param))
        //    {
        //        Component3 = new POCD_MT000040Component3();
        //        Section = new POCD_MT000040Section();
        //        Section.id = GetII();
        //        //Section.templateId = GetTemplateId("1.3.6.1.4.1.19376.1.5.3.1.3.1", "2014-06-09");
        //        Section.templateId = GetTemplateId(SectionOID.Reason_For_Referral, "2014-06-09");
        //        //Section.code = GetCE("42349-1", "Reason for Referral", CodeSystemName.LOINC, OID.LOINC);
        //        Section.code = GetCE(LOINC.Reason_for_Referral, LoincDisplayName.Reason_for_Referral, CodeSystemName.LOINC, OID.LOINC);
        //        //Section.title = GetST("Reason for Referral");
        //        switch (type)
        //        {
        //            case "1.2.410.100110.40.2.1.3":
        //                Section.title = GetST("회신사유");
        //                CreateParagraph(new List<string>() { param });
        //                break;
        //            case "1.2.410.100110.40.2.1.2":
        //                Section.title = GetST("회송사유");
        //                CreateParagraph(new List<string>() { param });
        //                break;
        //            case "1.2.410.100110.40.2.1.1":
        //                Section.title = GetST("의뢰사유");
        //                CreateParagraph(new List<string>() { param });
        //                break;
        //            default:
        //                break;
        //        }

        //        //CreateParagraph(new List<string>() { item.ReasonForReferral });
        //        Component3.section = Section;
        //        return Component3;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        #endregion
        #region :  History of Past Illness ( 과거병력 )
        //internal POCD_MT000040Component3 CreateHistoryOfPresentIllness(string param)
        //{
        //    if (!string.IsNullOrEmpty(param))
        //    {
        //        Component3 = new POCD_MT000040Component3();
        //        Section = new POCD_MT000040Section();
        //        Section.id = GetII();
        //        Section.templateId = GetTemplateId(SectionOID.History_Of_Present_Illness);
        //        Section.code = GetCE(LOINC.History_Of_Present_Illness, LoincDisplayName.History_Of_Present_Illness, CodeSystemName.LOINC, OID.LOINC);
        //        Section.title = GetST("병력");
        //        //Section.title = GetST("소견 및 주의사항");
        //        CreateParagraph(new List<string>() { param });
        //        Component3.section = Section;
        //        return Component3;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        #endregion
        #region :  Interventions (교육정보)
        //internal POCD_MT000040Component3 CreateInterventions(List<EducationCheckObject> items)
        //{
        //    if (items != null && items.Count() > 0)
        //    {
        //        Component3 = new POCD_MT000040Component3();
        //        Section = new POCD_MT000040Section();
        //        Section.id = GetII();
        //        Section.templateId = GetTemplateId(SectionOID.Interventions);
        //        Section.code = GetCE(LOINC.Interventions, LoincDisplayName.Interventions, CodeSystemName.LOINC, OID.LOINC);
        //        Section.title = GetST("환자교육내역");

        //        CreateTableHead(Narrative[SectionType.Interventions]);
        //        SetTextTable();
        //        // { "교육일자", "교육명", "교육종류", "교육대상자", "의견 및 제언" });
        //        items.ForEach(d => d.TableBodyArray = new string[] { ConvertToDateFormat(d.Date), d.EducationName, d.EducationSubject.GetDescription(), d.EducationType.GetDescription(), d.Text });
        //        Tbody.tr = GetTbody<EducationCheckObject>(items);
        //        Component3.section = Section;

        //        return Component3;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        #endregion
        #region :  Allergies
        //internal POCD_MT000040Component3 CreateAllergies(List<AllergyObject> items, EntryOption option)
        //{
        //    if (items != null && items.Count() > 0)
        //    {
        //        Component3 = new POCD_MT000040Component3();
        //        Section = new POCD_MT000040Section();
        //        Section.id = GetII();
        //        switch (option)
        //        {
        //            case EntryOption.REQUIRED:
        //                Section.templateId = GetTemplateId(SectionOID.Allergies, "2015-08-01");
        //                //Section.templateId = GetTemplateId(SectionOID.Allergies, "2014-06-09");
        //                break;

        //            case EntryOption.OPTIONAL:
        //                Section.templateId = GetTemplateId(SectionOID.Allergies_NoEntry, "2015-08-01");
        //                //Section.templateId = GetTemplateId(SectionOID.Allergies_NoEntry, "2014-06-09");
        //                break;
        //            default:
        //                break;
        //        }
        //        Section.code = GetCE(LOINC.Allergies, LoincDisplayName.Allergies, "LOINC", OID.LOINC);
        //        Section.title = GetST("알러지 정보");
        //        //{ "등록일자", "알러지명", "반응", "알러지 비고" });
        //        CreateTableHead(Narrative[SectionType.Allergies]);
        //        items.ForEach(d => d.TableBodyArray = new string[] { ConvertToDateFormat(d.StartDate), d.Allergy, d.Reaction });
        //        SetTextTable();
        //        //Tbody.tr = GetTbody(allergy);
        //        Tbody.tr = GetTbody<AllergyObject>(items);
        //        Component3.section = Section;
        //        return Component3;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        #endregion
        #region :  Immunization
        //internal POCD_MT000040Component3 CreateImmunization(List<ImmunizationObject> items)
        //{
        //    if (items != null && items.Count() > 0)
        //    {
        //        Component3 = new POCD_MT000040Component3();
        //        Section = new POCD_MT000040Section();
        //        Section.id = GetII();
        //        Section.templateId = GetTemplateId(SectionOID.Immunizations, "2015-08-01");
        //        Section.code = GetCE(LOINC.Immunizations, LoincDisplayName.Immunizations, "LOINC", OID.LOINC);
        //        Section.title = GetST("예방접종내역");
        //        // { "접종일자", "접종코드", "백신종류", "백신명", "접종차수" });
        //        CreateTableHead(Narrative[SectionType.Immunizations]);
        //        items.ForEach(d => d.TableBodyArray = new string[] { ConvertToDateFormat(d.StartDate), d.ImmunizationName, string.Empty, d.VaccineName, d.RepeatNumber });
        //        SetTextTable();
        //        //Tbody.tr = GetTbody(allergy);
        //        Tbody.tr = GetTbody<ImmunizationObject>(items);
        //        Component3.section = Section;
        //        Component3.section.entry = entryLogic.SetImmunizationActivity(items);

        //        return Component3;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        #endregion
        #region :  Vital Signs
        /// <summary>
        /// Create Vital Signs
        /// Entry Required
        /// </summary>
        /// <param name="items">VitalSignsObject</param>
        /// <returns>POCD_MT000040Section</returns>
        //internal POCD_MT000040Component3 CreateVitalSigns(List<VitalSignsObject> items)
        //{
        //    if (items != null && items.Count() > 0)
        //    {
        //        Component3 = new POCD_MT000040Component3();
        //        Section = new POCD_MT000040Section();
        //        Section.id = GetII();
        //        Section.templateId = GetTemplateId(SectionOID.Vital_Signs, "2015-08-01");
        //        //Section.templateId = GetTemplateId(SectionOID.Vital_Signs_Entry, "2014-06-09");
        //        Section.code = GetCE(LOINC.Vital_Signs, LoincDisplayName.Vital_Signs, CodeSystemName.LOINC, OID.LOINC);
        //        Section.title = GetST("생체신호 및 상태");
        //        //{ "측정일자", "키", "몸무게",  "혈압(확장기)", "혈압(수축기)", "체온" });
        //        CreateTableHead(Narrative[SectionType.Vital_signs]);
        //        items.ForEach(d => d.TableBodyArray = new string[] { ConvertToDateFormat(d.Date), d.Height, d.Weight, d.BP_Diastolic + " mmHg", d.BP_Systolic + " mmHg", d.BodyTemperature });
        //        SetTextTable();
        //        //Tbody.tr = GetTbody(vital);
        //        Tbody.tr = GetTbody<VitalSignsObject>(items);
        //        Component3.section = Section;
        //        Component3.section.entry = entryLogic.SetVitalSignOrganizer(items);
        //        return Component3;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        #endregion
        #region :  Social History (Smoking Status)
        //internal POCD_MT000040Component3 CreateSocialHistory(CDAObject item)
        //{
        //    if (item != null)
        //    {
        //        Component3 = new POCD_MT000040Component3();
        //        Section = new POCD_MT000040Section();
        //        Section.id = GetII();
        //        Section.templateId = GetTemplateId(SectionOID.Social_History, "2015-08-01");
        //        Section.code = GetCE(LOINC.Social_History, LoincDisplayName.Social_History, "LOINC", OID.LOINC);
        //        Section.title = GetST("흡연상태 / 음주상태");

        //        item.TableBodyArray = new string[]
        //        { 
        //            item.SocialHistory.SmokingStatusCode,
        //            item.SocialHistory.SmokingStatus,
        //            item.SocialHistory.Frequency,
        //            item.SocialHistory.AlcoholConsumption,
        //            item.SocialHistory.Overdrinking
        //        };
        //        CreateTableHead(new List<string>() { "흡연 상태코드", "흡연 상태명", "음주상태(음주빈도)", "음주 상태(1일 음주량)", "음주 상태(과음빈도)" });
        //        SetTextTable();
        //        Tbody.tr = GetTbody<SmokingStatusObject>(item);
        //        Component3.section = Section;
        //        Component3.section.entry = entryLogic.SetSocialHistory(item);
        //        return Component3;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        #endregion
        #region :  Reason for visit
        //internal POCD_MT000040Component3 CreateReasonForVisit(DescriptionObject item)
        //{
        //    if (item != null && !string.IsNullOrEmpty(item.ReasonForVisit))
        //    {
        //        Component3 = new POCD_MT000040Component3();
        //        Section = new POCD_MT000040Section();
        //        Section.id = GetII();
        //        Section.templateId = GetTemplateId(SectionOID.Reason_For_Visit);
        //        Section.code = GetCE(LOINC.Reason_For_Visit, null, CodeSystemName.LOINC, OID.LOINC);
        //        //Section.title = GetST("Reason for Referral");
        //        Section.title = GetST("방문사유");
        //        CreateParagraph(new List<string>() { item.ReasonForVisit });

        //        Component3.section = Section;
        //        return Component3;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        #endregion
        #region :  Chief complaint (Reason For Hospitalization)
        //internal POCD_MT000040Component3 CreateCheifComplaint(DescriptionObject item)
        //{
        //    if (item != null && !string.IsNullOrEmpty(item.ReasonForHospitalization))
        //    {
        //        Component3 = new POCD_MT000040Component3();
        //        Section = new POCD_MT000040Section();
        //        Section.id = GetII();
        //        Section.templateId = GetTemplateId(SectionOID.Cheif_Complaint);
        //        Section.code = GetCE(LOINC.Cheif_Complaint, null, CodeSystemName.LOINC, OID.LOINC);
        //        //Section.title = GetST("Reason for Referral");
        //        Section.title = GetST("입원사유");
        //        CreateParagraph(new List<string>() { item.ReasonForHospitalization });

        //        Component3.section = Section;
        //        return Component3;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        #endregion
        #region :  Hospital discharge Instruction
        //internal POCD_MT000040Component3 CreateHospitalDischargeInstruction(DescriptionObject item)
        //{
        //    if (item != null && !string.IsNullOrEmpty(item.HospitalDiscargeInstruction))
        //    {
        //        Component3 = new POCD_MT000040Component3();
        //        Section = new POCD_MT000040Section();
        //        Section.id = GetII();
        //        Section.templateId = GetTemplateId("2.16.840.1.113883.10.20.22.2.41");
        //        Section.code = GetCE("8653-8", "HOSPITAL DISCHARGE INSTRUCTIONS", CodeSystemName.LOINC, OID.LOINC);
        //        Section.title = GetST("퇴원지시사항");
        //        CreateParagraph(new List<string>() { item.HospitalDiscargeInstruction });
        //        Component3.section = Section;
        //        return Component3;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        #endregion
        #region :  History of infection(법정 감염성 전염병)
        //internal POCD_MT000040Component3 CreateHistoryOfInfection(InfectionObject item)
        //{
        //    if (item != null)
        //    {
        //        Component3 = new POCD_MT000040Component3();
        //        Section = new POCD_MT000040Section();
        //        Section.id = GetII();
        //        Section.templateId = GetTemplateId(SectionOID.History_Of_Infection, "2015-08-01");
        //        //Section.templateId = GetTemplateId(SectionOID.Vital_Signs_Entry, "2014-06-09");
        //        Section.code = GetCE(LOINC.History_Of_Infection, LoincDisplayName.History_Of_Infection, CodeSystemName.LOINC, OID.LOINC);
        //        //Section.title = GetST("법정 감염성 감염병(History of infections)");
        //        Section.title = GetST("법정 전염성 감염병");
        //        CreateTableHead(Narrative[SectionType.Infection]);
        //        //{ "발병일자", "진단일", "감염병명", "신고일", "환자분류", "확진검사 결과", "입원여부", "추정감염지역" });
        //        item.TableBodyArray = new string[]
        //        { 
        //            ConvertToDateFormat(item.OnsetDate),
        //            ConvertToDateFormat(item.DiagnosisDate),
        //            item.InfectionName, 
        //            ConvertToDateFormat(item.ReportedDate),
        //            item.Classification,
        //            item.TestResult,
        //            item.AdmissionYN.ToString(),
        //            item.SuspectedArea
        //        };

        //        SetTextTable();
        //        //Tbody.tr = GetTbody(vital);
        //        Tbody.tr = GetTbody<InfectionObject>(item);
        //        Component3.section = Section;
        //        return Component3;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        #endregion
        #region :  Signature
        //Create Signature Section
        //internal POCD_MT000040Component3 CreateSignatureSection(CDAObject item)
        //{
        //    if (item.Signature != null)
        //    {
        //        Component3 = new POCD_MT000040Component3();
        //        Section = new POCD_MT000040Section();
        //        Section.id = GetII();
        //        Section.templateId = GetTemplateId(SectionOID.SIGNATURES);
        //        Section.code = GetCE(LOINC.Signatures_Section, null, CodeSystemName.LOINC, OID.LOINC);
        //        Section.title = GetST("Signatures");
        //        Section.text = new StrucDocText() { Text = new string[] { "해당 section은 서명 image data를 포함하고 있습니다" } };
        //        try
        //        {
        //            //string imageData = Convert.ToBase64String(item.Signature.ImageData);
        //            Section.entry = entryLogic.SetSignatureImage(item.Signature.ImageData, item.Signature.MediaType);
        //        }
        //        catch (Exception)
        //        {
        //            throw new Exception("서명 image data가 없습니다");
        //        }
        //        Component3.section = Section;
        //        //CreateParagraphAndContent(new List<string>() { imageData });
        //        return Component3;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        #endregion
        #region :  Findings (판독내역)
        //internal POCD_MT000040Component3 CreateFinding(CDAObject obj)
        //{
        //    Component3 = new POCD_MT000040Component3();
        //    Section = new POCD_MT000040Section();
        //    //Section.templateId = GetTemplateId("2.16.840.1.113883.10.20.6.1.2");
        //    Section.templateId = GetTemplateId(SectionOID.Findings);
        //    Section.code = GetCE(DicomCode.Findings, DicomDisplayName.Findings, CodeSystemName.DCM, OID.DCM);
        //    //obj.section.title = GetST("Findings");
        //    Section.title = GetST("판독결과");

        //    List<StrucDocItem> docitemList = new List<StrucDocItem>();
        //    //docitemList.Add(new StrucDocItem() { Text = new string[] { "영상 촬영일 : " + ConvertToDateFormat(obj.ImageInterpretation.Date) } });
        //    //docitemList.Add(new StrucDocItem() { Text = new string[] { "최종 판독일 : " + ConvertToDateFormat(obj.ImageInterpretation.InterpretationDate) } });
        //    docitemList.Add(new StrucDocItem() { Text = new string[] { "판독의 : " + obj.ImageInterpretation.DoctorName } });
        //    docitemList.Add(new StrucDocItem() { Text = new string[] { "이미지 URL : " + obj.ImageInterpretation.ImageURL } });
        //    docitemList.Add(new StrucDocItem() { Text = new string[] { "Study Instance UID : " + obj.ImageInterpretation.StudyUID } });
        //    docitemList.Add(new StrucDocItem() { Text = new string[] { "Series Instance UID : " + obj.ImageInterpretation.SeriesUID } });
        //    docitemList.Add(new StrucDocItem() { Text = new string[] { "SOP Instance UID : " + obj.ImageInterpretation.SopUID } });
        //    docitemList.Add(new StrucDocItem() { Text = new string[] { "판독결과 : " + obj.ImageInterpretation.Interpretation } });
        //    docitemList.Add(new StrucDocItem() { Text = new string[] { "검사코드 : " + obj.ImageInterpretation.TestCode } });
        //    docitemList.Add(new StrucDocItem() { Text = new string[] { "검사명 : " + obj.ImageInterpretation.TestName } });

        //    StrucDocList strucDocList = new StrucDocList() { item = docitemList.ToArray() };
        //    Section.text = new StrucDocText() { Items = new object[] { strucDocList } };

        //    Component3.section = Section;
        //    //Link
        //    return Component3;
        //}
        #endregion
        #region :  Dicom Object Catalog
        //internal POCD_MT000040Component3 CreateDicomObjectCatalog()
        //{
        //    POCD_MT000040Component3 obj = new POCD_MT000040Component3();
        //    obj.section = new POCD_MT000040Section();
        //    obj.section.templateId = GetTemplateId(SectionOID.Dicom_Object_Catalog);
        //    //obj.section.code = GetCE("121181", "DICOM Object Catalog", CodeSystemName.DCM, SectionOID.Dicom_Object_Catalog);
        //    obj.section.code = GetCE(DicomCode.Dicom_Object_Catalog, DicomDisplayName.Dicom_Object_Catalog, CodeSystemName.DCM, OID.DCM);
        //    return obj;
        //}
        #endregion

        #endregion

        #endregion

        #endregion
    }
}
