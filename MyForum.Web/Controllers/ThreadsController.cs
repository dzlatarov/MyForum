using Microsoft.AspNetCore.Authorization;
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

            var model = new ThreadsEditViewModel
            {
                Id = thread.Id,
                Title = thread.Title,
                Content = thread.Content,
                CategoryId = thread.CategoryId
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [Route("/Threads/Edit/{threadId}")]
        public IActionResult Edit(string threadId, ThreadsEditViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            DateTime modifiedOn = DateTime.UtcNow;
            var categoryId = this.threadsService.Edit(threadId, input.Title, input.Content, modifiedOn);

            return this.Redirect($"/Categories/AllThreads/{categoryId}");
        }
    }
}
