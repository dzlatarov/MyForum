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
               MinimumLength = 5)]
        public string Username { get; set; }

        [StringLength(20, ErrorMessage = GlobalConstants.LengthError,
              MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = GlobalConstants.LengthError,
               MinimumLength = 3)]
        public string MiddleName { get; set; }

        [StringLength(20, ErrorMessage = GlobalConstants.LengthError,
                MinimumLength = 3)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(15, ErrorMessage = GlobalConstants.LengthError,
                MinimumLength = 3)]
        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }
    }    
}
