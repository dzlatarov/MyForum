using MyForum.Domain;
using MyForum.Persistence;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public IQueryable<ApplicationUser> All()
        {
            var allUsers = this.db.Users;                
            return allUsers;
        }

        public void Edit(string id, string username, string firstName, string middleName, string lastName, string email, string phoneNumber, string dayOfBirth)
        {
            var userFromDb = this.GetUserById(id);
            userFromDb.UserName = username;
            userFromDb.FirstName = firstName;
            userFromDb.MiddleName = middleName;
            userFromDb.LastName = lastName;
            userFromDb.Email = email;
            userFromDb.PhoneNumber = phoneNumber;
            userFromDb.DateOfBirth = DateTime.Parse(dayOfBirth, CultureInfo.InvariantCulture);

            this.db.Update(userFromDb);
            this.db.SaveChanges();
        }

        public ApplicationUser GetUserById(string id)
        {
            return this.db.Users.FirstOrDefault(u => u.Id == id);
        }

        public ApplicationUser GetUserByUsername(string username)
        {
            return this.db.Users.FirstOrDefault(u => u.UserName == username);
        }
    }
}
