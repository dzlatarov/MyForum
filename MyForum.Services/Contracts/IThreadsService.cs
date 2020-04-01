using MyForum.Domain;
using MyForum.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Services.Contracts
{
    public interface IThreadsService
    {
        IQueryable<Thread> All();

        //To do
        Task Create(string title, string content, string authorId, string categoryId);

        Task<Thread> GetThreadById(string id);

        Task<string> Edit(string threadId, string title, string content, DateTime modifiedOn);

        Task Delete(string threadId);
    }
}