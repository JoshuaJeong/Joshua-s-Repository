using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using xave.com.generator.cus.CodeModel;
using xave.com.generator.cus.StructureSetModel;


namespace xave.web.generator.helper.Util
{
    internal static class CommonQuery
    {
        static Document Document;
        static Containers structureSetContainer;
        static CodeContainers codeContainer;
        static string ErrorLogDirectory = System.Configuration.ConfigurationManager.AppSettings["ErrorLogDirectory"];
        internal static void Set(Containers _containers, CodeContainers _codeContainer)
        {
            structureSetContainer = _containers;
            codeContainer = _codeContainer;
        }
        internal static void Set(string DocumentType)
        {
            if (structureSetContainer == null || structureSetContainer.DocumentType == null || structureSetContainer.DocumentType.Count() < 1) throw new Exception("Document Set is empty!");
            Document = structureSetContainer.DocumentType.FirstOrDefault(t => t.DocTypeCode == DocumentType);
        }
        internal static TResult GetHeader<TResult>(string[] xPath, HeaderPart headerPart = null, int index = 0)
        {
            if (headerPart == null) return default(TResult);
            object obj = default(TResult);
            try
            {
                var query = headerPart.StructureSet.AsQueryable().DynamicAndQuery(xPath, (t, k) => t.PathSplited != null && t.PathSplited.Count() == xPath.Count() && t.PathSplited.Contains(k));
                if (typeof(TResult) == typeof(string))
                    obj = (query != null) ? query.Select(t => t.Value).ElementAtOrDefault(index) : null;
                else if (typeof(TResult) == typeof(HeaderStructure))
                    obj = (query != null) ? query.ElementAtOrDefault(index) : null;
                else
                    obj = (query != null) ? query.ToList() : null;
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                obj = default(TResult);
            }
            return (TResult)obj;
        }
        internal static TResult GetHeader<TResult>(string xPath, HeaderPart headerPart = null, int index = 0)
        {
            if (headerPart == null) return default(TResult);
            object obj = default(TResult);
            try
            {
                var query = headerPart.StructureSet.Where(t => t.Path == xPath);
                if (typeof(TResult) == typeof(string))
                    obj = (query != null) ? query.Select(t => t.Value).ElementAtOrDefault(index) : null;
                else if (typeof(TResult) == typeof(HeaderStructure))
                    obj = (query != null) ? query.ElementAtOrDefault(index) : null;
                else
                    obj = (query != null) ? query.ToList() : null;
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                obj = default(TResult);
            }
            return (TResult)obj;
        }
        internal static string GetHeaderValue(string[] xPath, HeaderPart headerPart = null, int index = 0)
        {
            if (headerPart == null) return null;
            try
            {
                return headerPart.StructureSet.AsQueryable().DynamicAndQuery(xPath, (t, k) => t.PathSplited.Count() == xPath.Count() && t.PathSplited.Contains(k)).Select(t => t.Value).ElementAtOrDefault(index);
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                return null;
            }
        }

        internal static string GetHeaderValue(string xPath, HeaderPart headerPart = null, int index = 0)
        {
            if (headerPart == null) return null;
            try
            {
                return headerPart.StructureSet.Where(t => t.Path == xPath).Select(t => t.Value).ElementAtOrDefault(index);
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                return null;
            }
        }

        internal static string GetBodyValue(string xPath, SectionPart entry, int index = 0)
        {
            try
            {
                return entry.BodyStructureList.Where(t => t.Path == xPath).Select(t => t.Value).ElementAtOrDefault(index);
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                return string.Empty;
            }
        }

        //internal static string GetBodyValue(string[] xPath, SectionPart entry, int index = 0)
        //{
        //    try
        //    {
        //        return entry.BodyStructureList.AsQueryable().DynamicAndQuery(xPath, (t, k) => t.HashDict.Count() == xPath.Count() && t.HashDict.Contains(k)).Select(t => t.Value).ElementAtOrDefault(index);
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.ExceptionLog(e, ErrorLogDirectory);
        //        return string.Empty;
        //    }
        //}

