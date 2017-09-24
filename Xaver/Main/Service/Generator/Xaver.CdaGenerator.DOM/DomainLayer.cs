using Generator.Entity.StructureSet;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using Xaver.Framework;

namespace Xaver.CdaGenerator.DOM
{
    public class DomainLayer : BaseDac<Document, Int64>// : DacBase<Document>
    {
        protected static SessionHelper _sessionHelper = null;
        public DomainLayer()
        {
            SessionOpen();
        }

        private static void SessionOpen()
        {
            if (_sessionHelper == null)
            {
                _sessionHelper = new SessionHelper();
                _sessionHelper.OpenSession();
            }
        }
        public string GetConnectionState()
        {
            return CurrentSession.Connection.State.ToString();
        }

        public string GetConnectionString()
        {
            return CurrentSession.Connection.ConnectionString;
        }

        public List<T> Read<T>(string documentUid) where T : class
        {
            ICriteria query = CurrentSession.CreateCriteria(typeof(T));
            if (!string.IsNullOrEmpty(documentUid))
            {
                query.Add(Expression.Eq("ReferralDocumentUniqueId", documentUid));
                IList<T> retVal = query.List<T>();
                return retVal != null ? retVal.ToList() : null;
            }
            return null;
        }

        public List<T> Read<T>(object value, string param) where T : class
        {
            ICriteria query = CurrentSession.CreateCriteria(typeof(T));
            if (value != null && !string.IsNullOrEmpty(param))
            {
                query.Add(Expression.Eq(param, value));
                IList<T> retVal = query.List<T>();
                return retVal != null ? retVal.ToList() : null;
            }
            return null;
        }

        #region ------- Basic CRUD -------
        /// <summary>
        /// Read
        /// </summary>
        /// <returns></returns>
        public List<T> Read<T>() where T : class
        {
            var query = CurrentSession.QueryOver<T>();
            List<T> retVal = query.List() as List<T>;
            return retVal;
        }

        /// <summary>
        /// Create (Header)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public object Create(object obj)
        {
            object retVal = null;
            using (ITransaction transaction = CurrentSession.BeginTransaction())
            {
                retVal = CurrentSession.Save(obj);
                CurrentSession.Flush();
                transaction.Commit();
            }
            return retVal;
        }

        /// <summary>
        /// Update (Header)
        /// </summary>
        /// <param name="obj"></param>
        public void Update(object obj)
        {
            using (ITransaction transaction = CurrentSession.BeginTransaction())
            {
                //ReferralStatus document = obj as ReferralStatus;
                CurrentSession.Update(obj);
                CurrentSession.Flush();
                transaction.Commit();
            }
        }

        /// <summary>
        /// Update (Header)
        /// </summary>
        /// <param name="obj"></param>
        public void SaveOrUpdate(object obj)
        {
            using (ITransaction transaction = CurrentSession.BeginTransaction())
            {
                //ReferralStatus document = obj as ReferralStatus;
                CurrentSession.SaveOrUpdate(obj);
                CurrentSession.Flush();
                transaction.Commit();
            }
        }

        ///// <summary>
        ///// Delete (Header)
        ///// </summary>
        ///// <param name="obj"></param>
        //public void Delete(object obj)
        //{
        //    using (ITransaction transaction = CurrentSession.BeginTransaction())
        //    {
        //        ReferralStatus document = obj as ReferralStatus;
        //        CurrentSession.Delete(document);
        //        CurrentSession.Flush();
        //        transaction.Commit();
        //    }
        //}
        #endregion

        public List<T> Read<T>(Dictionary<string, object> param) where T : class
        {
            ICriteria query = CurrentSession.CreateCriteria(typeof(T));
            if (param != null && param.Any())
            {
                foreach (var item in param)
                {
                    query.Add(Expression.Eq(item.Key, item.Value));
                }
                IList<T> retVal = query.List<T>();
                return retVal != null ? retVal.ToList() : null;
            }
            return null;
        }
    }
}
