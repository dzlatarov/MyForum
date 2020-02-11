using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Domain
{
    public class Thread
    {
        public Thread()
        {
            this.Posts = new HashSet<Post>();
        }
        public string Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
