using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyForum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService service;

        public CategoriesController(ICategoriesService service)
        {
            this.service = service;
        }

        [Route("/Categories/AllThreads/{id}")]
        public IActionResult All(string id)
        {
            return this.View();
        }
    }
}
