using FluentNHibernate.Mapping;
using System;

namespace NHibernateLog4Net
{
    public class Log
    {
        public virtual int Id { get; set; }
        public virtual string ApplicationEntity { get; set; }
        public virtual string Method { get; set; }
        public virtual string RequestMessage { get; set; }
        public virtual string ResponseMessage { get; set; }
        public virtual string UserMessage { get; set; }
        public virtual string Endpoint { get; set; }
        public virtual DateTime Regdate { get; set; }
        public virtual string RequesterIPAddress { get; set; }
    }

    public class LogMap : ClassMap<Log>
    {
        public LogMap()
        {
            Id(u => u.Id);
            Map(u => u.ApplicationEntity).Nullable();
            Map(u => u.Method).Nullable();
            Map(u => u.RequestMessage).Nullable();
            Map(u => u.ResponseMessage).Nullable();
            Map(u => u.UserMessage).Nullable();
            Map(u => u.Endpoint).Nullable();
            Map(u => u.Regdate).Nullable();
            Map(u => u.RequesterIPAddress).Nullable();
            Table("TB_LOG");
        }
    }
}
