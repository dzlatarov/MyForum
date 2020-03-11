using MyForum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyForum.Web.Models.Threads
{
    public class ThreadsEditViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

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
