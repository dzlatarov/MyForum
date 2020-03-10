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
            Task.Run(SeedThreads).Wait();
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

        private async void SeedThreads()
        {
            if (this.context.Threads.Any())
            {
                return;
            }

            var allThreads = new List<Thread>()
            {
                 new Thread
                {
                     Id = Guid.NewGuid().ToString(),
                    Content = "I have problem in FIFA 2020 with my fut champion team can buy players. Has anyone deal with this bug ?",
                    Title = "FIFA 2020",
                    CreatedOn = DateTime.UtcNow,
                    ThreadCreatorId = "8232a4a5-44e8-4704-8adf-0a746b712dd5",
                    CategoryId = "3e475006-5ef0-4576-a71d-f0795d68c104"
                },
                 new Thread
                 {
                     Id = Guid.NewGuid().ToString(),
                     Content = "I recently updated my NBA and now i cannot play the game. Do you know how to fix this ?",
                     Title = "NBA",
                     CreatedOn = DateTime.UtcNow,
                    ThreadCreatorId = "ee80469d-4e2d-4445-b600-d3d275062f05",
                    CategoryId = "3e475006-5ef0-4576-a71d-f0795d68c104"
                },
                  new Thread
                 {
                     Id = Guid.NewGuid().ToString(),
                     Content = "I want to play Rocket League, can you tell me how to install it ?",
                     Title = "Rocket Legue",
                     CreatedOn = DateTime.UtcNow,
                    ThreadCreatorId = "210d4331-e2f8-44f2-9e37-878710d04e61",
                    CategoryId = "3e475006-5ef0-4576-a71d-f0795d68c104"
                }
            };

            await this.context.Threads.AddRangeAsync(allThreads);
            this.context.SaveChanges();
        }
    }
}