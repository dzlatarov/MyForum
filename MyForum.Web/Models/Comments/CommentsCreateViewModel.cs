using MyForum.Domain;
using MyForum.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyForum.Web.Models.Comments
{
    public class CommentsCreateViewModel
    {
        [Required]
        [StringLength(GlobalConstants.CommentContentMaxLength, ErrorMessage = GlobalConstants.CommentErrorMessage)]
        [Display(Name = "Comment")]
        public string Content { get; set; }

        public string ThreadId { get; set; }

        public string CreatorId { get; set; }

        [Display(Name = "Thread")]
        public string ThreadContent { get; set; }

        public static Expression<Func<Comment, CommentsCreateViewModel>> FromComment
        {
            get => c => new CommentsCreateViewModel
            {
                Content = c.Content,
                ThreadId = c.ThreadId,
                CreatorId = c.CommentCreatorId,
                ThreadContent = c.Thread.Content
            };
        }
    }
}
