using Microsoft.EntityFrameworkCore;
using MyForum.Domain;
using MyForum.Persistence;
using MyForum.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyForum.Tests
{
    public class ThreadsServiceTests
    {
        [Fact]
        public async Task CheckIfGetThreadByIdReturnsCorrectThread()
        {
            var optionBuilder = new DbContextOptionsBuilder<MyForumDbContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var context = new MyForumDbContext(optionBuilder.Options);
            var usersService = new UsersService(context);
            var categoriesService = new CategoriesService(context);
            var threadsService = new ThreadsService(context, usersService, categoriesService);

            var thread = new Thread
            {
                Id = "112233",
                Title = "some title",
                Content = "some content",
                CreatedOn = DateTime.UtcNow,
                ThreadCreatorId = "1122",
                CategoryId = "2211"
            };

            await context.Threads.AddAsync(thread);
            await context.SaveChangesAsync();

            var expected = thread;
            var actual = threadsService.GetThreadById(thread.Id).Result;

            Assert.Equal(expected, actual);
        }
    }
}
