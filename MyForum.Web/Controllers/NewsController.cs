using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyForum.Infrastructure;
using MyForum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Authorize(Roles = GlobalConstants.AdminRole)]
        [Route("/News/Create")]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }
    }
}
