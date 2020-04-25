using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyForum.Domain;
using MyForum.Domain.Enums;
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

        //Seed admins
        private void SeedAdmins()
        {
            var adminRole = GlobalConstants.AdminRole;

            var usersFromDb = this.context.Users;

            var admins = new List<ApplicationUser>()
            {
                new ApplicationUser
                {
                    UserName = "pesho",
                    NormalizedUserName = "pesho".ToUpper(),
                    Email = "pesho@myForum.com",
                    NormalizedEmail = "pesho@myForum.com".ToUpper(),
                    PasswordHash = "123456",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = true,
                    FirstName = "Pesho",
                    LastName = "Peshov",
                    Gender = Gender.Male,
                    IsDeactivate = false
                },
                new ApplicationUser
                {
                    UserName = "ivancho",
                    NormalizedUserName = "ivancho".ToUpper(),
                    Email = "ivancho@myForum.com",
                    NormalizedEmail = "ivancho@myForum.com".ToUpper(),
                    PasswordHash = "123456",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = true,
                    FirstName = "Ivancho",
                    LastName = "Ivanov",
                    Gender = Gender.Male,
                    IsDeactivate = false,
                },
                new ApplicationUser
                {
                    UserName = "maria",
                    NormalizedUserName = "maria".ToUpper(),
                    Email = "maria@myForum.com",
                    NormalizedEmail = "maria@myForum.com".ToUpper(),
                    PasswordHash = "123456",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = true,
                    FirstName = "Maria",
                    LastName = "Ivanova",
                    Gender = Gender.Female,
                    IsDeactivate = false,
                }
            };

            HashPaswword(admins, usersFromDb);
            CreateUserAddRole(admins, usersFromDb, adminRole).Wait();
            this.context.SaveChanges();
        }

        // Seed  users
        private void SeedUsers()
        {
            var userRole = GlobalConstants.UserRole;

            var usersFromDb = this.context.Users;

            var usersToDb = new List<ApplicationUser>()
            {
                new ApplicationUser
                {
                    UserName = "petar",
                    NormalizedUserName = "petar".ToUpper(),
                    Email = "petar@myForum.com",
                    NormalizedEmail = "petar@myForum.com".ToUpper(),
                    PasswordHash = "123456",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = true,
                    FirstName = "Petar",
                    LastName = "Petrov",
                    Gender = Gender.Male,
                    IsDeactivate = false
                },
                new ApplicationUser
                {
                    UserName = "mishoto",
                    NormalizedUserName = "mishoto".ToUpper(),
                    Email = "mishoto@myForum.com",
                    NormalizedEmail = "mishoto@myForum.com".ToUpper(),
                    PasswordHash = "123456",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = true,
                    FirstName = "Mihail",
                    LastName = "Mihailov",
                    Gender = Gender.Male,
                    IsDeactivate = false
                },
                new ApplicationUser
                {
                    UserName = "mirela",
                    NormalizedUserName = "mirela".ToUpper(),
                    Email = "mirela@myForum.com",
                    NormalizedEmail = "mirela@myForum.com".ToUpper(),
                    PasswordHash = "123456",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = true,
                    FirstName = "Mirela",
                    LastName = "Petrova",
                    Gender = Gender.Female,
                    IsDeactivate = false
                },
            };

            HashPaswword(usersToDb, usersFromDb);
            CreateUserAddRole(usersToDb, usersFromDb, userRole).Wait();
            this.context.SaveChanges();
        }

        public async Task SeedAllData()
        {
            Task.Run(ApplyMigration).Wait();
            Task.Run(SeedRoles).Wait();
            Task.Run(SeedAdmins).Wait();
            Task.Run(SeedUsers).Wait();
            Task.Run(SeedCategories).Wait();
            Task.Run(SeedNews).Wait();
            //Task.Run(SeedThreads).Wait();
            //Task.Run(SeedComments).Wait();
        }

        //Seed Categories
        private void SeedCategories()
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
                    Name = "Sport",
                    Description = "Sport game bugs and fixes.",
                    ImageUrl = GlobalConstants.SportsImg
                },
                new Category
                {
                   Id = Guid.NewGuid().ToString(),
                    Name = "Action",
                    Description = "Action games bugs and fixes",
                    ImageUrl = GlobalConstants.ActionImg
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Adventure",
                    Description = "Adventure games bugs and fixes",
                    ImageUrl = GlobalConstants.AdventureImg
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Strategy",
                    Description = "Strategy games bugs and fixes",
                    ImageUrl = GlobalConstants.StrategyImg
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Simulation",
                    Description = "Simulation games bugs and fixes",
                    ImageUrl = GlobalConstants.SimulationImg
                }
            };

            this.context.Categories.AddRange(allCategories);
            this.context.SaveChanges();
        }

        //Seed News
        private void SeedNews()
        {
            if (this.context.News.Any())
            {
                return;
            }

            var allNews = new List<News>()
            {
                new News
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "QUICK LOL THOUGHTS: APRIL 24",
                    Content = "With Mecha Kingdoms especially we felt we’d gone a bit too far in reducing commitment needed, both from the perspective of how exclusive prestige skins should be and also just from a revenue perspective. Event pass and related sales help fund things like game modes, ability to give away content in free missions/loot, etc. With Galaxies we pulled back as a result, though then realized we’d put too much grind back in. As a result we pivoted partway through Galaxies, putting more tokens into missions to reduce grind.",
                    ImageUrl = "https://images.contentstack.io/v3/assets/blt731acb42bb3d1659/bltf6dd19677aae2ddd/5e69a61134ac7c1e5a6f980d/LoL_Thoughts_Banner.jpg",
                    CreatedOn = DateTime.UtcNow
                },
                new News
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Team Fortress 2 and Counter-Strike: Global Offensive source code has been leaked",
                    Content = "Looks like there’s a drip somewhere in Valve’s spigot. Source code for both Team Fortress 2 and Counter-Strike: Global Offensive has apparently been made public. The files appear to be from years ago, though that may not prevent present troublemakers from doing their thing. Some fan servers are worried enough that they’ve gone offline until they can assure that these leaks won’t compromise the security of players. So far it’s unclear where the leak originated, though of course that hasn’t stopped anyone from guessing or pointing fingers.",
                    ImageUrl = "https://assets.rockpapershotgun.com/images/2019/10/team-fortress-2-meet-the-spy-b.jpg",
                    CreatedOn = DateTime.UtcNow
                }
            };

            this.context.News.AddRange(allNews);
            this.context.SaveChanges();
        }

        //Seed Threads
        //private async void SeedThreads()
        //{
        //    if (this.context.Threads.Any())
        //    {
        //        return;
        //    }

        //    var allThreads = new List<Thread>()
        //    {
        //         new Thread
        //        {
        //             Id = "c33813ac-671b-4283-adf8-2f4b2f657aea",
        //            Content = "I have problem in FIFA 2020 with my fut champion team cannot buy players. Has anyone deal with this bug ?",
        //            Title = "Problem with Fifa 2020 new patch...",
        //            CreatedOn = DateTime.UtcNow,
        //            ModifiedOn = null,
        //            ThreadCreatorId = "4e18436d-ac14-4dea-915e-9078f1857b19",
        //            CategoryId = "a077753c-8ae1-494a-b381-99a466d37008"
        //        },
        //         new Thread
        //         {
        //             Id = "ebdcf790-3b90-4119-aea5-5a41d22e8764",
        //             Content = "I recently updated my NBA and now i cannot play the game. Do you know how to fix this ?",
        //             Title = "NBA",
        //             CreatedOn = DateTime.UtcNow,
        //             ModifiedOn = null,
        //             ThreadCreatorId = "bd19a8b2-1885-4f41-bfc4-5524ebd94fcb",
        //             CategoryId = "a077753c-8ae1-494a-b381-99a466d37008"
        //        },
        //          new Thread
        //         {
        //             Id = "26cbb7c9-ba97-47cc-83ea-25ac75721004",
        //             Content = "I want to play Rocket League, can you tell me how to install it ?",
        //             Title = "Rocket Legue",
        //             CreatedOn = DateTime.UtcNow,
        //             ModifiedOn = null,
        //             ThreadCreatorId = "f1e55758-4216-4153-8020-68c5b8be1f3b",
        //             CategoryId = "6832ad8e-7597-4ca7-8fb1-63eca8cdcfe0"
        //        }
        //    };

        //    await this.context.Threads.AddRangeAsync(allThreads);
        //    this.context.SaveChanges();
        //}

        //seed comments
        //private async void SeedComments()
        //{
        //    if (this.context.Comments.Any())
        //    {
        //        return;
        //    }

        //    var listOfComments = new List<Comment>()
        //    {
        //        new Comment
        //        {
        //            Id = "e99b9f19-683e-4d71-9268-bd1308140dad",
        //            Content = "Try to unninstall the game and then install it. It worked for me, i hope this will work for you.",
        //            ThreadId = "ebdcf790-3b90-4119-aea5-5a41d22e8764",
        //            CommentCreatorId = "1cb6d955-5de7-446f-b272-a0fb3e53c2cf",
        //            CreatedOn = DateTime.UtcNow
        //        },
        //        new Comment
        //        {
        //             Id = "1d1f95f0-ed6e-4e3f-9166-964a5c1b9960",
        //            Content = "Try to restart your console, then sign out from the marketplace, wait 5 minutes and after sign in.",
        //            ThreadId = "c33813ac-671b-4283-adf8-2f4b2f657aea",
        //            CommentCreatorId = "bd19a8b2-1885-4f41-bfc4-5524ebd94fcb",
        //            CreatedOn = DateTime.UtcNow
        //        },
        //        new Comment
        //        {
        //             Id = "411dc6bf-a0df-40ba-9022-c0143618477a",
        //            Content = "Open Youtube and there are simple steps to install Rocket League.",
        //            ThreadId = "26cbb7c9-ba97-47cc-83ea-25ac75721004",
        //            CommentCreatorId = "11b928d4-7677-4add-ae10-50ecf27f0ac8",
        //            CreatedOn = DateTime.UtcNow
        //        }
        //    };

        //    await this.context.Comments.AddRangeAsync(listOfComments);
        //    this.context.SaveChanges();
        //}

        //password hasher
        public void HashPaswword(List<ApplicationUser> usersToDb, DbSet<ApplicationUser> usersFromDb)
        {
            foreach (var currentUser in usersToDb)
            {
                if (usersFromDb.Any(user => user.UserName == currentUser.UserName) == false)
                {
                    var password = new PasswordHasher<ApplicationUser>().HashPassword(currentUser, currentUser.PasswordHash);
                    currentUser.PasswordHash = password;
                }
            }
        }

        // Set role
        private async Task CreateUserAddRole(List<ApplicationUser> usersToDb, DbSet<ApplicationUser> usersFromDb, string role)
        {
            foreach (var currentUser in usersToDb)
            {
                if (usersFromDb.Any(user => user.UserName == currentUser.UserName) == false)
                {
                    var userStore = new UserStore<ApplicationUser>(context);
                    await userStore.CreateAsync(currentUser);
                    await userStore.AddToRoleAsync(currentUser, role);
                }
            }
        }
    }
}