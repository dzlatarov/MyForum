using MyForum.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.Web.Models.Users
{
    public class UsersEditedViewModel
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
    }
}
