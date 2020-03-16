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
        private readonly ICategoriesService categoriesService;

        public ThreadsController(IThreadsService threadsService, ICategoriesService categoriesService)
        {
            this.threadsService = threadsService;
            this.categoriesService = categoriesService;
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewData["AllCategories"] = this.categoriesService.GetAll().ToList();

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

            this.threadsService.Create(model.Title, model.Content, authorId, model.CategoryId);

            return this.Redirect($"/Categories/AllThreads/{model.CategoryId}");
        }

        [Authorize]
        [Route("/Threads/Edit/{id}")]
        public IActionResult Edit(string id)
        {
            var thread = this.threadsService.All()
                .Where(t => t.Id == id)
                .Select(ThreadsEditViewModel.EditThread)
                .FirstOrDefault();

            //var model = new ThreadsEditViewModel
            //{
            //    Id = thread.Id,
            //    Title = thread.Title,
            //    Content = thread.Content,
            //    CategoryId = thread.CategoryId
            //};

            return this.View(thread);
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

        [Authorize]
        [Route("/Threads/Delete{threadId}")]
        public IActionResult Delete(string threadId)
        {
            var categoryId = this.threadsService.GetThreadById(threadId).CategoryId;

            this.threadsService.Delete(threadId);

            return this.Redirect($"/Categories/AllThreads/{categoryId}");
        }
    }
}