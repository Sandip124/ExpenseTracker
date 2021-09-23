using ExpenseTracker.Core.Entities;
using FluentNHibernate.Mapping;

namespace ExpenseTracker.Infrastructure.Mapping
{
    public class TransactionMap: ClassMap<Transaction>
    {
        public TransactionMap()
        {
            Table("transaction");
            Id(a => a.Id).Column("transaction_id");
            Map(a => a.Amount).CustomSqlType("decimal(18,2)").Column("amount");
            Map(a => a.Description).CustomSqlType("text").Column("description");
            Map(a => a.EntryDate).Column("entry_date");
            Map(a => a.TransactionDate).Column("transaction_date");
            Map(a => a.Type).Column("type");
            Map(x => x.TransactionCategoryId).Column("transaction_category_id").Not.Nullable().Not.Insert().Not.Update();
            References(x => x.TransactionCategory).Column("transaction_category_id").Cascade.None();
            Map(x => x.WorkspaceId).Column("workspace_id").Not.Nullable().Not.Insert().Not.Update();
            References(x => x.Workspace).Column("workspace_id").Cascade.None();

        }
    }
}