using Microsoft.AspNetCore.Mvc;
using MyForum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.Web.Controllers
{
    public class NewsController : Controller
    {
        public NewsController(INewsService newsService)
        {

        }
    }
}
