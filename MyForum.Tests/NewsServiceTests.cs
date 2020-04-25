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
    public class NewsServiceTests
    {
        [Fact]
        public async Task CheckIfDeleteMethodReturnCorrectCountOfNews()
        {
            var optionBuilder = new DbContextOptionsBuilder<MyForumDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var context = new MyForumDbContext(optionBuilder.Options);
            var newsService = new NewsService(context);

            var news = new News
            {
                Id = "123456",
                Name = "some name",
                Content = "some content",
                ImageUrl = "some img",
                CreatedOn = DateTime.UtcNow,
                CreatorId = "1122"
            };

            await context.News.AddAsync(news);
            await context.SaveChangesAsync();

            await newsService.Delete("123456");

            var expected = 0;
            var actual = await newsService.GetAll().CountAsync();

            Assert.Equal(expected, actual);
        }
    }
}
