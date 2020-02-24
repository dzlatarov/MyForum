using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.Web.Models.Users
{
    public class UsersProfileViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string PhoneNumber { get; set; }
    }
}