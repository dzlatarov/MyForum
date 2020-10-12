namespace MyForum.Web.Extensions
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using MyForum.Domain;
    using MyForum.Infrastructure;
    using MyForum.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = GlobalConstants.PasswordMinLength;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = GlobalConstants.UniqueChars;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.User.AllowedUserNameCharacters = GlobalConstants.AllowedChars;
                options.User.RequireUniqueEmail = true;
            })
               .AddDefaultUI()
               .AddRoles<IdentityRole>()
               .AddRoleManager<RoleManager<IdentityRole>>()
               .AddDefaultTokenProviders()
               .AddEntityFrameworkStores<MyForumDbContext>();

            return services;
        }
    }
}
