using MyForum.Domain;
using MyForum.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyForum.Services.Contracts
{
    public interface IThreadsServices
    {
        IQueryable<Thread> All();
    }
}
