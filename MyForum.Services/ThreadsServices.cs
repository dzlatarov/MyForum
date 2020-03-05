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

        public void Create(string name, string authorId)
        {
            var thread = new Thread
            {
                Name = name,
                ThreadCreatorId = authorId
            };

            this.db.Threads.Add(thread);
            this.db.SaveChanges();
        }
    }
}
