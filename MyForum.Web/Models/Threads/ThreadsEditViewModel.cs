using MyForum.Domain;
using MyForum.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyForum.Web.Models.Threads
{
    public class ThreadsEditViewModel
    {
        public string Id { get; set; }

        [StringLength(GlobalConstants.TitleMaxLength, ErrorMessage = GlobalConstants.TitleErrorMessage, MinimumLength = GlobalConstants.TitleMinLength)]
        public string Title { get; set; }

        [StringLength(GlobalConstants.ContentMaxLength, ErrorMessage = GlobalConstants.ContentErrorMessage, MinimumLength = GlobalConstants.ContentMinLength)]
        public string Content { get; set; }

        public string CategoryId { get; set; }

        public static Expression<Func<Thread, ThreadsEditViewModel>> EditThread
        {
            get => t => new ThreadsEditViewModel
            {
                Id = t.Id,
                Title = t.Title,
                Content = t.Content,
                CategoryId = t.CategoryId
            };
        }
    }
}