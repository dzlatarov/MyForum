using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Infrastructure.Exceptions
{
    public class DeactivatedException : ForumBaseException
    {
        private const string DeactivatedExceptionMessage = GlobalConstants.DeactivatedExceptionMessage;
        private readonly string errorMessage;

        public DeactivatedException(string errorMessage = DeactivatedExceptionMessage) : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }
    }
}
