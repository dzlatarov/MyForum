using MyForum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyForum.Web.Models.Threads
{
    public class ThreadsAllViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int CommentsCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public static Expression<Func<Thread, ThreadsAllViewModel>> AllThreads
        {
            get => t => new ThreadsAllViewModel
            {
                Id = t.Id,
                Username = t.ThreadCreator.UserName,
                Title = t.Title,
                Content = t.Content,
                CreatedOn = t.CreatedOn,
                ModifiedOn = t.ModifiedOn == null ? null : t.ModifiedOn,
                CommentsCount = t.Comments.Count()
            };
        }
    }
}
