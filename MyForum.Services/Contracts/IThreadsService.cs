using MyForum.Domain;
using MyForum.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyForum.Services.Contracts
{
    public interface IThreadsService
    {
        IQueryable<Thread> All();

        //To do
        void Create(string name, string authorId);

        Thread GetThreadById(string id);

        string Edit(string threadId, string title, string content, DateTime modifiedOn);
    }
}
