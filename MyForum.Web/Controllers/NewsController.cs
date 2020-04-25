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

        [Authorize(Roles = GlobalConstants.AdminRole)]
        [Route("/News/Details/{newsId}")]
        public IActionResult Details(string newsId)
        {
            var news = this.newsService.GetNewsById(newsId);

            if(news == null)
            {
                return this.NotFound();
            }

            var model = new NewsDetailsInfoViewModel
            {
                Id = news.Id,
                Name = news.Name,
                Content = news.Content,
                ImageUrl = news.ImageUrl
            };

            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        [Route("/News/Edit/{newsId}")]
        public IActionResult Edit(string newsId)
        {
            var news = this.newsService.GetNewsById(newsId);

            if(news == null)
            {
                return this.NotFound();
            }

            var model = new NewsEditViewModel
            {
                Id = news.Id,
                Name = news.Name,
                Content = news.Content,
                ImageUrl = news.ImageUrl
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        [Route("/News/Edit/{newsId}")]
        public async Task<IActionResult> Edit(string newsId, NewsEditViewModel model)
        {
            if(!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.newsService.Edit(newsId, model.Name, model.Content, model.ImageUrl);

            return this.RedirectToAction(nameof(All));
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        [Route("/News/Delete/{newsId}")]
        public async Task<IActionResult> Delete(string newsId)
        {
            var news = this.newsService.GetNewsById(newsId);

            if(news == null)
            {
                return this.NotFound();
            }

            await this.newsService.Delete(newsId);

            return this.RedirectToAction(nameof(All));
        }
    }
}