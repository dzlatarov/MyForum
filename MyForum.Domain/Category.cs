using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Domain
{
    public class Category
    {
        public Category()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Threads = new HashSet<Thread>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Thread> Threads { get; }
    }
}