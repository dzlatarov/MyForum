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
        public string Username { get; set; }

        public string Content { get; set; }

        public int CommentsPost { get; set; }

        public static Expression<Func<Thread, ThreadsAllViewModel>> AllThreads
        {
            get => t => new ThreadsAllViewModel
            {
                Username = t.ThreadCreator.UserName,
                Content = t.Content,
                CommentsPost = t.Comments.Count()
            };
        }
    }
}
