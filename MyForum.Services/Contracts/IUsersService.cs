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

        ApplicationUser GetUserByUsername(string username);

        ApplicationUser GetUserById(string id);

        void Edit(string id, string firstName, string middleName, string lastName, string email, string phoneNumber, string dayOfBirth);
    }
}