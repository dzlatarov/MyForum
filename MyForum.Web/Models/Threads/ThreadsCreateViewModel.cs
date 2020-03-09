using MyForum.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.Web.Models.Threads
{
    public class ThreadsCreateViewModel
    {
        [Required]
        [StringLength(GlobalConstants.NameMaxLength, ErrorMessage = GlobalConstants.NameErrorMessage,
           MinimumLength = GlobalConstants.NameMinLength)]
        [Display(Name= "Thread Content")]
        public string Content { get; set; }       
    }
}
