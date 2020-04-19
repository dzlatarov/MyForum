using MyForum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyForum.Web.Models.Comments
{
    public class CommentsInfoViewModel
    {
        public string Id { get; set; }

        public string CommentCreator { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Quote { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public static Expression<Func<Comment, CommentsInfoViewModel>> FromComment
        {
            get => c => new CommentsInfoViewModel
            {
                Id = c.Id,  
                CommentCreator = c.CommentCreator.UserName,
                Content = c.Content,
                Quote = c.Quote,
                CreatedOn = c.CreatedOn,
                ModifiedOn = c.ModifiedOn == null ? null : c.ModifiedOn
            };
        }
    }
}