using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xave.com.helper.model
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
            Id(u => u.Id).Column("C_ID");
            Map(u => u.ApplicationEntity).Column("C_APPLICATIONENTITY").Nullable();
            Map(u => u.Method).Column("C_METHOD").Nullable();
            Map(u => u.RequestMessage).Column("C_REQUESTMESSAGE").Nullable();
            Map(u => u.ResponseMessage).Column("C_RESPONSEMESSAGE").Nullable();
            Map(u => u.UserMessage).Column("C_USERMESSAGE").Nullable();
            Map(u => u.Endpoint).Column("C_ENDPOINT").Nullable();
            //Map(u => u.Regdate).Column("C_REGDATE").Nullable();
            Map(u => u.RequesterIPAddress).Column("C_REQUESTERIPADDRESS").Nullable();
            Table("TB_LOG");
        }
    }
}
