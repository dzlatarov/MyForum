using MyForum.Domain;
using MyForum.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Services
{
    public class UsersService : IUsersService
    {
        private readonly MyForumDbContext db;

        public UsersService(MyForumDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<ApplicationUser> All()
        {
            var allUsers = this.db.Users.ToList();                
            return allUsers;
        }

        public ApplicationUser GetUserByUsername(string username)
        {
            return this.db.Users.FirstOrDefault(u => u.UserName == username);
        }
    }
}
