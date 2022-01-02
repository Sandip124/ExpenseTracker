using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Entities.Common;
using Xunit;

namespace ExpenseTracker.UnitTests.Entity
{
    public class WorkSpaceEntityTest
    {
        [Fact]
        public void test_protected_property_sets_correct_value()
        {
            var user = User.Create("admin", "admin");
            var workspace = Workspace.Create(WorkspaceType.Personal,user, "apt", "red");

            typeof(Workspace).GetProperty(nameof(Workspace.UserId))?
                .SetValue(workspace, 1);
            typeof(Workspace).GetProperty(nameof(Workspace.WorkSpaceName))?
                .SetValue(workspace, "apt");
            typeof(Workspace).GetProperty(nameof(Workspace.Color))?
                .SetValue(workspace, "red");
            Assert.Equal("apt", workspace.WorkSpaceName);
            Assert.Equal("red", workspace.Color);
            Assert.Equal(1, workspace.UserId);

        }
        [Fact]
        public void test_set_name_method_sets_correct_value()
        {
            var user = User.Create("admin", "admin");
            var workspace = Workspace.Create(WorkspaceType.Personal,user, "apt", "red");

            workspace.UpdateName("name");
            Assert.Equal("name", workspace.WorkSpaceName);
        }
        [Fact]
        public void test_set_default_method_sets_default()
        {
            var user = User.Create("admin", "admin");
            var workspace = Workspace.Create(WorkspaceType.Personal,user, "apt", "red");

            workspace.SetDefault();
            Assert.True(workspace.IsDefault);
        }
        [Fact]
        public void test_change_color_method_sets_default()
        {
            var user = User.Create("admin", "admin");
            var workspace = Workspace.Create(WorkspaceType.Personal,user, "apt", "red");

            workspace.UpdateColor("cc");
            Assert.Equal("cc", workspace.Color);
        }
    }
}
