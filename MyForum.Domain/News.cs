using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Domain
{
    public class News
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
