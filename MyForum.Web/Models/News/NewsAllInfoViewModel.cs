using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.Web.Models.News
{
    public class NewsAllInfoViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ImageUrl { get; set; }

        public string CreatorName { get; set; }
    }
}
