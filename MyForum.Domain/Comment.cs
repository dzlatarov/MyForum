using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Domain
{
    public class Comment
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string PostId { get; set; }

        public Post Post { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
