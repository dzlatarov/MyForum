using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyForum.Services.Contracts;
using MyForum.Web.Models.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult All(string id)
        {
            var category = this.categoriesService.GetCategoryById(id);

            if(category == null)
            {
                return NotFound();
            }

            var allThreadsInCategory = this.threadsService.All()
                .Where(t => t.CategoryId == id)
                .Select(ThreadsAllViewModel.AllThreads)                
                .ToList();

            return this.View(new ThreadsInCategoryListViewModel { Name = category.Name, Threads = allThreadsInCategory });
        }
    }
}
