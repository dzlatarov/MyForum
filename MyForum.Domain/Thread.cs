using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Domain
{
    public class Thread
    {
        public Thread()
        {
            this.Comments = new HashSet<Comment>();
        }
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ThreadCreatorId { get; set; }

        public ApplicationUser ThreadCreator { get; set; }

        public string CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
