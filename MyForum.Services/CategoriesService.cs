using MyForum.Domain;
using MyForum.Persistence;
using MyForum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly MyForumDbContext db;

        public CategoriesService(MyForumDbContext db)
        {
            this.db = db;
        }

        public async Task Create(string name, string description, string imageUrl)
        {
            var newCategory = new Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Description = description,
                ImageUrl = imageUrl
            };

            this.db.Categories.Add(newCategory);
            await this.db.SaveChangesAsync();
        }

        public async Task Delete(string categoryId)
        {
            var category = this.GetCategoryById(categoryId).Result;

            this.db.Remove(category);
            await this.db.SaveChangesAsync();
        }

        public async Task Edit(string categoryId, string name, string description, string imageUrl)
        {
            var categoryFromDb = this.GetCategoryById(categoryId).Result;

            categoryFromDb.Name = name;
            categoryFromDb.Description = description;
            categoryFromDb.ImageUrl = imageUrl;


            this.db.Update(categoryFromDb);
            await this.db.SaveChangesAsync();
        }

        public IQueryable<Category> GetAll()
        {
            var categories = this.db.Categories;
            return categories;
        }

        public async Task<Category> GetCategoryById(string id)
        {
            var category = this.db.Categories.FirstOrDefault(c => c.Id == id);
            return category;
        }

        public Category GetCategoryByName(string name)
        {
            var category = this.db.Categories.FirstOrDefault(c => c.Name == name);
            return category;
        }
    }
}