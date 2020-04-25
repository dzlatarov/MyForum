﻿using Microsoft.EntityFrameworkCore;
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
    }
}