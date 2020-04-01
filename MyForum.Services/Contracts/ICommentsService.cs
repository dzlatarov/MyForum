using MyForum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Services.Contracts
{
    public interface ICommentsService
    {
        Task CreateComment(string content, string threadId, string creatorId);

        IQueryable<Comment> GetAllComments();

        Task<Comment> GetCommentById(string commentId);

        Task Delete(string commentId);

        Task Edit(string commentId, string content, DateTime modifiedOn);
    }
}