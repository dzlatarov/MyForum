using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyForum.Domain;
using MyForum.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Persistence.Seeds
{
    public class MyForumSeedData
    {
        private readonly MyForumDbContext context;
        private readonly IApplicationBuilder app;
        private readonly IHostingEnvironment env;

        public MyForumSeedData(MyForumDbContext context, IApplicationBuilder app, IHostingEnvironment env)
        {
            this.context = context;
            this.app = app;
            this.env = env;
        }

        //Migrations
        private void ApplyMigration()
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MyForumDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }
            }
        }


        // Seed Roles
        private async void SeedRoles()
        {
            if (this.context.Roles.Any())
            {
                return;
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var roleManger = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                if (!roleManger.RoleExistsAsync(GlobalConstants.AdminRole).Result)
                {
                    await roleManger.CreateAsync(new IdentityRole(GlobalConstants.AdminRole));
                }

                if (!roleManger.RoleExistsAsync(GlobalConstants.UserRole).Result)
                {
                    await roleManger.CreateAsync(new IdentityRole(GlobalConstants.UserRole));
                }
            }
        }

        public async Task SeedAllData()
        {
            Task.Run(SeedRoles).Wait();
            Task.Run(ApplyMigration).Wait();
            Task.Run(SeedCategories).Wait();
            Task.Run(SeedCategories).Wait();
        }

        private async void SeedCategories()
        {
            if (this.context.Categories.Any())
            {
                return;
            }

            var allCategories = new List<Category>()
            {
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Sports",
                    Description = "Here you can find and discuss problems linked to all sports.",
                    ImageUrl = "https://code.org/images/sports/all_sports.png"
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Action",
                    Description = "Here you can find interesting action games problems and you can find the answer to your need.",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn"
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Adventure",
                    Description = "Adventure games bugs and fixes",
                    ImageUrl = "https://i.ytimg.com/vi/YGy32f86fbI/maxresdefault.jpg"
                }
            };

            await this.context.Categories.AddRangeAsync(allCategories);
            await this.context.SaveChangesAsync();
        }
    }
}
