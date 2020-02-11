using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Domain
{
    public class User
    {
        public User()
        {
            this.Threads = new HashSet<Thread>();
            this.Comments = new HashSet<Comment>();
            this.Posts = new HashSet<Post>();
        }

        public string Id { get; set; }

        public ICollection<Thread> Threads { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
