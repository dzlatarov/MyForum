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
    }
}