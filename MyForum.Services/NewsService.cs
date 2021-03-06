﻿using MyForum.Domain;
using MyForum.Persistence;
using MyForum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task Create(string name, string content, string imageUrl, string creatorId)
        {
            var news = new News()
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Content = content,
                ImageUrl = imageUrl,
                CreatedOn = DateTime.UtcNow,
                CreatorId = creatorId
            };

            this.db.News.Add(news);
            await this.db.SaveChangesAsync();
        }

        public async Task Delete(string newsId)
        {
            var news = this.GetNewsById(newsId);

            this.db.News.Remove(news);
            await this.db.SaveChangesAsync();
        }

        public async Task Edit(string newsId, string name, string content, string imageUrl)
        {
            var newsFromDb = this.GetNewsById(newsId);

            newsFromDb.Name = name;
            newsFromDb.Content = content;
            newsFromDb.ImageUrl = imageUrl;


            this.db.Update(newsFromDb);
            await this.db.SaveChangesAsync();
        }

        public IQueryable<News> GetAll()
        {
            var allNews = this.db.News;
            return allNews;
        }

        public News GetNewsById(string newsId)
        {
            var news = this.db.News.FirstOrDefault(n => n.Id == newsId);

            return news;
        }
    }
}
