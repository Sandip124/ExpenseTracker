using ExpenseTracker.Core.Entities;
using Xunit;

namespace ExpenseTracker.UnitTests.Entity
{
    public class UserEntityTest
    {
        [Fact]
        public void test_protected_property_sets_correct_value()
        {
            var user = User.Create("admin", "admin");

            typeof(User).GetProperty(nameof(User.UserId))?
                .SetValue(user, 1);
            typeof(User).GetProperty(nameof(User.Username))?
                .SetValue(user, "ad");
            typeof(User).GetProperty(nameof(User.Password))?
                .SetValue(user, "psd");
            Assert.Equal("ad", user.Username);
            Assert.Equal("psd", user.Password);
            Assert.Equal(1, user.UserId);

        }
        [Fact]
        public void test_Set_User_Name_sets_correct_value()
        {
            var user = User.Create("admin", "admin");
            user.SetUserName("aa");
            Assert.Equal("aa", user.Username);

        }
        [Fact]
        public void test_Set_User_pass_sets_correct_value()
        {
            var user = User.Create("admin", "admin");
            user.SetPassword("aa");
            Assert.Equal("aa", user.Password);

        }
    }
}
