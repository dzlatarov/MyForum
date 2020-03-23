﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.Web.Models.Comments
{
    public class CommentsAllViewModel
    {       
        [Display(Name = "Thread Content")]
        public string ThreadContent { get; set; }

        public List<CommentsInfoViewModel> Comments { get; set; }        
    }
}