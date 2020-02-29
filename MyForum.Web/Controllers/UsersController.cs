using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyForum.Services;
using MyForum.Web.Models;
using MyForum.Web.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [Authorize]
        [Route("/Users/Profile/{username}")]
        public IActionResult Profile(string username)
        {
            var user = this.usersService.GetUserByUsername(username);
            var viewModel = new UsersProfileViewModel()
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Gender = user.Gender.ToString(),
                PhoneNumber = string.IsNullOrEmpty(user.PhoneNumber) ? " " : user.PhoneNumber,
                Email = user.Email,
                ThreadsCount = user.Threads.Count,
                PostsCount = user.Posts.Count
            };
            return this.View(viewModel);
        }

        [Authorize]
        [Route("/Users/Profile/Edit/{id}")]
        public IActionResult EditProfile(string id)
        {
            var user = this.usersService.GetUserById(id);
            var viewModel = new UsersEditViewModel
            {
                Id = id,
                Username = user.UserName,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [Route("/Users/Profile/Edit/{id}")]
        public IActionResult EditProfile(string id, UsersEditedViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect($"/Users/Profile/Edit/{id}");
            }

            this.usersService.Edit(id, input.Username, input.FirstName, input.MiddleName, input.LastName, input.Email, input.PhoneNumber);

            return this.Redirect($"/Users/Profile/{this.User.Identity.Name}");
        }
    }
}