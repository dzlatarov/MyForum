using MyForum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyForum.Services.Contracts
{
    public interface ICommentsService
    {
        void CreateComment(string content, string threadId, string creatorId);

        IQueryable<Comment> GetAllComments();

        Comment GetCommentById(string commentId);

        void Delete(string commentId);
    }
}