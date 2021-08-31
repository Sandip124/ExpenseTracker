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
            Map(x => x.BackgroundImage).CustomSqlType("varchar(50)").Not.Nullable().Column("background_image");
            Map(x => x.Description).CustomSqlType("text").Nullable().Column("description");
            Map(x => x.Token).CustomSqlType("varchar(50)").Nullable().Column("token");
            
          
        }        
    }
}