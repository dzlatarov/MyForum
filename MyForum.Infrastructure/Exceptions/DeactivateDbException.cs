using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Infrastructure.Exceptions
{
    public class DeactivateDbException : ForumBaseException
    {
        private const string DeactivateDbExceptionMessage = GlobalConstants.DeactivateDbExceptionMessage;
        private readonly string errorMessage;

        public DeactivateDbException(string errorMessage = DeactivateDbExceptionMessage) 
            : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }
    }
}
