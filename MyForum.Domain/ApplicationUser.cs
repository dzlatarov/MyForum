﻿using Microsoft.AspNetCore.Identity;
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

        public ICollection<Thread> Threads { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}