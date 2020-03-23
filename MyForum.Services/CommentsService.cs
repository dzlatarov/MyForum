using MyForum.Domain;
using MyForum.Persistence;
using MyForum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public IQueryable<Comment> GetAllComments()
        {
            return this.db.Comments;
        }
    }
}
