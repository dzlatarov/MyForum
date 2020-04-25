using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyForum.Infrastructure;
using MyForum.Services.Contracts;
using MyForum.Web.Models.Threads;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cloudscribe.Pagination.Models;
using MyForum.Domain;
using MyForum.Web.Models.Categories;

namespace MyForum.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IThreadsService threadsService;

        public CategoriesController(ICategoriesService categoriesService, IThreadsService threadsService)
        {
            this.categoriesService = categoriesService;
            this.threadsService = threadsService;
        }

        [Authorize]
        [Route("/Categories/AllThreads/{id}")]
        public IActionResult All(string id, int pageNumber = 1, int pageSize = 2)
        {
            var category = this.categoriesService.GetCategoryById(id).Result;

            if (category == null)
            {
                return NotFound();
            }

            ViewData["CategoryName"] = category.Name;

            var excludeRecords = (pageSize * pageNumber) - pageSize;

            var allThreadsInCategory = this.threadsService.All()
                .Where(t => t.CategoryId == id)
                .Skip(excludeRecords)
                .Take(pageSize)
                .Select(ThreadsAllViewModel.AllThreads)
                .ToList();


            var model = new ThreadsInCategoryListViewModel { Name = category.Name, Threads = allThreadsInCategory };

            var resultModel = new PagedResult<ThreadsAllViewModel>
            {
                Data = allThreadsInCategory.ToList(),
                TotalItems = this.threadsService.All().Where(t => t.CategoryId == id).ToList().Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return this.View(resultModel);
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        [Route("/Categories/Create")]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        [Route("/Categories/Create")]
        public async Task<IActionResult> Create(CategoriesCreateViewModel model)
        {

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.categoriesService.Create(model.Name, model.Description, model.ImageUrl);

            return this.Redirect("/");
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        [Route("/Categories/Delete{categoryId}")]
        public async Task<IActionResult> Delete(string categoryId)
        {
            var category = this.categoriesService.GetCategoryById(categoryId);

            if (category == null)
            {
                return this.NotFound();
            }

            await this.categoriesService.Delete(categoryId);

            return this.Redirect("/");
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        [Route("/Categories/Edit/{categoryId}")]
        public IActionResult Edit(string categoryId)
        {
            var category = this.categoriesService.GetCategoryById(categoryId).Result;

            if (category == null)
            {
                return this.NotFound();
            }

            var model = new CategoriesEditViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Name,
                ImageUrl = category.ImageUrl
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        [Route("/Categories/Edit/{categoryId}")]
        public async Task<IActionResult> Edit(string categoryId, CategoriesEditViewModel model)
        {
            if(!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.categoriesService.Edit(categoryId, model.Name, model.Description, model.ImageUrl);

            return this.Redirect("/");
        }
    }
}