using MyForum.Domain;
using MyForum.Persistence;
using MyForum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Services
{
    public class NewsService : INewsService
    {
        private readonly MyForumDbContext db;

        public NewsService(MyForumDbContext db)
        {
            this.db = db;
        }

        public async Task Create(string name, string content, string creatorId)
        {
            var news = new News()
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Content = content,
                CreatedOn = DateTime.UtcNow,
                CreatorId = creatorId
            };

            this.db.News.Add(news);
            await this.db.SaveChangesAsync();
        }
    }
}
