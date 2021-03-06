﻿using MyForum.Domain;
using MyForum.Persistence;
using MyForum.Services.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly MyForumDbContext db;
        private readonly IThreadsService threadsService;
        private readonly IUsersService usersService;

        public CommentsService(MyForumDbContext db, IThreadsService threadsService, IUsersService usersService)
        {
            this.db = db;
            this.threadsService = threadsService;
            this.usersService = usersService;
        }

        public void CreateComment(string content, string threadId, string creatorId)
        {
            var comment = new Comment
            {
                Content = content,
                ThreadId = threadId,
                CommentCreatorId = creatorId,
                CreatedOn = DateTime.UtcNow
            };

            this.db.Comments.Add(comment);
            this.db.SaveChanges();
        }

        public void CreateCommentQuote(string content, string threadId, string creatorId, string quote)
        {
            var comment = new Comment
            {
                Content = content,
                ThreadId = threadId,
                CommentCreatorId = creatorId,
                CreatedOn = DateTime.UtcNow,
                Quote = quote
            };

            this.db.Comments.Add(comment);
            this.db.SaveChanges();
        }

        public async Task Delete(string commentId)
        {
            var comment = this.GetCommentById(commentId).Result;

            this.db.Comments.Remove(comment);
            await this.db.SaveChangesAsync();
        }

        public async Task Edit(string commentId, string content, DateTime modifiedOn)
        {
            var commentFromDb = this.db.Comments.FirstOrDefault(c => c.Id == commentId);

            commentFromDb.Content = content;
            commentFromDb.ModifiedOn = modifiedOn;

            this.db.Comments.Update(commentFromDb);
            await this.db.SaveChangesAsync();
        }

        public IQueryable<Comment> GetAllComments()
        {
            return this.db.Comments;
        }

        public async Task<Comment> GetCommentById(string commentId)
        {
            return this.db.Comments.FirstOrDefault(c => c.Id == commentId);
        }
    }
}