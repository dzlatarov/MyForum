using Microsoft.EntityFrameworkCore;
using MyForum.Domain;
using MyForum.Persistence;
using MyForum.Services;
using System;
using System.Collections.Generic;
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
    }
}
