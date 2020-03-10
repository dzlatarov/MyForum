using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Domain
{
    public class Comment
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string ThreadId { get; set; }

        public Thread Thread { get; set; }

        public string CommentCreatorId { get; set; }

        public ApplicationUser CommentCreator { get; set; }
    }
}