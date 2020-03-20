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
            var model = new ThreadsCreateViewModel()
            {
                Categories = this.categoriesService.GetAll()
                .Select(ThreadCategoryViewModel.FromCategory)
                .ToList()
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [Route("/Threads/Create")]
        public IActionResult Create(ThreadsCreateViewModel model)
        {           
            if (!this.ModelState.IsValid)
            {
                model.Categories = this.categoriesService.GetAll()
                    .Select(ThreadCategoryViewModel.FromCategory)
                    .ToList();

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
            var thread = this.threadsService.GetThreadById(threadId);

            if(thread == null)
            {
                return NotFound();
            }

            this.threadsService.Delete(threadId);

            return this.Redirect($"/Categories/AllThreads/{thread.CategoryId}");
        }       
    }
}