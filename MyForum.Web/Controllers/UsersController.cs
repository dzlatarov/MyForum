using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyForum.Infrastructure;
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
                .Select(AllUsersViewModel.AllUser)
                .ToList();

            return this.View(new AllUserListViewModel { Members = allUsers });
        }

        [Authorize]
        [Route("/Users/Profile/{id}")]
        public IActionResult Profile(string id)
        {
            var user = this.usersService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            var userModel = this.usersService.All()
                 .Where(u => u.Id == id)
                 .Select(UsersProfileViewModel.FromUser)
                .FirstOrDefault();

            return this.View(userModel);
        }

        [Authorize]
        [Route("/Users/Profile/Edit/{id}")]
        public IActionResult EditProfile(string id)
        {
            var user = this.usersService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new UsersEditViewModel
            {
                Id = id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth.ToString("dd/MM/yyyy")
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [Route("/Users/Profile/Edit/{id}")]
        public IActionResult EditProfile(string id, UsersEditViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            this.usersService.Edit(id, input.FirstName, input.MiddleName, input.LastName, input.Email, input.PhoneNumber, input.DateOfBirth);

            return this.RedirectToAction(nameof(Profile), new { id = this.User.FindFirstValue(ClaimTypes.NameIdentifier) });
        }

        [Authorize]
        [Route("/Users/Search")]
        public IActionResult Search(UsersSearchViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = this.usersService.GetUserByUsername(model.Username);

            if (user == null)
            {
                return this.View();
            }

            return this.RedirectToAction(nameof(Profile), new { id = user.Id });
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        [Route("/Users/Deactivate/{id}")]
        public IActionResult Deactivate(string id)
        {
            var user = this.usersService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            this.usersService.Deactivate(id);

            return this.RedirectToAction(nameof(All));
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        public IActionResult Activate(string id)
        {
            var user = this.usersService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            this.usersService.Activate(id);
            return this.RedirectToAction(nameof(All));
        }
    }
}