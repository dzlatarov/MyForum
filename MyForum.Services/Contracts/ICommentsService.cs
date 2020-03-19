using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Services.Contracts
{
    public interface ICommentsService
    {
        void CreateComment(string content, string threadId, string creatorId);
    }
}
