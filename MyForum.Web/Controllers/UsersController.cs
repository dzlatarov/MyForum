using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.Web.Controllers
{
    public class UsersController : Controller
    {
        public UsersController()
        {

        }

        public async Task<IActionResult> All()
        {
           return this.Redirect("/");
        }
    }
}
