using Microsoft.AspNetCore.Identity;
using MyForum.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Threads = new HashSet<Thread>();
            this.Comments = new HashSet<Comment>();
            this.News = new HashSet<News>();
        }

        public string FirstName { get; set; }


        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool IsDeactivate { get; set; }

        public ICollection<Thread> Threads { get; set; }

        public ICollection<News> News { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}