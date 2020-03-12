using MyForum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.Web.Models.Threads
{
    public class ThreadsListCategoriesViewModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public List<Category> Categories { get; set; }
    }
}
