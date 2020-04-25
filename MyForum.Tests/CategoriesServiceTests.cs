using Microsoft.EntityFrameworkCore;
using MyForum.Domain;
using MyForum.Persistence;
using MyForum.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyForum.Tests
{
    public class CategoriesServiceTests
    {
        [Fact]
        public async Task CheckIfGetCategoryByIdReturnsCorrectCategory()
        {
            var optionBuilder = new DbContextOptionsBuilder<MyForumDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var context = new MyForumDbContext(optionBuilder.Options);
            var categoryService = new CategoriesService(context);

            var appCategories = new List<Category>
            {
                new Category
                {
                    Id = "1122",
                    Name = "Action",
                    Description = "Description......"
                },
                new Category
                {
                    Id = "1123",
                    Name = "Adventure",
                    Description = "Description......"
                }
            };

            await context.Categories.AddRangeAsync(appCategories);
            await context.SaveChangesAsync();

            var category = appCategories[0];
            var categoryId = category.Id;

            var actualUser = await categoryService.GetCategoryById(categoryId);
            var expected = category;

            Assert.Equal(expected, actualUser);
        }

        [Fact]
        public void CheckIfCategoryGetAllReturnCorrectAllCategories()
        {
            var optionBuilder = new DbContextOptionsBuilder<MyForumDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var context = new MyForumDbContext(optionBuilder.Options);
            var categoryService = new CategoriesService(context);

            var appCategories = new List<Category>
            {
                new Category
                {
                    Id = "1122",
                    Name = "Action",
                    Description = "Description......"
                },
                new Category
                {
                    Id = "1123",
                    Name = "Adventure",
                    Description = "Description......"
                }
            };

            context.Categories.AddRange(appCategories);
            context.SaveChanges();

            var expected = appCategories.AsQueryable();
            var actual = categoryService.GetAll();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CheckIfAfterDeleteReturnsTheCorrectCountOfCategories()
        {
            var optionBuilder = new DbContextOptionsBuilder<MyForumDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var context = new MyForumDbContext(optionBuilder.Options);
            var categoryService = new CategoriesService(context);

            var appCategories = new List<Category>
            {
                new Category
                {
                    Id = "1122",
                    Name = "Action",
                    Description = "Description......"
                },
                new Category
                {
                    Id = "1123",
                    Name = "Adventure",
                    Description = "Description......"
                }
            };

            context.Categories.AddRange(appCategories);
            context.SaveChanges();

            var category = appCategories[0];
            var categoryId = category.Id;

            var expectedCount = 1;
            await categoryService.Delete(categoryId);
            var actual = categoryService.GetAll().Count();

            Assert.Equal(expectedCount, actual);
        }

        [Fact]
        public async Task CheckIfEditMethodChangesCorrectly()
        {
            var optionBuilder = new DbContextOptionsBuilder<MyForumDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var context = new MyForumDbContext(optionBuilder.Options);
            var categoryService = new CategoriesService(context);

            var appCategories = new List<Category>
            {
                new Category
                {
                    Id = "1122",
                    Name = "Action",
                    Description = "Description......",
                    ImageUrl = "https://cdn.clipart.email/7bad7f269d0261c39d848eaeefa0d83c_filesports-and-gamessvg-wikimedia-commons_1102-1024.png"
                },
                new Category
                {
                    Id = "1123",
                    Name = "Adventure",
                    Description = "Some description......",
                    ImageUrl = "https://lh3.googleusercontent.com/proxy/Se4QkcpRfAb7bLYl_KcDNTP3UBfK3a5eYfswoQQiVS-HP8HBzQxjEqhC9M2QF04AqNaxD-MZ9PTTSwdmsuj_ZW7hXhjMXyPDVbGg1bwe_4_Y3kKzGDXTm9_a"
                }
            };

            context.Categories.AddRange(appCategories);
            context.SaveChanges();

            var category = appCategories[0];
            var categoryId = category.Id;

            var name = "Action";
            var description = "Description......";
            var imageUrl = "https://lh3.googleusercontent.com/proxy/Se4QkcpRfAb7bLYl_KcDNTP3UBfK3a5eYfswoQQiVS-HP8HBzQxjEqhC9M2QF04AqNaxD-MZ9PTTSwdmsuj_ZW7hXhjMXyPDVbGg1bwe_4_Y3kKzGDXTm9_a";


            await categoryService.Edit(categoryId, "Sport", "Hello new description", "https://cdn.clipart.email/7bad7f269d0261c39d848eaeefa0d83c_filesports-and-gamessvg-wikimedia-commons_1102-1024.png");


            Assert.True(name != category.Name);
            Assert.True(description != category.Description);
            Assert.True(imageUrl != category.ImageUrl);
        }
    }
}