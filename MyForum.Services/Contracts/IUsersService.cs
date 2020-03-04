using MyForum.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Services
{
    public interface IUsersService
    {
        IEnumerable<ApplicationUser> All();

        ApplicationUser GetUserByUsername(string username);

        ApplicationUser GetUserById(string id);

        void Edit(string id, string username, string firstName, string middleName, string lastName, string email, string phoneNumber);
    }
}
