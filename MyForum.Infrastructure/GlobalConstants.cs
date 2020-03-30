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

        //Exeptions
        public const string ForumBaseExceptionMessage = @"Something went wrong during the process! /n Please try again!";
        public const string CreateThreadExceptionMessage = @"Something went wrong during creating a new thread! /n Please try again!";
        public const string DeactivatedExceptionMessage = @"Sorry u are deactivated!";
        public const string DeactivateDbExceptionMessage = @"Somethin went wrong during deactivating the user! /n Please try again!";

        //Coockie paths
        public const string LoginPath = "/Identity/Account/Login";
        public const string LogoutPath = "/Identity/Account/Logout";

        //Create model constraints

        public const int TitleMaxLength = 50;
        public const int TitleMinLength = 20;
        public const string TitleErrorMessage = "The {0} must be at least {2} and at max {1} characters long.";
        public const int ContentMaxLength = 300;
        public const int ContentMinLength = 20;
        public const string ContentErrorMessage = "The {0} must be at least {2} and at max {1} characters long.";
        public const int CommentContentMaxLength = 500;
        public const string CommentErrorMessage = "The {0} must be at least {2} and max {1} characters long.";        

        //Configure
        public const string statusCodeWithReExecuteTemplate = "/Error/{0}";
        public const string RouteName = "default";
        public const string template = "{controller=Home}/{action=Index}/{id?}";
        public const string ExeptionHandlerErr = "/Home/Error";
    }
}