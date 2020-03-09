using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Domain
{
    public class Post
    {
        public Post()
        {
            this.Id = Guid.NewGuid().ToString();
            //this.Comments = new HashSet<Comment>();
        }

        public string Id { get; set; }
        
        public string Content { get; set; }

        //public string ThreadId { get; set; }

        //public Thread Thread { get; set; }

        //public string PostCreatorId { get; set; }

        //public ApplicationUser PostCreator { get; set; }

        //public ICollection<Comment> Comments { get; set; }
    }
}
