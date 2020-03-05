using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyForum.Services;
using MyForum.Services.Contracts;
using MyForum.Web.Models;
using MyForum.Web.Models.Threads;

namespace MyForum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IThreadsServices threadsServices;

        public HomeController(IUsersService usersService, IThreadsServices threadsServices)
        {
            this.usersService = usersService;
            this.threadsServices = threadsServices;
        }

        public IActionResult Index()
        {
            ViewData["Currentuser"] = this.usersService.GetUserByUsername(this.User.Identity.Name);
            var allThreads = this.threadsServices.All()
                .Select(t => new ThreadsAllViewModel
                {
                    Name = t.Name,
                    Username = t.ThreadCreator.UserName,
                    PostsCount = t.Posts.Count()
                }).ToList();

           
            ViewData["AllThreads"] = allThreads;

            var allUsers = this.usersService.All()   
                .Include(u => u.Threads)
                .Include(u => u.Posts)
                .Select(u => new AllUsersViewModel
                {
                    Id = u.Id,
                    Username = u.UserName,
                    ThreadsCount = u.Threads.Count,
                    PostsCount = u.Posts.Count
                }).ToList();


            return this.View(new AllUserListViewModel { Members = allUsers });
        }

        public async Task<IActionResult> About()
        {
            return this.View();
        }
    }
}
