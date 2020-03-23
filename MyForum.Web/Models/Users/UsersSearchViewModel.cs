using System;
using MyForum.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace MyForum.Web.Models.Users
{
    public class UsersSearchViewModel
    {
        [StringLength(20, ErrorMessage = GlobalConstants.LengthError,
                MinimumLength = 5)]
        public string Username { get; set; }
    }
}