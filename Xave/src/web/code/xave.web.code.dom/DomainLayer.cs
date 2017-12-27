using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using xave.com.helper;
using xave.web.code.dto;

namespace xave.web.code.dom
{
    public class DomainLayer
    {
        private static object syncRoot = new Object();

        private static ISession session;
        private static ISession Session
        {
            get
            {
                if (session == null || !session.IsOpen)
                {
                    lock (syncRoot)
                    {
                        session = NHibernateHelper.GetSession("Code", new List<Type>() { typeof(CodeMap), typeof(FormatMap) });
                    }
                }
                return session;
            }
        }

        public DomainLayer()
        {
            //List<Type> _MappingClasses = new List<Type>() { typeof(CodeMap), typeof(FormatMap), typeof(KOSTOM_DiagnosisMap) };
            //List<Type> _MappingClasses = new List<Type>() { typeof(CodeMap), typeof(FormatMap) };
            //Session = NHibernateHelper.GetSession("Code", _MappingClasses);
        }

        public string GetConnectionState()
        {
            return Session.Connection.State.ToString();
        }

        public string GetConnectionString()
        {
            return Session.Connection.ConnectionString;
        }

        public List<T> Read<T>(string documentUid) where T : class
        {
            ICriteria query = Session.CreateCriteria(typeof(T));
            if (!string.IsNullOrEmpty(documentUid))
            {
                query.Add(Expression.Eq("ReferralDocumentUniqueId", documentUid));
                IList<T> retVal = query.List<T>();
                return retVal != null ? retVal.ToList() : null;
            }
            return null;
        }

        public List<T> Read<T>(object value, string param, int MaxNCount = 0) where T : class
        {
            if (MaxNCount < 1) MaxNCount = 100;

            ICriteria query = Session.CreateCriteria(typeof(T));
            if (value != null && !string.IsNullOrEmpty(param))
            {
                query.Add(Expression.Eq(param, value));
                IEnumerable<T> retVal = query.List<T>().Take(MaxNCount);
                return retVal != null ? retVal.ToList() : null;
            }
            return null;
        }

        public int GetMaxSequence<T>(object value, string param, string sequenceColumn) where T : class
        {
            ICriteria query = Session.CreateCriteria(typeof(T));
            if (value != null && !string.IsNullOrEmpty(param))
            {
                query.Add(Restrictions.Eq(param, value));
                query.SetProjection(Projections.Max(sequenceColumn));
                return query.UniqueResult<int>();
            }
            else return 0;
        }

        #region ------- Basic CRUD -------
        /// <summary>
        /// Read
        /// </summary>
        /// <returns></returns>
        public List<T> Read<T>() where T : class
        {
            try
            {
                var query = Session.QueryOver<T>();
                List<T> retVal = query.List() as List<T>;
                return retVal;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Create (Header)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public object Create(object obj)
        {
            object retVal = null;
            using (ITransaction transaction = Session.BeginTransaction())
            {
                retVal = Session.Save(obj);
                Session.Flush();
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
            using (ITransaction transaction = Session.BeginTransaction())
            {
                //ReferralStatus document = obj as ReferralStatus;
                Session.Update(obj);
                Session.Flush();
                transaction.Commit();
            }
        }

        /// <summary>
        /// Update (Header)
        /// </summary>
        /// <param name="obj"></param>
        public void SaveOrUpdate(object obj)
        {
            using (ITransaction transaction = Session.BeginTransaction())
            {
                //ReferralStatus document = obj as ReferralStatus;
                Session.SaveOrUpdate(obj);
                Session.Flush();
                transaction.Commit();
            }
        }

        /// <summary>
        /// Delete (Header)
        /// </summary>
        /// <param name="obj"></param>
        public void Delete(object obj)
        {
            using (ITransaction transaction = Session.BeginTransaction())
            {
                Session.Delete(obj);
                Session.Flush();
                transaction.Commit();
            }
        }
        #endregion

        public List<T> Read<T>(Dictionary<string, object> param, int MaxNCount = 0) where T : class
        {
            if (MaxNCount < 1) MaxNCount = 100;

            ICriteria query = Session.CreateCriteria(typeof(T));
            if (param != null && param.Any())
            {
                foreach (var item in param)
                {
                    query.Add(Expression.Eq(item.Key, item.Value));
                }
                IEnumerable<T> retVal = query.List<T>().Take(MaxNCount);
                return retVal != null ? retVal.ToList() : null;
            }
            return null;
        }
    }
}
