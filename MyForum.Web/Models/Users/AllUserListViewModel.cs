using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.Web.Models
{
    public class AllUserListViewModel
    {
        public AllUserListViewModel()
        {
            this.Members = new List<AllUsersViewModel>();
        }

        public List<AllUsersViewModel> Members { get; set; }
    }
}
