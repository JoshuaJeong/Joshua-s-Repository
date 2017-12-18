using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xave.com.helper;

namespace xave.web.code.dom
{
    public class DomainLayer
    {

        static ISession db;

        public DomainLayer()
        {
            db = NHibernateHelper.GetSession("Code");
        }

        public string GetConnectionState()
        {
            return db.Connection.State.ToString();
        }

        public string GetConnectionString()
        {
            return db.Connection.ConnectionString;
        }

        public List<T> Read<T>(string documentUid) where T : class
        {
            ICriteria query = db.CreateCriteria(typeof(T));
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

            ICriteria query = db.CreateCriteria(typeof(T));
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
            ICriteria query = db.CreateCriteria(typeof(T));
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
            var query = db.QueryOver<T>();
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
            using (ITransaction transaction = db.BeginTransaction())
            {
                retVal = db.Save(obj);
                db.Flush();
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
            using (ITransaction transaction = db.BeginTransaction())
            {
                //ReferralStatus document = obj as ReferralStatus;
                db.Update(obj);
                db.Flush();
                transaction.Commit();
            }
        }

        /// <summary>
        /// Update (Header)
        /// </summary>
        /// <param name="obj"></param>
        public void SaveOrUpdate(object obj)
        {
            using (ITransaction transaction = db.BeginTransaction())
            {
                //ReferralStatus document = obj as ReferralStatus;
                db.SaveOrUpdate(obj);
                db.Flush();
                transaction.Commit();
            }
        }

        /// <summary>
        /// Delete (Header)
        /// </summary>
        /// <param name="obj"></param>
        public void Delete(object obj)
        {
            using (ITransaction transaction = db.BeginTransaction())
            {
                db.Delete(obj);
                db.Flush();
                transaction.Commit();
            }
        }
        #endregion

        public List<T> Read<T>(Dictionary<string, object> param, int MaxNCount = 0) where T : class
        {
            if (MaxNCount < 1) MaxNCount = 100;

            ICriteria query = db.CreateCriteria(typeof(T));
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
