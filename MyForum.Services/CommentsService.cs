using MyForum.Domain;
using MyForum.Persistence;
using MyForum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly MyForumDbContext db;

        public CommentsService(MyForumDbContext db)
        {
            this.db = db;
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
    }
}
