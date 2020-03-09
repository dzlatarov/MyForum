using MyForum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public int ThreadsCount { get; set; }
        
        public static Expression<Func<ApplicationUser, UsersProfileViewModel>> FromUser
        {
            get => u => new UsersProfileViewModel
            {
                Id = u.Id,
                Username = u.UserName,
                FirstName = u.FirstName,
                MiddleName = u.MiddleName,
                LastName = u.LastName,
                Gender = u.Gender.ToString(),
                PhoneNumber = u.PhoneNumber ?? string.Empty,
                Email = u.Email,
                ThreadsCount = u.Threads.Count,
                DateOfBirth = u.DateOfBirth
            };
        }
    }
}