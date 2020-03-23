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
        

        [Display(Name = "Thread")]
        public string ThreadContent { get; set; }        
    }
}
