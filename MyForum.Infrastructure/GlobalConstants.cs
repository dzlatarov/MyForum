using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Infrastructure
{
    public class GlobalConstants
    {
        // Project info
        public const string ProjectName = "Game Zone";
        public const string ProjectAuthor = "Danail Zlatarov";
        public const string ProjectDescription = "Problem solving forum";

        // Roles
        public const string AdminRole = "Admin";
        public const string UserRole = "User";

        // Password
        public const int PasswordMinLength = 3;
        public const int PasswordMaxLength = 30;
        public const string LengthError = "The {0} must be at least {2} and at max {1} characters long.";
        public const string ConfirmPasswordEr = "The password and confirmation password do not match.";


        // Identity options
        public const string AllowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        public const int UniqueChars = 0;

        //Connection configure
        public const string ConnectionName = "DefaultConnection";

        //MVC routing config
        public const string RouteName = "default";
        public const string template = "{controller=Home}/{action=Index}/{id?}";

        //Exeptions
        public const string ExeptionHandlerErr = "/Home/Error";

        //Coockie paths
        public const string LoginPath = "/Identity/Account/Login";
        public const string LogoutPath = "/Identity/Account/Logout";
    }
}