        internal static List<string> GetBodyValues(string xPath, SectionPart entry, int index = 0)
        {
            try
            {
                return entry.BodyStructureList.Where(t => t.Path == xPath).Select(t => t.Value).ToList();
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                return null;
            }
        }

        //internal static List<string> GetBodyValues(string[] xPath, SectionPart entry, int index = 0)
        //{
        //    try
        //    {
        //        return entry.BodyStructureList.AsQueryable().DynamicAndQuery(xPath, (t, k) => t.HashDict.Count() == xPath.Count() && t.HashDict.Contains(k)).Select(t => t.Value).ToList();
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.ExceptionLog(e, ErrorLogDirectory);
        //        return null;
        //    }
        //}

        internal static BodyStructure GetBodyStructure(string xPath, SectionPart entry, int index = 0)
        {
            try
            {
                return entry.BodyStructureList.Where(t => t.Path == xPath).FirstOrDefault();
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                return null;
            }
        }

        //internal static BodyStructure GetBodyStructure(string[] xPath, SectionPart entry, int index = 0)
        //{
        //    try
        //    {
        //        return entry.BodyStructureList.AsQueryable().DynamicAndQuery(xPath, (t, k) => t.HashDict.Count() == xPath.Count() && t.HashDict.Contains(k)).FirstOrDefault();
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.ExceptionLog(e, ErrorLogDirectory);
        //        return null;
        //    }
        //}

        internal static List<BodyStructure> GetBodyStructures(string xPath, SectionPart entry, int index = 0)
        {
            try
            {
                var query = entry.BodyStructureList.Where(t => t.Path == xPath);
                return query.ToList();
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                return null;
            }
        }

        //internal static List<BodyStructure> GetBodyStructures(string[] xPath, SectionPart entry, int index = 0)
        //{
        //    try
        //    {
        //        var query = entry.BodyStructureList.AsQueryable().DynamicAndQuery(xPath, (t, k) => t.HashDict.Count() == xPath.Count() && t.HashDict.Contains(k));
        //        return query.ToList();
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.ExceptionLog(e, ErrorLogDirectory);
        //        return null;
        //    }
        //}

        //internal static TResult GetBodyPart<TResult>(string[] xPath, SectionPart entry, int index = 0)
        //{
        //    object obj;
        //    try
        //    {
        //        var query = entry.BodyStructureList.AsParallel().AsQueryable().DynamicAndQuery(xPath, (t, k) => t.HashDict.Count() == xPath.Count() && t.HashDict.Contains(k));
        //        obj = query.ToList();
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.ExceptionLog(e, ErrorLogDirectory);
        //        obj = default(TResult);
        //    }
        //    return (TResult)obj;
        //}

        internal static BodyStructure GetBodyPartByGroup(string xPath, SectionPart entry, int GroupIndex = 0)
        {
            try
            {
                return entry.BodyStructureList.Where(t => t.Path == xPath).FirstOrDefault(t => t.Group == GroupIndex);
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                return null;
            }
        }

        //internal static BodyStructure GetBodyPartByGroup(string[] xPath, SectionPart entry, int GroupIndex = 0)
        //{
        //    try
        //    {
        //        return entry.BodyStructureList.AsQueryable().DynamicAndQuery(xPath, (t, k) => t.HashDict.Count() == xPath.Count() && t.HashDict.Contains(k)).FirstOrDefault(t => t.Group == GroupIndex);
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.ExceptionLog(e, ErrorLogDirectory);
        //        return null;
        //    }
        //}

        //internal static TResult GetBodyPartByGroup<TResult>(string[] xPath, SectionPart entry, int GroupIndex = 0)
        //{
        //    object obj;
        //    try
        //    {
        //        var query = entry.BodyStructureList.AsParallel().AsQueryable().DynamicAndQuery(xPath, (t, k) => t.HashDict.Count() == xPath.Count() && t.HashDict.Contains(k));
        //        if (typeof(TResult) == typeof(string))
        //            obj = (query != null) ? query.Select(t => t.Value).ElementAtOrDefault(GroupIndex) : null;
        //        else if (typeof(TResult) == typeof(List<string>))
        //            obj = (query != null) ? query.Select(t => t.Value).ToList() : null;
        //        else if (typeof(TResult) == typeof(BodyStructure))
        //            obj = (query != null) ? query.FirstOrDefault(t => t.Group == GroupIndex) : null;
        //        else
        //            obj = (query != null) ? query.ToList() : null;
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.ExceptionLog(e, ErrorLogDirectory);
        //        obj = default(TResult);
        //    }
        //    return (TResult)obj;
        //}

