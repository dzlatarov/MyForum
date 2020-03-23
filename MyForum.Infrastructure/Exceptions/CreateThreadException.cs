using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Infrastructure.Exceptions
{
    public class CreateThreadException : ForumBaseException
    {
        private const string CreateThreadExceptionMessage = GlobalConstants.CreateThreadExceptionMessage;
        private readonly string errorMessage;

        public CreateThreadException(string errorMessage = CreateThreadExceptionMessage) : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }
    }
}