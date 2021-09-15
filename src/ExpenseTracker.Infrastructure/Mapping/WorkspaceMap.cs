using ExpenseTracker.Core.Entities;
using FluentNHibernate.Mapping;

namespace ExpenseTracker.Infrastructure.Mapping
{
    public class WorkspaceMap: ClassMap<Workspace>
    {
        public WorkspaceMap()
        {
            Table("workspace");
            Id(x => x.WorkspaceId).Column("workspace_id");
            Map(x => x.WorkSpaceName).CustomSqlType("varchar(100)").Not.Nullable().Column("name");
            Map(x => x.Color).CustomSqlType("varchar(50)").Not.Nullable().Column("color");
            Map(x => x.Description).CustomSqlType("text").Nullable().Column("description");
            Map(x => x.Token).CustomSqlType("varchar(50)").Not.Nullable().Column("token");
            Map(x => x.WorkspaceType).CustomSqlType("varchar(50)").Not.Nullable().Column("workspace_type");
            Map(x => x.UserId).Column("user_id").Not.Nullable().Not.Insert().Not.Update();
            References(x => x.User).Column("user_id").Cascade.None();
          
        }        
    }
}