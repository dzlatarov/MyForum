﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyForum.Services.Contracts;
using MyForum.Web.Models.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyForum.Web.Controllers
{
    public class ThreadsController : Controller
    {
        private readonly IThreadsService threadsService;

        public ThreadsController(IThreadsService threadsService)
        {
            this.threadsService = threadsService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]        
        [Route("/Threads/Create")]
        public IActionResult Create(ThreadsCreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var authorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            this.threadsService.Create(model.Content, authorId);

            return this.Redirect("/Home");
        }

        [Authorize]
        [Route("/Threads/Edit/{id}")]
        public IActionResult Edit(string id)
        {
            var thread = this.threadsService.GetThreadById(id);

            return this.Redirect($"/Categories/AllThreads{id}");
        }
    }
}
