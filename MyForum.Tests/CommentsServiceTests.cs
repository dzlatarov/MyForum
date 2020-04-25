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

        [Fact]
        public async Task CheckIfDeleteMethodReturnsTheCorrectCountOfComments()
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

            await commentsService.Delete("123456");

            var expected = 0;
            var actual = await commentsService.GetAllComments().CountAsync();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CheckIfEditMethodChangesCorrectly()
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

            var content = "some content";
            var createdOn = DateTime.UtcNow;

            await commentsService.Edit("123456", "only content", DateTime.Now);
            var commentFromDb = await commentsService.GetCommentById("123456");

            Assert.True(content != commentFromDb.Content);
            Assert.True(createdOn != commentFromDb.CreatedOn);
        }
    }
}