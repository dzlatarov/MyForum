using MyForum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyForum.Web.Models
{
    public class AllUsersViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public int ThreadsCount { get; set; }        

        public static Expression<Func<ApplicationUser, AllUsersViewModel>> AllUser
        {
            get => u => new AllUsersViewModel
            {
                Id = u.Id,
                Username = u.UserName,
                ThreadsCount = u.Threads.Count,                
            };
        }
    }
}