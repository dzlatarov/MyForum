using MyForum.Domain;
using MyForum.Persistence;
using MyForum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyForum.Services
{
    public class ThreadsServices : IThreadsServices
    {
        private readonly MyForumDbContext db;

        public ThreadsServices(MyForumDbContext db)
        {
            this.db = db;
        }

        public IQueryable<Thread> All()
        {
            var allTreads = this.db.Threads;
            return allTreads;
        }
    }
}
