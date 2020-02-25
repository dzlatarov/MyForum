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
            this.Posts = new HashSet<Post>();
        }
       
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        //Day of Birth

        public ICollection<Thread> Threads { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
