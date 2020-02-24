using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyForum.Services;
using MyForum.Web.Models;

namespace MyForum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService usersService;

        public HomeController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            ViewData["Currentuser"] = this.usersService.GetUserByUsername(this.User.Identity.Name);

            var allUsers = this.usersService.All()
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
