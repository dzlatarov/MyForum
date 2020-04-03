using MyForum.Persistence;
using MyForum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Services
{
    public class NewsService : INewsService
    {
        private readonly MyForumDbContext db;

        public NewsService(MyForumDbContext db)
        {
            this.db = db;
        }
    }
}
