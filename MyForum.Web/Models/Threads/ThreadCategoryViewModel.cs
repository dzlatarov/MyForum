using MyForum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyForum.Web.Models.Threads
{
    public class ThreadCategoryViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static Expression<Func<Category, ThreadCategoryViewModel>> FromCategory
        {
            get => c => new ThreadCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            };
        }
    }
}
