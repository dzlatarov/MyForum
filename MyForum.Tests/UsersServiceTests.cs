using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Threading.Tasks;
using MyForum.Domain;

namespace MyForum.Tests
{
    public class UsersServiceTests
    {
        [Fact]
        public async Task CheckIfUserIsDeactivateStateReturnsFalse()
        {
            var mockUser = new Mock<ApplicationUser>();
            mockUser.Setup(u => u.UserName).Returns("pesho");

            var expected = false;

            Assert.Equal(expected, mockUser.Object.IsDeactivate);
        }

        //public async Task Check
    }
}
