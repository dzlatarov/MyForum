using MyForum.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace MyForum.Web.Models.News
{
    public class NewsCreateViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(GlobalConstants.NewsTitleMaxLength, ErrorMessage = GlobalConstants.NewsTitleErrorMessage,
            MinimumLength = GlobalConstants.NewsTitleMinLength)]
        [Display(Name = "Title")]
        public string Name { get; set; }

        [Required]
        [StringLength(GlobalConstants.NewsContentMaxLength, ErrorMessage = GlobalConstants.NewsContentErrorMessage, MinimumLength = GlobalConstants.NewsContentMinLength)]
        public string Content { get; set; }

        public string ImageUrl { get; set; }
    }
}
