using Microsoft.AspNetCore.Mvc;
using MyForum.Services;
using MyForum.Web.Models;
using MyForum.Web.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult All()
        {
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

        public IActionResult Profile(string username)
        {
            var user = this.usersService.GetUserByUsername(username);
            var viewModel = new UsersProfileViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Gender = user.Gender.ToString(),
                PhoneNumber = string.IsNullOrEmpty(user.PhoneNumber) ? " " : user.PhoneNumber,
                Email = user.Email
            };
            return this.View(viewModel);
        }
    }
}
