using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Threading.Tasks;
using MyForum.Domain;
using MyForum.Services;
using System.Linq;

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

        [Fact]
        public async Task CheckThreadCount_shouldBe0()
        {
            var mockUser = new Mock<ApplicationUser>();
            mockUser.Setup(u => u.UserName).Returns("pesho");

            var mockUsersService = new Mock<IUsersService>();
            mockUsersService.Setup(u => u.GetUserByUsername(mockUser.Object.UserName));


            var threadCreatedCountByUser = mockUser.Object.Threads.Count;
            var expected = 0;

            Assert.Equal(expected, threadCreatedCountByUser);
        }
    }
}
