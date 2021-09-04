using ExpenseTracker.Core.Entities;
using FluentNHibernate.Mapping;

namespace ExpenseTracker.Infrastructure.Mapping
{
    public class TransactionCategoryMap: ClassMap<TransactionCategory>
    {
        public TransactionCategoryMap()
        {
            Table("transaction_category");
            Id(x => x.TransactionCategoryId).Column("transaction_category_id");
            Map(x => x.CategoryName).CustomSqlType("varchar(100)").Not.Nullable().Column("category_name");
            Map(x => x.Color).CustomSqlType("varchar(50)").Not.Nullable().Column("color");
            Map(x => x.Icon).CustomSqlType("text").Not.Nullable().Column("icon");
            Map(x => x.Type).CustomSqlType("varchar(50)").Nullable().Column("type");
            HasMany(x => x.Transactions)
                .Cascade.All()
                .KeyColumn("transaction_category_id")
                .LazyLoad()
                .Inverse();
        }
    }
}