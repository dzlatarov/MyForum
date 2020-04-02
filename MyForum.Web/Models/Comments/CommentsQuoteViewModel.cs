using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.Web.Models.Comments
{
    public class CommentsQuoteViewModel
    {
        public string QuotedCommentId { get; set; }

        public string QuotedCommentCreator { get; set; }

        [Required]
        public string QuotedCommentContent { get; set; }

        public DateTime QuotedCommentCreatedOn { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string NewCommentContent { get; set; }
    }
}