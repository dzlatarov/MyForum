﻿using System;
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
using MyForum.Web.Models.Categories;
using MyForum.Web.Models.Threads;

namespace MyForum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IUsersService usersService;

        public HomeController(ICategoriesService categoriesService, IUsersService usersService)
        {
            this.categoriesService = categoriesService;
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            ViewData["Currentuser"] = this.usersService.GetUserByUsername(this.User.Identity.Name);
            
            var allCategories = this.categoriesService.GetAll()
                .Select(CategoriesAllViewModel.AllCategories)
                .ToList();


            return this.View(new CategoriesListAllViewModel { Categories = allCategories });
        }        
    }
}