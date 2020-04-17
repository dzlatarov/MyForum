using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyForum.Infrastructure;
using MyForum.Services.Contracts;
using MyForum.Web.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyForum.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService newsService;

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        [Authorize]
        [Route("/News/All")]
        public IActionResult All()
        {
            var allNews = this.newsService.GetAll()
                .Select(n => new NewsAllInfoViewModel()
                {
                    Id = n.Id,
                    Name = n.Name,
                    Content = n.Content,
                    CreatedOn = n.CreatedOn,
                    CreatorName = n.Creator.UserName,
                    ImageUrl = n.ImageUrl
                })
                .ToList();

            return this.View(new NewsAllInfoListViewModel { News = allNews });
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        [Route("/News/Create")]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        [Route("/News/Create")]
        public async Task<IActionResult> Create(NewsCreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var creatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.newsService.Create(model.Name, model.Content, model.ImageUrl, creatorId);

            return this.RedirectToAction(nameof(All));
        }
    }
}