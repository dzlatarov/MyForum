using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.Web.Models.Threads
{
    public class ThreadsInCategoryListViewModel
    {
        public string Name { get; set; }
        public IList<ThreadsAllViewModel> Threads { get; set; }
    }
}
