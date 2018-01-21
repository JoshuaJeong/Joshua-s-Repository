using FluentNHibernate.Mapping;

namespace NHibernateLog4Net
{
    public class Store
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string Zip { get; set; }
    }

    public class StoreMap : ClassMap<Store>
    {
        public StoreMap()
        {
            Id(u => u.Id);
            Map(u => u.Name).Nullable();
            Map(u => u.Address).Nullable();
            Map(u => u.City).Nullable();
            Map(u => u.Zip).Nullable();
            Table("store");
        }
    }
}
