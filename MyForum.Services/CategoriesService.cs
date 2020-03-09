using MyForum.Domain;
using MyForum.Persistence;
using MyForum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyForum.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly MyForumDbContext db;

        public CategoriesService(MyForumDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Category> GetAll()
        {
            var categories = this.db.Categories.ToList();
            return categories;
        }
    }
}
