using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Threading.Tasks;
using MyForum.Domain;
using MyForum.Services;
using System.Linq;
using MyForum.Domain.Enums;
using MyForum.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MyForum.Tests
{
    public class UsersServiceTests
    {
        public IQueryable<ApplicationUser> GetApplicationUsers()
        {
            var users = new List<ApplicationUser>()
            {
                new ApplicationUser
                {
                    Id = "5566",
                    UserName = "Ivancho",
                    NormalizedUserName = "IVANCHO",
                    Email = "ivancho@abv.bg",
                    NormalizedEmail = "IVANCHO@ABV.BG",
                    PasswordHash = "123",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = true,
                    FirstName = "Ivan",
                    MiddleName = "Ivanov",
                    LastName = "Ivanov",
                    Gender = Gender.Male,
                    IsDeactivate = false
                },
                new ApplicationUser
                {
                    Id = "6677",
                    UserName = "Petar",
                    NormalizedUserName = "PETAR",
                    Email = "petar@abv.bg",
                    NormalizedEmail = "PETAR@ABV.BG",
                    PasswordHash = "123",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = true,
                    FirstName = "Petar",
                    MiddleName = "Petrov",
                    LastName = "Petrov",
                    Gender = Gender.Male,
                    IsDeactivate = false
                }
            };

            return users.AsQueryable();
        }

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

        [Fact]
        public void MethodGetUserByUsername_ReturnsCorrectUser()
        {
            var repo = new Mock<IUsersService>();
            repo.Setup(r => r.GetUserByUsername("Petar")).Returns(GetApplicationUsers().Where(u => u.UserName == "Petar").FirstOrDefault());


            var user = repo.Object.GetUserByUsername("Petar");

            Assert.True(user.UserName == "Petar");
            Assert.True(user.PasswordHash == "123");
        }

        [Fact]
        public void MethodGetUserById_ReturnsCorrectUser()
        {
            var repo = new Mock<IUsersService>();
            repo.Setup(r => r.GetUserById("5566")).Returns(GetApplicationUsers().Where(u => u.Id == "5566").FirstOrDefault());


            var user = repo.Object.GetUserById("5566");

            Assert.True(user.UserName == "Ivancho");
            Assert.True(user.FirstName == "Ivan");
            Assert.True(user.MiddleName == "Ivanov");
            Assert.True(user.LastName == "Ivanov");
        }

        [Fact]
        public async Task CheckIfDeactivateChangeIsDeactivateStatusCorrectly()
        {
            var optionBuilder = new DbContextOptionsBuilder<MyForumDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var context = new MyForumDbContext(optionBuilder.Options);
            var userService = new UsersService(context);

            var appUser = new ApplicationUser
            {
                Id = "1516",
                UserName = "pesho",
                NormalizedUserName = "pesho".ToUpper(),
                Email = "pesho@abv.bg",
                NormalizedEmail = "pesho@abv.bg".ToUpper(),
                PasswordHash = "123",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                FirstName = "Ivan",
                MiddleName = "Ivanov",
                LastName = "Ivanov",
                Gender = Gender.Male,
                IsDeactivate = false
            };

            await context.Users.AddAsync(appUser);
            await context.SaveChangesAsync();


            var user = appUser;
            var userId = user.Id;

            await userService.Deactivate(userId);

            var expected = true;
            var actual = context.Users.FirstOrDefault(x => x.UserName == user.UserName).IsDeactivate;


            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CheckIfActiveMethodChangeIsDeactiveStatusCorrectly()
        {
            var optionBuilder = new DbContextOptionsBuilder<MyForumDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var context = new MyForumDbContext(optionBuilder.Options);
            var userService = new UsersService(context);

            var appUser = new ApplicationUser
            {
                Id = "1516",
                UserName = "pesho",
                NormalizedUserName = "pesho".ToUpper(),
                Email = "pesho@abv.bg",
                NormalizedEmail = "pesho@abv.bg".ToUpper(),
                PasswordHash = "123",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                FirstName = "Ivan",
                MiddleName = "Ivanov",
                LastName = "Ivanov",
                Gender = Gender.Male,
                IsDeactivate = true
            };

            await context.Users.AddAsync(appUser);
            await context.SaveChangesAsync();


            var user = appUser;
            var userId = user.Id;

            await userService.Activate(userId);

            var expected = false;
            var actual = context.Users.FirstOrDefault(x => x.UserName == user.UserName).IsDeactivate;


            Assert.Equal(expected, actual);
        }
    }
}