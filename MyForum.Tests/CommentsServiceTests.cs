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
    public class CommentsServiceTests
    {
        [Fact]
        public async Task CheckIfGetCommentIdRetursTheCorrectComment()
        {
            var optionBuilder = new DbContextOptionsBuilder<MyForumDbContext>()
             .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var context = new MyForumDbContext(optionBuilder.Options);
            var usersService = new UsersService(context);
            var categoriesService = new CategoriesService(context);
            var threadsService = new ThreadsService(context, usersService, categoriesService);
            var commentsService = new CommentsService(context, threadsService, usersService);

            var comment = new Comment
            {
                Id = "123456",
                Content = "some content",
                ThreadId = "1122",
                CommentCreatorId = "3344",
                CreatedOn = DateTime.UtcNow,
                Quote = "some quote"
            };

            context.Comments.Add(comment);
            context.SaveChanges();

            var expected = comment;
            var actual = commentsService.GetCommentById("123456").Result;

            Assert.Equal(expected, actual);
        }
    }
}