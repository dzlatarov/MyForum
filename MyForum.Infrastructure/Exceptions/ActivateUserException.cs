using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Infrastructure.Exceptions
{
    public class ActivateUserException : ForumBaseException
    {
        private const string ActivateUserExceptionMessage = GlobalConstants.ActivateUserExceptionMessage;
        private readonly string errorMessage;

        public ActivateUserException(string errorMessage = ActivateUserExceptionMessage)
            : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }
    }
}
