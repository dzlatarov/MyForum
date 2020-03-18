using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Infrastructure.Exceptions
{
    public abstract class ForumBaseException : Exception
    {
        private const string ForumBaseExceptionMessage = GlobalConstants.ForumBaseExceptionMessage;
        private readonly string errorMessage;
        protected ForumBaseException(string errorMessage = ForumBaseExceptionMessage) : base(errorMessage)
        {
           this.errorMessage = errorMessage;
        }
    }
}
