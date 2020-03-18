using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.Web.Controllers
{
    public class ErrorsController : Controller
    {
        [Route("Error/404")]
        public IActionResult PageNotFound()
        {
            return this.View();
        }

        [Route("Error/{code:int}")]
        public IActionResult Error(int code)
        {
            return View();
        }
    }
}
