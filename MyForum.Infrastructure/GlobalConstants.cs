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

        // Register
        public const int PasswordMinLength = 3;
        public const int PasswordMaxLength = 30;
        public const string LengthError = "The {0} must be at least {2} and at max {1} characters long.";
        public const string ConfirmPasswordEr = "The password and confirmation password do not match.";
        public const string DateTimeFormatError = "The format should be dd.mm.yyyy";

        // Login
        public const string LoginError = "Invalid Username or password";


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

        //Threads create model constraints

        public const int TitleMaxLength = 50;
        public const int TitleMinLength = 20;
        public const string TitleErrorMessage = "The {0} must be at least {2} and at max {1} characters long.";
        public const int ContentMaxLength = 300;
        public const int ContentMinLength = 20;
        public const string ContentErrorMessage = "The {0} must be at least {2} and at max {1} characters long.";

        //Status code with reExecute template
        public const string statusCodeWithReExecuteTemplate = "/Error/{0}";
    }
}
