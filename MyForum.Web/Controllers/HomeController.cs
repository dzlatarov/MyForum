﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyForum.Web.Models;

namespace MyForum.Web.Controllers
{    
    public class HomeController : Controller
    {
        public HomeController()
        {            
        }
                
        public async Task<IActionResult> Index()
        {
            return this.View();
        }
    }
}
