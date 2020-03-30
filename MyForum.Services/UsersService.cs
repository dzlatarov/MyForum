using MyForum.Domain;
using MyForum.Infrastructure.Exceptions;
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

        public void Activate(string userId)
        {
            string errorMessage = "";

            try
            {
                var user = this.db.Users.FirstOrDefault(u => u.Id == userId && u.IsDeactivate == true);
                user.IsDeactivate = false;

                this.db.Users.Update(user);
                this.db.SaveChanges();
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                throw new ActivateUserException();
            }
        }

        public IQueryable<ApplicationUser> All()
        {
            var allUsers = this.db.Users;
            return allUsers;
        }

        public void Deactivate(string userId)
        {
            string errorMessage = "";

            try
            {
                var user = this.db.Users.FirstOrDefault(u => u.Id == userId && u.IsDeactivate == false);
                user.IsDeactivate = true;

                this.db.Users.Update(user);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                throw new DeactivateDbException();
            }
        }

        public void Edit(string id, string firstName, string middleName, string lastName, string email, string phoneNumber, string dayOfBirth)
        {
            var userFromDb = this.GetUserById(id);
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