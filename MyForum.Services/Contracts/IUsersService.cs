using MyForum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Services
{
    public interface IUsersService
    {
        IQueryable<ApplicationUser> All();

        Task<ApplicationUser> GetUserByUsername(string username);

        Task<ApplicationUser> GetUserById(string id);

        Task Edit(string id, string firstName, string middleName, string lastName, string email, string phoneNumber, string dayOfBirth);

        Task Deactivate(string userId);

        Task Activate(string userId);
    }
}