        internal static TResult GetCode<TResult>(string[] _codes, string _type = "NAME")
        {
            object obj = default(TResult);
            if (string.IsNullOrEmpty(_type)) _type = "NAME";
            //if (_type != "NAME" && _type != "CODE" && _type != "CLASSIFICATION" && _type != "TYPE") return default(TResult);

            try
            {
                var query = codeContainer.CodeType.AsQueryable().DynamicAndQuery(_codes, (t, k) => t.CodeCD.Equals(k));
                if (typeof(TResult) == typeof(string))
                    obj = (query != null) ? query.Select(t => t.ValueOf(_type)).ElementAtOrDefault(0) : null;
                else if (typeof(TResult) == typeof(Code))
                    obj = (query != null) ? query.FirstOrDefault() : null;
                else
                    obj = (query != null) ? query.ToList() : null;
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                obj = default(TResult);
            }
            return (TResult)obj;
        }

        internal static string GetCodeValue(string _codes, string _type = "NAME")
        {
            if (string.IsNullOrEmpty(_type)) _type = "NAME";

            try
            {
                var query = codeContainer.CodeType.Where(t => t.UseYN == "TRUE" && t.CodeCD == _codes);
                return query.Select(t => t.ValueOf(_type)).ElementAtOrDefault(0);
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                return null;
            }
        }

        internal static string GetCodeValue(string[] _codes, string _type = "NAME")
        {
            if (string.IsNullOrEmpty(_type)) _type = "NAME";

            try
            {
                var query = codeContainer.CodeType.AsQueryable().DynamicAndQuery(_codes, (t, k) => t.CodeCD.Equals(k));
                return query.Select(t => t.ValueOf(_type)).ElementAtOrDefault(0);
            }
            catch (Exception e)
            {
                Logger.ExceptionLog(e, ErrorLogDirectory);
                return null;
            }
        }

        private static string ValueOf(this Code t, string _type)
        {
            return
                (_type.ToUpper() == "NAME") ? t.CodeName :
                (_type.ToUpper() == "CODE") ? t.CodeCD :
                (_type.ToUpper() == "CLASSIFICATION") ? t.CodeClassification :
                (_type.ToUpper() == "TYPE") ? t.CodeType : string.Empty;
        }

        internal static IQueryable<TEntity> DynamicAndQuery<TEntity, TKey>(
            this IQueryable<TEntity> query,
            TKey[] keys,
            Expression<Func<TEntity, TKey, bool>> testExpression)
        {
            var arg = Expression.Parameter(typeof(TEntity), "entity");
            Expression expBody = null;
            keys.All(key => { ExpressionValue<TEntity, TKey>(testExpression, arg, ref expBody, key); return true; });
            //Parallel.ForEach(keys, key => { ExpressionValue<TEntity, TKey>(testExpression, arg, ref expBody, key); });

            return query.Where((Expression<Func<TEntity, bool>>)Expression.Lambda(expBody, arg));
        }

        private static void ExpressionValue<TEntity, TKey>(Expression<Func<TEntity, TKey, bool>> testExpression, ParameterExpression arg, ref Expression expBody, TKey key)
        {
            //var keyExp = Expression.Constant(key);
            var invokeExp = Expression.Invoke(testExpression, arg, Expression.Constant(key));
            expBody = expBody == null ? (Expression)invokeExp : (Expression)Expression.And(expBody, invokeExp);
            //if (expBody == null)
            //{
            //    expBody = invokeExp; // first 
            //}
            //else
            //{
            //    expBody = Expression.And(expBody, invokeExp);
            //}
        }
    }
}
