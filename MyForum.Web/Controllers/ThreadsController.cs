using Microsoft.AspNetCore.Mvc;
using MyForum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.Web.Controllers
{
    public class ThreadsController : Controller
    {
        private readonly IThreadsServices threadsService;

        public ThreadsController(IThreadsServices threadsService)
        {
            this.threadsService = threadsService;
        }

        public IActionResult Create()
        {
            return this.View();
        }
    }
}
