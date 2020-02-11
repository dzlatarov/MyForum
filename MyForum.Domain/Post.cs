using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Domain
{
    public class Post
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string ThreadId { get; set; }

        public Thread Thread { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
