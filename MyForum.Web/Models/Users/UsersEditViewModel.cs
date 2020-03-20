using MyForum.Domain;
using MyForum.Domain.Enums;
using MyForum.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyForum.Web.Models.Users
{
    public class UsersEditViewModel
    {
        public string Id { get; set; }

        [StringLength(20, ErrorMessage = GlobalConstants.LengthError,
              MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [StringLength(20, ErrorMessage = GlobalConstants.LengthError,
               MinimumLength = 3)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [StringLength(20, ErrorMessage = GlobalConstants.LengthError,
                MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(15, ErrorMessage = GlobalConstants.LengthError,
                MinimumLength = 3)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Date Of Birth")]
        public string DateOfBirth { get; set; }
    }    
}