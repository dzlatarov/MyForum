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

        public IQueryable<Category> GetAll()
        {
            var categories = this.db.Categories;
            return categories;
        }

        public Category GetCategoryById(string id)
        {
            var category = this.db.Categories.FirstOrDefault(c => c.Id == id);
            return category;
        }
    }
}