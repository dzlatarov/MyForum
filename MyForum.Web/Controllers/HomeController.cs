using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyForum.Persistence;
using MyForum.Services;
using MyForum.Services.Contracts;
using MyForum.Web.Models;
using MyForum.Web.Models.Threads;

namespace MyForum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IThreadsService threadsServices;      

        public HomeController(IUsersService usersService, IThreadsService threadsServices)
        {
            this.usersService = usersService;
            this.threadsServices = threadsServices;
        }

        public IActionResult Index()
        {
            ViewData["Currentuser"] = this.usersService.GetUserByUsername(this.User.Identity.Name);
            var allThreads = this.threadsServices.All()
                .Select(ThreadsAllViewModel.AllThreads)
                .ToList();

            ViewData["AllThreads"] = allThreads;

            var allUsers = this.usersService.All()                
                .Select(u => new AllUsersViewModel
                {
                    Id = u.Id,
                    Username = u.UserName,
                    ThreadsCount = u.Threads.Count,                   
                }).ToList();


            return this.View(new AllUserListViewModel { Members = allUsers });
        }

        public async Task<IActionResult> About()
        {
            return this.View();
        }
    }
}
