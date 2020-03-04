using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.Web.Models.Threads
{
    public class ThreadsListViewModel
    {
        public ThreadsListViewModel()
        {
            this.Threads = new List<ThreadsAllViewModel>();
        }

        public List<ThreadsAllViewModel> Threads { get; set; }
    }
}
