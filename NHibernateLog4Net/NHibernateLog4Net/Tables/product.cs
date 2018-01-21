using FluentNHibernate.Mapping;

namespace NHibernateLog4Net
{
    public class Product
    {
        public virtual int ProductId { get; set; }
        public virtual string Name { get; set; }
        public virtual int Store { get; set; }
    }

    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(u => u.ProductId);
            Map(u => u.Name).Nullable();
            Map(u => u.Store);
            Table("product");
        }
    }
}
