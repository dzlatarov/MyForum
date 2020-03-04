using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyForum.Domain;
using MyForum.Infrastructure;
using MyForum.Persistence;
using MyForum.Persistence.Seeds;
using Microsoft.AspNetCore.Hosting;
using MyForum.Services;

namespace MyForum.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";
                options.SuppressXFrameOptionsHeader = false;
            });

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            })
               .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = GlobalConstants.LoginPath;
                options.LogoutPath = GlobalConstants.LogoutPath;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContextPool<MyForumDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString(GlobalConstants.ConnectionName)));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
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


            services.AddControllersWithViews();
            services.AddRazorPages();
           
            services.AddTransient<MyForumDbContext>();
            services.AddTransient<IUsersService, UsersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IAntiforgery antiForgery, MyForumDbContext context)
        {
            MyForumSeedData seeder = new MyForumSeedData(context, app, env);
            seeder.SeedAllData().Wait();

            if (env.IsDevelopment())
            {                
                app.UseDeveloperExceptionPage();                   
            }
            else
            {
                app.UseExceptionHandler(GlobalConstants.ExeptionHandlerErr);
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: GlobalConstants.RouteName,
                    pattern: GlobalConstants.template);
                endpoints.MapRazorPages();
            });

            app.Use(next => httpContext =>
            {
                string path = httpContext.Request.Path.Value;

                if (
                    string.Equals(path, "/", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(path, "/index.html", StringComparison.OrdinalIgnoreCase))
                {
                // The request token can be sent as a JavaScript-readable cookie, 
                // and Angular uses it by default.
                var tokens = antiForgery.GetAndStoreTokens(httpContext);
                    httpContext.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
                        new CookieOptions() { HttpOnly = false });
                }

                return next(httpContext);
            });
        }
    }
}
